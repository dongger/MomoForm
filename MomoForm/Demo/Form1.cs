using Momo.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.mBanner1.AddImage(Properties.Resources._1454689047513);
            //this.mBanner1.AddImage(Properties.Resources._1454689059779);
            //this.mBanner1.AddImage(Properties.Resources._1454689085322);
            //this.mBanner1.AddImage(Properties.Resources._1454689102185);
            //this.mBanner1.Start();
            //mTriangleMarkPanel1.TriangleMarkMode = TriangleMarkMode.Top | TriangleMarkMode.Left;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //new MPopup(this.mBanner1).Show(button1);
            //Popup.Show(button1, this.mBanner1, Size.Empty);

        }


        private void mButton1_Paint(object sender, PaintEventArgs e)
        {
            //var img = new Bitmap(mButton1.Width, mButton1.Height);
            //mButton1.DrawToBitmap(img, e.ClipRectangle);
            //pictureBox1.Image = img;
        }

        private void mLabel1_Click(object sender, EventArgs e)
        {
            //var img = new Bitmap(mButton1.Width, mButton1.Height);
            //mButton1.DrawToBitmap(img, mButton1.ClientRectangle);
            //pictureBox1.Image = img;
        }

        bool flag = false;
        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBoxEx.Show("我是超长的提示文本我是超长的提示文本我是超长的提示文本我是超长的提示文本我是超长的提示文本");
        }
    }
}
