
using Newtonsoft.Json;
using Syroot.BinaryData;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSBTEditor
{
    public partial class Form1 : Form
    {
        Log log = new Log();
        public Form1()
        {
            InitializeComponent();
        }
        public ByteOrder endianness;
        bool txtChange = false;
        MSBT currMSBT;
        string oldItem;
        private void OpenFile_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var bytes = File.ReadAllBytes(openFileDialog1.FileName);
            currMSBT = new MSBT(bytes, log);
            msgList.Items.Clear();
            if (currMSBT.failed)
            {
                return;
            }
            foreach (var entry in currMSBT.entries)
            {
                msgList.Items.Add(entry.Value.key);
            }
            msgList.SelectedIndex = 0;
        }

        private void txtChanged(object sender, KeyEventArgs e)
        {
            txtChange = true;
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            if(currMSBT == null)
            {
                MessageBox.Show("You don't have a file opened");
                return;
            }
            if (!SaveCurrentChanges())
                return;
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            //Weird overwrite problems
            File.Delete(saveFileDialog1.FileName);
            var file = File.OpenWrite(saveFileDialog1.FileName);
            currMSBT.Write(file);
            file.Close();
        }

        private void MsgList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(msgList.SelectedIndex != -1)
            {
                var item = (string)msgList.SelectedItem;
                if (item == oldItem) return;
                if (!SaveCurrentChanges(true))
                    return;

                //Change visible item
                var filter = currMSBT.entries.Values.Where(x => x.key == item);
                if (filter.Count() > 0)
                {
                    var entry = filter.First();
                    //Show msg

                    var msg = entry.value.ToString();
                    //https://stackoverflow.com/a/31056/10211613
                    msg = Regex.Replace(msg, "(?<!\r)\n", "\r\n");
                    textBox1.Text = msg;

                    //Attributes

                    txtType.Text = entry.attributes.type.ToString();
                    txtU1.Text = entry.attributes.unk1.ToString();
                    txtU2.Text = entry.attributes.unk2.ToString();
                    txtU3.Text = entry.attributes.unk3.ToString();
                    txtU4.Text = entry.attributes.unk4.ToString();
                    txtU5.Text = entry.attributes.unk5.ToString();
                    txtU6.Text = entry.attributes.unk6.ToString();
                    txtU7.Text = BitConverter.ToString(entry.attributes.unk7);
                }
                oldItem = item;
            }
        }

        public bool SaveCurrentChanges(bool change = false)
        {
            var item = (string)msgList.SelectedItem;
            bool verify;
            if (textBox1.Text == "")
            {
                verify = true;
            }
            else
            {
                verify = VerifyInput();
            }
            if (txtChange && oldItem != null && verify)
            {
                txtChange = false;
                var filtered = currMSBT.entries.Values.Where(x => x.key == oldItem);
                if (filtered.Count() > 0)
                {
                    var entry = filtered.First();
                    //Set attributes

                    entry.attributes.type = byte.Parse(txtType.Text);
                    entry.attributes.unk1 = byte.Parse(txtU1.Text);
                    entry.attributes.unk2 = byte.Parse(txtU2.Text);
                    entry.attributes.unk3 = byte.Parse(txtU3.Text);
                    entry.attributes.unk4 = ushort.Parse(txtU4.Text);
                    entry.attributes.unk5 = byte.Parse(txtU5.Text);
                    entry.attributes.unk6 = byte.Parse(txtU6.Text);
                }
            }
            if (!verify && change)
            {
                if (MessageBox.Show("Verifing input failed, do you change the active item anyway?\nWarning! Your changes to this item will be lost!", "", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    msgList.SelectedItem = oldItem;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return verify;
        }

        private void OpenLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Show();
            log.TopMost = true;
            log.TopMost = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtNames.Add("txtType", "type");
            txtNames.Add("txtU1", "Unknown 1");
            txtNames.Add("txtU2", "Unknown 2");
            txtNames.Add("txtU3", "Unknown 3");
            txtNames.Add("txtU4", "Unknown 4");
            txtNames.Add("txtU5", "Unknown 5");
            txtNames.Add("txtU6", "Unknown 6");
            log.newLog("[System] Loaded main form");
        }

        bool VerifyInput()
        {
            var byteTextBoxes = new TextBox[] { txtType, txtU1, txtU2, txtU3, txtU5, txtU6 };
            foreach (TextBox boxToVerify in byteTextBoxes)
            {
                if (!byte.TryParse(boxToVerify.Text, out byte result))
                {
                    MessageBox.Show("Incorrect input at " + txtNames[boxToVerify.Name]);
                    return false;
                }
            }
            var U16TextBoxes = new TextBox[] { txtU4 };
            foreach (TextBox boxToVerify in U16TextBoxes)
            {
                if (!ushort.TryParse(boxToVerify.Text, out ushort result))
                {
                    MessageBox.Show("Incorrect input at " + txtNames[boxToVerify.Name]);
                    return false;
                }
            }
            return true;
        }
        Dictionary<string, string> txtNames = new Dictionary<string, string>();
    }
}
