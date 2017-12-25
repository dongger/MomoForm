namespace Momo.Forms.Controls
{
    partial class MPathChoice
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.borderPanel1 = new Momo.Forms.MPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWater = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.borderPanel1.SuspendLayout();
            this.SuspendLayout();
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
            this.borderPanel1.Controls.Add(this.button1);
            this.borderPanel1.Controls.Add(this.label1);
            this.borderPanel1.Controls.Add(this.lblWater);
            this.borderPanel1.Controls.Add(this.textBox1);
            this.borderPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borderPanel1.LinearGradientMode = Momo.Forms.GradientMode.Horizontal;
            this.borderPanel1.Location = new System.Drawing.Point(0, 0);
            this.borderPanel1.Name = "borderPanel1";
            this.borderPanel1.Size = new System.Drawing.Size(295, 32);
            this.borderPanel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(233, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "选择...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "标题";
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
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(47, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(180, 16);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // MFileChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.borderPanel1);
            this.Name = "MFileChoice";
            this.Size = new System.Drawing.Size(295, 32);
            this.borderPanel1.ResumeLayout(false);
            this.borderPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MPanel borderPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWater;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}
