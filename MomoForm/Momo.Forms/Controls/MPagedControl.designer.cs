using System.Windows.Forms;
namespace Momo.Forms
{
    partial class MPagedControl
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
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.pbLast = new System.Windows.Forms.PictureBox();
            this.pbNext = new System.Windows.Forms.PictureBox();
            this.pbPrevious = new System.Windows.Forms.PictureBox();
            this.pbFirst = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPrevious)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFirst)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblPageInfo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPageInfo.Location = new System.Drawing.Point(4, 4);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(0, 17);
            this.lblPageInfo.TabIndex = 4;
            // 
            // pbLast
            // 
            this.pbLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbLast.Image = global::Momo.Forms.Properties.Resources.last_page;
            this.pbLast.Location = new System.Drawing.Point(409, 3);
            this.pbLast.Name = "pbLast";
            this.pbLast.Size = new System.Drawing.Size(18, 18);
            this.pbLast.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLast.TabIndex = 3;
            this.pbLast.TabStop = false;
            this.pbLast.Click += new System.EventHandler(this.pbLast_Click);
            // 
            // pbNext
            // 
            this.pbNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbNext.Image = global::Momo.Forms.Properties.Resources.next_page;
            this.pbNext.Location = new System.Drawing.Point(385, 3);
            this.pbNext.Name = "pbNext";
            this.pbNext.Size = new System.Drawing.Size(18, 18);
            this.pbNext.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbNext.TabIndex = 2;
            this.pbNext.TabStop = false;
            this.pbNext.Click += new System.EventHandler(this.pbNext_Click);
            // 
            // pbPrevious
            // 
            this.pbPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPrevious.Image = global::Momo.Forms.Properties.Resources.previous_page;
            this.pbPrevious.Location = new System.Drawing.Point(361, 3);
            this.pbPrevious.Name = "pbPrevious";
            this.pbPrevious.Size = new System.Drawing.Size(18, 18);
            this.pbPrevious.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPrevious.TabIndex = 1;
            this.pbPrevious.TabStop = false;
            this.pbPrevious.Click += new System.EventHandler(this.pbPrevious_Click);
            // 
            // pbFirst
            // 
            this.pbFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbFirst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbFirst.Image = global::Momo.Forms.Properties.Resources.first_page;
            this.pbFirst.Location = new System.Drawing.Point(337, 3);
            this.pbFirst.Name = "pbFirst";
            this.pbFirst.Size = new System.Drawing.Size(18, 18);
            this.pbFirst.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFirst.TabIndex = 0;
            this.pbFirst.TabStop = false;
            this.pbFirst.Click += new System.EventHandler(this.pbFirst_Click);
            // 
            // MPagedControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPageInfo);
            this.Controls.Add(this.pbLast);
            this.Controls.Add(this.pbNext);
            this.Controls.Add(this.pbPrevious);
            this.Controls.Add(this.pbFirst);
            this.Name = "MPagedControl";
            this.Size = new System.Drawing.Size(433, 24);
            ((System.ComponentModel.ISupportInitialize)(this.pbLast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPrevious)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFirst)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbFirst;
        private System.Windows.Forms.PictureBox pbPrevious;
        private System.Windows.Forms.PictureBox pbNext;
        private System.Windows.Forms.PictureBox pbLast;
        private Label lblPageInfo;

    }
}
