using System;
using System.Windows.Forms;

namespace Momo.Forms
{
    public partial class FrmCaptionControlButtonEditor<T> : Form where T : CaptionControlButton, new()
    {
        public FrmCaptionControlButtonEditor(object value)
        {
            InitializeComponent();

            this.value = value as CaptionControlButtonCollection<T>;
            this.Bind();
        }

        private void Bind()
        {
            this.lvList.Clear();
            foreach (var item in this.value)
            {
                var lvi = new ListViewItem();
                lvi.Text = item.Name;
                lvi.Tag = item;
                this.lvList.Items.Add(lvi);
            }
        }

        private CaptionControlButtonCollection<T> value;
        public CaptionControlButtonCollection<T> Value { get { return this.value; } }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void picUp_Click(object sender, EventArgs e)
        {
            var index = this.SelectIndex;
            var item = this.SelectItem as T;
            var temp = this.value[index - 1];
            this.value[index - 1] = item;
            this.value[index] = temp;
            Bind();
        }

        private void picDown_Click(object sender, EventArgs e)
        {
            var index = this.SelectIndex;
            var item = this.SelectItem as T;
            var temp = this.value[index + 1];
            this.value[index + 1] = item;
            this.value[index] = temp;
            Bind();
        }

        private void picAdd_Click(object sender, EventArgs e)
        {
            this.value.Add(new T() { Name = "按钮" });
            this.Bind();
        }

        private void picDelete_Click(object sender, EventArgs e)
        {
            this.value.RemoveAt(this.SelectIndex);
            this.Bind();
        }

        private int SelectIndex
        {
            get
            {
                if (lvList.SelectedItems == null || lvList.SelectedItems.Count != 1)
                {
                    return -1;
                }

                return lvList.SelectedIndices[0];
            }
        }

        private object SelectItem
        {
            get
            {
                if (lvList.SelectedItems == null || lvList.SelectedItems.Count != 1)
                {
                    return null;
                }

                return lvList.SelectedItems[0].Tag;
            }
        }

        private void lvList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = this.SelectItem;
            if (item == null)
            {
                picDelete.Visible = picDown.Visible = picUp.Visible = false;
                return;
            }

            var index = this.SelectIndex;
            this.propertyGrid1.SelectedObject = item;
            picUp.Visible = index > 0;
            picDown.Visible = index < value.Count;
            picDelete.Visible = true;
        }

        private void propertyGrid1_Leave(object sender, EventArgs e)
        {
            this.Bind();
        }
    }
}
