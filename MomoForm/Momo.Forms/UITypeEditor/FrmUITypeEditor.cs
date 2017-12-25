using System;
using System.Windows.Forms;

namespace Momo.Forms
{
    public partial class FrmUITypeEditor : Form
    {
        public FrmUITypeEditor(string title, object value)
        {
            InitializeComponent();

            this.value = value;
            this.propertyGrid1.SelectedObject = this.value;
            this.Text = title;
        }

        private object value;
        public object Value { get { return this.value; } }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
