namespace Momo.Forms
{
    partial class MTextBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtText = new System.Windows.Forms.TextBox();
            this.borderPanel1 = new Momo.Forms.MPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWater = new System.Windows.Forms.Label();
            this.borderPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.txtText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtText.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtText.Location = new System.Drawing.Point(47, 8);
            this.txtText.Name = "textBox1";
            this.txtText.Size = new System.Drawing.Size(100, 16);
            this.txtText.TabIndex = 0;
            this.txtText.Click += new System.EventHandler(this.textBox1_Click);
            this.txtText.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtText.Enter += new System.EventHandler(this.textBox1_Enter);
            this.txtText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            this.txtText.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // borderPanel1
            // 
            this.borderPanel1.BackColor = System.Drawing.Color.White;
            this.borderPanel1.BackColorGradint = System.Drawing.Color.Empty;
            this.borderPanel1.BorderBottomColor = System.Drawing.Color.Empty;
            this.borderPanel1.BorderBottomWidth = 1;
            this.borderPanel1.BorderColor = System.Drawing.Color.Empty;
            this.borderPanel1.BorderLeftColor = System.Drawing.Color.Empty;
            this.borderPanel1.BorderLeftWidth = 1;
            this.borderPanel1.BorderRightColor = System.Drawing.Color.Empty;
            this.borderPanel1.BorderRightWidth = 1;
            this.borderPanel1.BorderTopColor = System.Drawing.Color.Empty;
            this.borderPanel1.BorderTopWidth = 1;
            this.borderPanel1.BorderWidth = 1;
            this.borderPanel1.Controls.Add(this.lblTitle);
            this.borderPanel1.Controls.Add(this.lblWater);
            this.borderPanel1.Controls.Add(this.txtText);
            this.borderPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borderPanel1.LinearGradientMode = Momo.Forms.GradientMode.Horizontal;
            this.borderPanel1.Location = new System.Drawing.Point(0, 0);
            this.borderPanel1.Name = "borderPanel1";
            this.borderPanel1.Size = new System.Drawing.Size(150, 32);
            this.borderPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 8);
            this.lblTitle.Name = "label1";
            this.lblTitle.Size = new System.Drawing.Size(32, 17);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "标题";
            this.lblTitle.Click += new System.EventHandler(this.lblWater_Click);
            // 
            // lblWater
            // 
            this.lblWater.AutoSize = true;
            this.lblWater.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblWater.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWater.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.lblWater.Location = new System.Drawing.Point(44, 8);
            this.lblWater.Name = "lblWater";
            this.lblWater.Size = new System.Drawing.Size(56, 17);
            this.lblWater.TabIndex = 1;
            this.lblWater.Text = "水印文字";
            this.lblWater.Click += new System.EventHandler(this.lblWater_Click);
            // 
            // MTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.borderPanel1);
            this.Name = "MTextBox";
            this.Size = new System.Drawing.Size(150, 32);
            this.borderPanel1.ResumeLayout(false);
            this.borderPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtText;
        private MPanel borderPanel1;
        private System.Windows.Forms.Label lblWater;
        private System.Windows.Forms.Label lblTitle;
    }
}
