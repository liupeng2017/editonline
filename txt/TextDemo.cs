using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TextDemo
{
    public partial class Form1 : Form
    {
        string path = System.Windows.Forms.Application.StartupPath+"\\ak.txt";
        public Form1()
        {
            InitializeComponent();
        }
        public void Write( string path , string txt)
        {
            StreamWriter wrt = new StreamWriter(path,true);
            wrt.WriteLine(txt);
            wrt.Close();//关闭文件
        }
        public string  Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            string result=string.Empty;
            
            while ((line = sr.ReadLine()) != null)
            {
                result += line.ToString()+"\r\n";
                Console.WriteLine(line.ToString());
            }
            sr.Close();
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                return;
            }
            else
            {
                Write(path, textBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Directory.Exists   是判读文件夹是否存在
            if (File.Exists(path))//判断是否存在
            {
               string txt =  Read(path);
               textBox2.Text = txt;
            }
        }
    }
}
