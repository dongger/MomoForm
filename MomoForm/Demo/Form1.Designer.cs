namespace Demo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.mCaptionPanel1 = new Momo.Forms.MCaptionPanel();
            this.mSideBar1 = new Momo.Forms.MSideBar();
            this.mSideBarMenu1 = new Momo.Forms.MSideBarMenu();
            this.mSideBarMenu2 = new Momo.Forms.MSideBarMenu();
            this.mSideBarMenu3 = new Momo.Forms.MSideBarMenu();
            this.mSideBarMenu4 = new Momo.Forms.MSideBarMenu();
            this.mSideBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(412, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(545, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // mCaptionPanel1
            // 
            this.mCaptionPanel1.BackColorGradint = System.Drawing.Color.Empty;
            this.mCaptionPanel1.BorderBottomColor = System.Drawing.Color.Empty;
            this.mCaptionPanel1.BorderColor = System.Drawing.Color.Empty;
            this.mCaptionPanel1.BorderLeftColor = System.Drawing.Color.Empty;
            this.mCaptionPanel1.BorderRightColor = System.Drawing.Color.Empty;
            this.mCaptionPanel1.BorderTopColor = System.Drawing.Color.Empty;
            this.mCaptionPanel1.CaptionBackColorGradint = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.mCaptionPanel1.CaptionFont = null;
            this.mCaptionPanel1.CaptionLocation = Momo.Forms.CaptionLocation.Top;
            this.mCaptionPanel1.LinearGradientMode = Momo.Forms.GradientMode.Horizontal;
            this.mCaptionPanel1.Location = new System.Drawing.Point(422, 117);
            this.mCaptionPanel1.Name = "mCaptionPanel1";
            this.mCaptionPanel1.Padding = new System.Windows.Forms.Padding(0);
            this.mCaptionPanel1.Radius = 20;
            this.mCaptionPanel1.RadiusMode = Momo.Forms.RadiusMode.TopLeft;
            this.mCaptionPanel1.Size = new System.Drawing.Size(362, 342);
            this.mCaptionPanel1.TabIndex = 5;
            this.mCaptionPanel1.Title = "带标题面板";
            // 
            // mSideBar1
            // 
            this.mSideBar1.Controls.Add(this.mSideBarMenu1);
            this.mSideBar1.Controls.Add(this.mSideBarMenu2);
            this.mSideBar1.Controls.Add(this.mSideBarMenu3);
            this.mSideBar1.Controls.Add(this.mSideBarMenu4);
            this.mSideBar1.Location = new System.Drawing.Point(77, 100);
            this.mSideBar1.Name = "mSideBar1";
            this.mSideBar1.SelectedIndex = 0;
            this.mSideBar1.SelectedMenu = this.mSideBarMenu4;
            this.mSideBar1.Size = new System.Drawing.Size(296, 363);
            this.mSideBar1.TabIndex = 6;
            this.mSideBar1.Title = "";
            this.mSideBar1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.mSideBar1.TitleForeColor = System.Drawing.Color.White;
            // 
            // mSideBarMenu1
            // 
            this.mSideBarMenu1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.mSideBarMenu1.BorderBottomColor = System.Drawing.Color.Empty;
            this.mSideBarMenu1.BorderBottomStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu1.BorderBottomWidth = 0;
            this.mSideBarMenu1.BorderLeftColor = System.Drawing.Color.Empty;
            this.mSideBarMenu1.BorderLeftStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu1.BorderLeftWidth = 0;
            this.mSideBarMenu1.BorderRightColor = System.Drawing.Color.Empty;
            this.mSideBarMenu1.BorderRightStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu1.BorderRightWidth = 0;
            this.mSideBarMenu1.BorderTopColor = System.Drawing.Color.Empty;
            this.mSideBarMenu1.BorderTopStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu1.BorderTopWidth = 0;
            this.mSideBarMenu1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mSideBarMenu1.Image = null;
            this.mSideBarMenu1.Location = new System.Drawing.Point(0, 40);
            this.mSideBarMenu1.Name = "mSideBarMenu1";
            this.mSideBarMenu1.Selected = false;
            this.mSideBarMenu1.Size = new System.Drawing.Size(296, 40);
            this.mSideBarMenu1.TabIndex = 1;
            this.mSideBarMenu1.Title = "SideBarMenu";
            // 
            // mSideBarMenu2
            // 
            this.mSideBarMenu2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.mSideBarMenu2.BorderBottomColor = System.Drawing.Color.Empty;
            this.mSideBarMenu2.BorderBottomStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu2.BorderBottomWidth = 0;
            this.mSideBarMenu2.BorderLeftColor = System.Drawing.Color.Empty;
            this.mSideBarMenu2.BorderLeftStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu2.BorderLeftWidth = 0;
            this.mSideBarMenu2.BorderRightColor = System.Drawing.Color.Empty;
            this.mSideBarMenu2.BorderRightStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu2.BorderRightWidth = 0;
            this.mSideBarMenu2.BorderTopColor = System.Drawing.Color.Empty;
            this.mSideBarMenu2.BorderTopStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu2.BorderTopWidth = 0;
            this.mSideBarMenu2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mSideBarMenu2.Image = null;
            this.mSideBarMenu2.Location = new System.Drawing.Point(0, 80);
            this.mSideBarMenu2.Name = "mSideBarMenu2";
            this.mSideBarMenu2.Selected = false;
            this.mSideBarMenu2.Size = new System.Drawing.Size(296, 40);
            this.mSideBarMenu2.TabIndex = 2;
            this.mSideBarMenu2.Title = "SideBarMenu";
            // 
            // mSideBarMenu3
            // 
            this.mSideBarMenu3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.mSideBarMenu3.BorderBottomColor = System.Drawing.Color.Empty;
            this.mSideBarMenu3.BorderBottomStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu3.BorderBottomWidth = 0;
            this.mSideBarMenu3.BorderLeftColor = System.Drawing.Color.Empty;
            this.mSideBarMenu3.BorderLeftStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu3.BorderLeftWidth = 0;
            this.mSideBarMenu3.BorderRightColor = System.Drawing.Color.Empty;
            this.mSideBarMenu3.BorderRightStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu3.BorderRightWidth = 0;
            this.mSideBarMenu3.BorderTopColor = System.Drawing.Color.Empty;
            this.mSideBarMenu3.BorderTopStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu3.BorderTopWidth = 0;
            this.mSideBarMenu3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mSideBarMenu3.Image = null;
            this.mSideBarMenu3.Location = new System.Drawing.Point(0, 120);
            this.mSideBarMenu3.Name = "mSideBarMenu3";
            this.mSideBarMenu3.Selected = false;
            this.mSideBarMenu3.Size = new System.Drawing.Size(296, 40);
            this.mSideBarMenu3.TabIndex = 3;
            this.mSideBarMenu3.Title = "SideBarMenu";
            // 
            // mSideBarMenu4
            // 
            this.mSideBarMenu4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.mSideBarMenu4.BorderBottomColor = System.Drawing.Color.Empty;
            this.mSideBarMenu4.BorderBottomStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu4.BorderBottomWidth = 0;
            this.mSideBarMenu4.BorderLeftColor = System.Drawing.Color.Empty;
            this.mSideBarMenu4.BorderLeftStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu4.BorderLeftWidth = 0;
            this.mSideBarMenu4.BorderRightColor = System.Drawing.Color.Empty;
            this.mSideBarMenu4.BorderRightStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu4.BorderRightWidth = 0;
            this.mSideBarMenu4.BorderTopColor = System.Drawing.Color.Empty;
            this.mSideBarMenu4.BorderTopStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.mSideBarMenu4.BorderTopWidth = 0;
            this.mSideBarMenu4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mSideBarMenu4.Image = null;
            this.mSideBarMenu4.Location = new System.Drawing.Point(0, 160);
            this.mSideBarMenu4.Name = "mSideBarMenu4";
            this.mSideBarMenu4.Selected = false;
            this.mSideBarMenu4.Size = new System.Drawing.Size(296, 40);
            this.mSideBarMenu4.TabIndex = 4;
            this.mSideBarMenu4.Title = "SideBarMenu";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 644);
            this.Controls.Add(this.mSideBar1);
            this.Controls.Add(this.mCaptionPanel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.mSideBar1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Momo.Forms.MCaptionPanel mCaptionPanel1;
        private Momo.Forms.MSideBar mSideBar1;
        private Momo.Forms.MSideBarMenu mSideBarMenu1;
        private Momo.Forms.MSideBarMenu mSideBarMenu2;
        private Momo.Forms.MSideBarMenu mSideBarMenu3;
        private Momo.Forms.MSideBarMenu mSideBarMenu4;
    }
}