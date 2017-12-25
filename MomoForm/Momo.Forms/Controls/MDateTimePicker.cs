using System;
using System.ComponentModel;
using System.Drawing;

namespace Momo.Forms
{
    public partial class MDateTimePicker : MBorderControl
    {
        public MDateTimePicker()
        {
            InitializeComponent();

        }

        [Browsable(true), Description("Value"), Category("Momo")]
        public DateTime Value { get { return this.dateTimePicker1.Value; } set { this.dateTimePicker1.Value = value; } }
        [Browsable(true), Description("MaxDate"), Category("Momo")]
        public DateTime MaxDate { get { return this.dateTimePicker1.MaxDate; } set { this.dateTimePicker1.MaxDate = value; } }
        [Browsable(true), Description("MinDate"), Category("Momo")]
        public DateTime MinDate { get { return this.dateTimePicker1.MinDate; } set { this.dateTimePicker1.MinDate = value; } }
        [DefaultValue("")]
        [Browsable(true), Description("CustomFormat"), Category("Momo")]
        public string CustomFormat
        {
            get { return this.dateTimePicker1.CustomFormat; }
            set
            {
                this.dateTimePicker1.CustomFormat = value;
                if (!string.IsNullOrEmpty(value))
                {
                    this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                }
                else
                {
                    this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Long;
                }
                //this.dateTimePicker1.Invalidate();
            }
        }

        [Browsable(true), Description("Text"), Category("Momo")]
        public string Title
        {
            get { return lblTitle.Text; }
            set
            {
                lblTitle.Text = value;
                this.panel1.Location = new Point(this.lblTitle.Width + 9, (this.Height - this.panel1.Height) / 2);
                this.panel1.Width = this.Width - this.panel1.Location.X - 2;
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.panel1.Location = new Point(this.lblTitle.Width + 9, (this.Height - this.panel1.Height) / 2);
            this.panel1.Width = this.Width - this.panel1.Location.X - 2;
            this.lblTitle.Location = new Point(3, (this.Height - this.lblTitle.Height) / 2);
        }
    }
}
