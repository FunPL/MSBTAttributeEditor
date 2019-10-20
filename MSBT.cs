//Credit to: https://github.com/shadowninja108/MSBTPatch
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSBTEditor
{
    public class MSBT
    {
        public bool failed = false;

        Log log;
        public Dictionary<long, Entry> entries;

        public ByteOrder endianness;

        public byte[] FilePart1;
        public byte[] FilePart2;

        public static long ATR1size;

        public MSBT(byte[] bytes, Log ilog)
        {
            log = ilog;
            log.newLog("[MSBT] == Started parsing ==");
            MemoryStream stream = new MemoryStream(bytes);
            BinaryDataReader reader = new BinaryDataReader(stream, Encoding.ASCII) { ByteConverter = ByteConverter.BigEndian };

            if (reader.ReadString(8) != "MsgStdBn")
                throw new InvalidDataException("MsgStdBn magic is missing!");

            ushort bom = reader.ReadUInt16();

            if (bom != 0xFEFF && bom != 0xFFFE)
                throw new InvalidDataException("BOM is not 0xFEFF or 0xFFFE!");

            endianness = (ByteOrder)bom;
            reader.ByteOrder = endianness;

            if (reader.ReadUInt16() != 0x0)
                throw new InvalidDataException("Invalid data!");

            reader.ReadBytes(2); // skip unknown

            int sections = reader.ReadUInt16();

            if (reader.ReadUInt16() != 0x0)
                throw new InvalidDataException("Invalid data!");

            long filesize = reader.ReadUInt32();

            if (reader.ReadBytes(10).IsOnly<byte>(0))
                throw new InvalidDataException("Invalid data!");

            Dictionary<long, string> LBL1 = null;
            Dictionary<int, Attributes> ATR1 = null;
            Dictionary<int, MsbtString> TXT2 = null;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (true)
            {
                try
                {
                    string magic = reader.ReadString(4);
                    log.newLog($"[MSBT] ({reader.Position - 4}) Trying to parse " + magic);
                    if(LBL1 != null && ATR1 != null && TXT2 != null)
                    {
                        break;
                    }
                    switch (magic)
                    {
                        case "LBL1":
                            log.newLog("[MSBT] Found section LBL1");

                            LBL1 = ParseLBL1(reader);

                            break;
                        case "ATR1":
                            log.newLog("[MSBT] Found section ATR1");
                            long retPos = reader.Position;
                            reader.Position = 0;
                            FilePart1 = reader.ReadBytes((int)retPos + 20);
                            reader.Position = retPos;

                            ATR1 = ParseATR1(reader);

                            break;
                        case "TXT2":
                            log.newLog("[MSBT] Found section TXT2");
                            long retPos2 = reader.Position;
                            long bytesTillEnd = reader.Length - retPos2;
                            bytesTillEnd += 4;
                            reader.Position -= 4;
                            FilePart2 = reader.ReadBytes((int)bytesTillEnd);
                            reader.Position = retPos2;

                            TXT2 = ParseTXT2(reader);

                            break;
                        default:
                            log.newLog("[MSBT] Unrecognized section "+magic);
                            if (!Encoding.ASCII.GetBytes(magic).IsOnly<byte>(0xAB))
                                Console.WriteLine($"Unknown section {magic}!");
                            reader.Seek(-3); // move back so that the read string moves 1 byte at a time (and not 4)
                            break;
                    }
                    if (sw.ElapsedMilliseconds > 30000)
                    {
                        if (MessageBox.Show("This seems to be taking too long... Do you want to continue?", "MSBTEditor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            failed = true;
                            return;
                        }
                        sw.Restart();
                    }
                }
                catch (EndOfStreamException)
                {
                    break; // end of file, just exit
                }
            }

            entries = new Dictionary<long, Entry>();

            foreach (KeyValuePair<long, string> kv in LBL1)
            {
                int key = (int)kv.Key;
                if (!TXT2.ContainsKey(key))
                    throw new InvalidDataException($"TXT2 does not have the associated index {kv.Key} from LBL1!");
                Entry entry = new Entry();
                entry.key = kv.Value;
                entry.value = TXT2[key];
                entry.attributes = ATR1.GetValue(key);
                entries.Add(kv.Key, entry);
            }

            stream.Dispose();
            log.newLog("[MSBT] Done parsing");
        }

        public void Write(Stream stream)
        {
            BinaryDataWriter writer = new BinaryDataWriter(stream, Encoding.ASCII) { ByteOrder = ByteOrder.BigEndian };

            int[] order = new[] { 3, 4, 1, 2 };

            // order by id so that code works
            entries = entries.OrderBy(x => x.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

            byte[] unk7bytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00 };

            writer.WriteObject(FilePart1);

            writer.Position -= 20;

            writer.WriteObject(BitConverter.GetBytes(Convert.ToUInt32(entries.Count * 25 + 8)));

            writer.Position += 8;
            //writer.Position -= 8;

            writer.WriteObject(BitConverter.GetBytes(Convert.ToUInt32(entries.Count)));

            writer.Position += 4;

            foreach (KeyValuePair<long, Entry> kv in entries)
            {
                Attributes set = kv.Value.attributes ?? new Attributes();
                writer.WriteObject(set.unk1);
                writer.WriteObject(set.unk2);
                writer.WriteObject(set.type);
                writer.WriteObject(set.unk3);
                writer.WriteObject(BitConverter.GetBytes(set.unk4));
                writer.WriteObject(set.unk5);
                writer.WriteObject(set.unk6);
                writer.WriteObject(unk7bytes);

            }

            if(writer.Position % 16 != 0)
                writer.WritePadding(0xAB);

            writer.WriteObject(FilePart2);

            if (writer.Position % 16 != 0)
                writer.WritePadding(0xAB);
            writer.Position = 16;
            
            var bytes = BitConverter.GetBytes(Convert.ToUInt32(writer.Length));
            writer.WriteObject(order.Select(i => bytes[i-1]));
        }

        public static Dictionary<long, string> ParseLBL1(BinaryDataReader reader)
        {
            long size = reader.ReadUInt32();

            if (reader.ReadBytes(8).IsOnly<byte>(0)) // padding
                throw new InvalidDataException("Offset 0x08 of LBL1 section is not empty!");

            long position = reader.Position; // offsets for entry text is relative from here, don't ask why

            List<Tuple<UInt32, UInt32>> entries = new List<Tuple<UInt32, UInt32>>();
            long entriesLength = reader.ReadUInt32();

            for (int i = 0; i < entriesLength; i++)
                entries.Add(new Tuple<UInt32, UInt32>(reader.ReadUInt32(), reader.ReadUInt32()));

            Dictionary<long, string> data = new Dictionary<long, string>();

            foreach (Tuple<UInt32, UInt32> entry in entries)
            {
                reader.Seek(position + entry.Item2, SeekOrigin.Begin);
                for (int i = 0; i < entry.Item1; i++)
                {
                    byte length = reader.ReadByte();
                    string text = reader.ReadString(length);
                    long index = reader.ReadUInt32();
                    data.Add(index, text);
                }
            }

            reader.Seek(position + size, SeekOrigin.Begin); // there is no "guarentee" that the last entry will be at the end of the section, so i just do this to make sure

            return data;
        }

        public static Dictionary<int, Attributes> ParseATR1(BinaryDataReader reader)
        {
            long size = reader.ReadUInt32();
            ATR1size = size;

            if (reader.ReadBytes(8).IsOnly<byte>(0)) // padding
                throw new InvalidDataException("Offset 0x08 of ATR1 section is not empty!");

            long entriesLength = reader.ReadUInt32();

            long entryLength = reader.ReadUInt32();

            Dictionary<int, Attributes> entries = new Dictionary<int, Attributes>();

            for (int i = 0; i < entriesLength; i++)
            {
                Attributes set = new Attributes
                {
                    unk1 = reader.ReadByte(),
                    unk2 = reader.ReadByte(),
                    type = reader.ReadByte(),
                    unk3 = reader.ReadByte(),
                    unk4 = reader.ReadUInt16(),
                    unk5 = reader.ReadByte(),
                    unk6 = reader.ReadByte(),
                    unk7 = reader.ReadBytes((int)(entryLength - 8)) ?? new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00 }
                };
                
                if (set.unk2 > 2)
                {
                    Console.WriteLine($"Entry {i} in ATR1 is invalid! Skipping...");
                    //reader.Position = reader.Position - entryLength;
                    //break;
                    continue;
                }

                if(set == null)
                {
                    Console.WriteLine($"Entry {i} has an undefined attr set!");
                }

                entries.Add(i, set);
            }

            return entries;
        }

        public static Dictionary<int, MsbtString> ParseTXT2(BinaryDataReader reader)
        {
            long size = reader.ReadUInt32();

            if (reader.ReadBytes(8).IsOnly<byte>(0)) // padding
                throw new InvalidDataException("Offset 0x08 of ATR1 section is not empty!");

            long position = reader.Position;

            long entriesLength = reader.ReadUInt32();

            Dictionary<int, MsbtString> entries = new Dictionary<int, MsbtString>();

            long[] textPositions = new long[entriesLength];

            for (int i = 0; i < entriesLength; i++)
                textPositions[i] = reader.ReadUInt32() + position;


            for (int i = 0; i < entriesLength; i++)
            {
                long currentOffset = textPositions[i];
                reader.Seek(currentOffset, SeekOrigin.Begin);

                MsbtString str = new MsbtString(reader);

                entries.Add(i, str);
            }

            return entries;
        }

        public class MsbtString
        {
            uint[] Data;
            Tuple<long, uint>[] opcodes;

            public MsbtString(BinaryReader br)
            {
                List<uint> data = new List<uint>(); // opcodes make the length not always equal actual string length, so we must expect it to be less
                List<Tuple<long, uint>> opcodeList = new List<Tuple<long, uint>>();

                long start = br.BaseStream.Position;

                while (true)
                {
                    uint c = br.ReadUInt16();
                    data.Add(c);
                    if (c == 0)
                        break;
                    if (c == 0xE)
                    {
                        data.Add(br.ReadUInt16());
                        data.Add(br.ReadUInt16());
                        uint count = br.ReadUInt16();
                        opcodeList.Add(new Tuple<long, uint>(data.Count - 3, ((count + 4) / 2)));
                        data.Add(count);
                        for (var i = 0; i < count / 2; i++)
                        {
                            byte[] b = br.ReadBytes(2);
                            data.Add((uint)(b[0] + (b[1] << 8)));
                        }
                    }

                    if (br.BaseStream.Position >= br.BaseStream.Length)
                        break;
                }
                int finalLength = data.Count;
                Data = data.Take(finalLength - 1).ToArray();
                opcodes = opcodeList.ToArray();
            }

            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < Data.Length; i++)
                {
                    uint c = Data[i];
                    if (IsPartOfOpcodeContents(i))
                        builder.Append(Convert.ToChar(c));
                    else
                    {
                        if (c == 0)
                            builder.Append('\0');
                        else
                            builder.Append(Convert.ToChar(c));
                    }
                }
                return builder.ToString();
            }

            public bool IsPartOfOpcode(int offset)
            {
                foreach (Tuple<long, uint> opcode in opcodes)
                {
                    long opcodeOffset = opcode.Item1;
                    uint opcodeLength = opcode.Item2;
                    long opcodeEnd = opcodeOffset + opcodeLength;
                    if (opcodeOffset <= offset && offset < opcodeEnd)
                        return true;
                }
                return false;
            }

            public bool IsPartOfOpcodeContents(int offset)
            {
                foreach (Tuple<long, uint> opcode in opcodes)
                {
                    long opcodeOffset = opcode.Item1 + 4;
                    uint opcodeLength = opcode.Item2 - 4;
                    long opcodeEnd = opcodeOffset + opcodeLength;
                    if (opcodeOffset <= offset && offset < opcodeEnd)
                        return true;
                }
                return false;
            }
        }

        public class Entry
        {
            public string key;
            public MsbtString value;
            public Attributes attributes;

            public override string ToString()
            {
                return $"{key} : {value}";
            }
        }

        public class Attributes
        {
            public Attributes()
            {
                unk1 = 0;
                unk2 = 0;
                unk3 = 0;
                type = 0;
                unk4 = 0;
                unk5 = 0;
                unk6 = 0;
                unk7 = new byte[24 - 8];
            }

            // no one fucking knows lol
            public byte unk1, unk2, type, unk3;
            public ushort unk4;
            public byte unk5, unk6;
            public byte[] unk7;
        }
    }
}
