namespace Momo.Forms
{
    partial class MessageBoxForm
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
            Momo.Forms.GradientColor gradientColor1 = new Momo.Forms.GradientColor();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBoxForm));
            Momo.Forms.GradientColor gradientColor2 = new Momo.Forms.GradientColor();
            Momo.Forms.GradientColor gradientColor3 = new Momo.Forms.GradientColor();
            Momo.Forms.GradientColor gradientColor4 = new Momo.Forms.GradientColor();
            this.btnOK = new Momo.Forms.MButton();
            this.btnCancel = new Momo.Forms.MButton();
            this.btnRetry = new Momo.Forms.MButton();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BackgroundColor.FromColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.btnOK.BackgroundColor.GradientMode = Momo.Forms.GradientMode.Vertical;
            this.btnOK.BackgroundColor.ToColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(160)))), ((int)(((byte)(133)))));
            this.btnOK.BorderColor = System.Drawing.Color.Empty;
            this.btnOK.BorderPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.btnOK.BorderWidth = 1;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.HoverBackColor.FromColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnOK.HoverBackColor.GradientMode = Momo.Forms.GradientMode.None;
            this.btnOK.HoverBackColor.ToColor = System.Drawing.Color.Empty;
            this.btnOK.Image = null;
            this.btnOK.ImageSize = new System.Drawing.Size(16, 16);
            this.btnOK.Location = new System.Drawing.Point(19, 102);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Radius = 0;
            this.btnOK.RadiusMode = Momo.Forms.RadiusMode.None;
            this.btnOK.Size = new System.Drawing.Size(100, 32);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.TextImageRelation = Momo.Forms.ImageTextRelation.Overlay;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundColor.FromColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.btnCancel.BackgroundColor.GradientMode = Momo.Forms.GradientMode.Vertical;
            this.btnCancel.BackgroundColor.ToColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(160)))), ((int)(((byte)(133)))));
            this.btnCancel.BorderColor = System.Drawing.Color.Empty;
            this.btnCancel.BorderPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.btnCancel.BorderWidth = 1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnCancel.HoverBackColor.FromColor = System.Drawing.Color.White;
            this.btnCancel.HoverBackColor.GradientMode = Momo.Forms.GradientMode.None;
            this.btnCancel.HoverBackColor.ToColor = System.Drawing.Color.White;
            this.btnCancel.Image = null;
            this.btnCancel.ImageSize = new System.Drawing.Size(16, 16);
            this.btnCancel.Location = new System.Drawing.Point(127, 102);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Radius = 0;
            this.btnCancel.RadiusMode = Momo.Forms.RadiusMode.None;
            this.btnCancel.Size = new System.Drawing.Size(100, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.TextImageRelation = Momo.Forms.ImageTextRelation.Overlay;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRetry
            // 
            this.btnRetry.BackColor = System.Drawing.Color.Transparent;
            this.btnRetry.BackgroundColor.FromColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.btnRetry.BackgroundColor.GradientMode = Momo.Forms.GradientMode.Vertical;
            this.btnRetry.BackgroundColor.ToColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(160)))), ((int)(((byte)(133)))));
            this.btnRetry.BorderColor = System.Drawing.Color.Empty;
            this.btnRetry.BorderPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.btnRetry.BorderWidth = 1;
            this.btnRetry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRetry.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRetry.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRetry.ForeColor = System.Drawing.Color.White;
            this.btnRetry.HoverBackColor.FromColor = System.Drawing.Color.White;
            this.btnRetry.HoverBackColor.GradientMode = Momo.Forms.GradientMode.None;
            this.btnRetry.HoverBackColor.ToColor = System.Drawing.Color.White;
            this.btnRetry.Image = null;
            this.btnRetry.ImageSize = new System.Drawing.Size(16, 16);
            this.btnRetry.Location = new System.Drawing.Point(235, 102);
            this.btnRetry.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Radius = 0;
            this.btnRetry.RadiusMode = Momo.Forms.RadiusMode.None;
            this.btnRetry.Size = new System.Drawing.Size(100, 32);
            this.btnRetry.TabIndex = 4;
            this.btnRetry.Text = "重试";
            this.btnRetry.TextImageRelation = Momo.Forms.ImageTextRelation.Overlay;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // MessageBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderWidth = 0;
            gradientColor1.FromColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            gradientColor1.GradientMode = Momo.Forms.GradientMode.Vertical;
            gradientColor1.ToColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.Caption.BackColor = gradientColor1;
            this.Caption.BackImage = null;
            this.Caption.CenterCaption = false;
            this.Caption.CenterControlBox = false;
            this.Caption.CenterTitle = false;
            this.Caption.CloseButtonImage = ((System.Drawing.Image)(resources.GetObject("resource.CloseButtonImage")));
            gradientColor2.FromColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            gradientColor2.GradientMode = Momo.Forms.GradientMode.None;
            gradientColor2.ToColor = System.Drawing.Color.Empty;
            this.Caption.ControlActivedColor = gradientColor2;
            gradientColor3.FromColor = System.Drawing.Color.Transparent;
            gradientColor3.GradientMode = Momo.Forms.GradientMode.None;
            gradientColor3.ToColor = System.Drawing.Color.Empty;
            this.Caption.ControlBackColor = gradientColor3;
            this.Caption.ControlBox = true;
            this.Caption.ControlBoxSize = new System.Drawing.Size(24, 24);
            this.Caption.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Caption.ForeColor = System.Drawing.Color.Black;
            gradientColor4.FromColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(57)))), ((int)(((byte)(198)))), ((int)(((byte)(131)))));
            gradientColor4.GradientMode = Momo.Forms.GradientMode.None;
            gradientColor4.ToColor = System.Drawing.Color.Empty;
            this.Caption.FullButtonBackColor = gradientColor4;
            this.Caption.Height = 40;
            this.Caption.HelpButton = false;
            this.Caption.HelpButtonImage = ((System.Drawing.Image)(resources.GetObject("resource.HelpButtonImage")));
            this.Caption.ImageList = null;
            this.Caption.Location = new System.Drawing.Point(0, 0);
            this.Caption.Logo = null;
            this.Caption.LogoSize = new System.Drawing.Size(24, 24);
            this.Caption.LogoVisible = true;
            this.Caption.MaximizeBox = false;
            this.Caption.MaximizeBoxImage = ((System.Drawing.Image)(resources.GetObject("resource.MaximizeBoxImage")));
            this.Caption.MinimizeBox = false;
            this.Caption.MinimizeBoxImage = ((System.Drawing.Image)(resources.GetObject("resource.MinimizeBoxImage")));
            this.Caption.NormalBoxImage = ((System.Drawing.Image)(resources.GetObject("resource.NormalBoxImage")));
            this.Caption.Padding = new System.Windows.Forms.Padding(4);
            this.Caption.Rectangle = new System.Drawing.Rectangle(0, 0, 300, 40);
            this.Caption.Size = new System.Drawing.Size(300, 40);
            this.Caption.Text = "Momo Form";
            this.Caption.Visible = true;
            this.Caption.Width = 300;
            this.Caption.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Caption.X = 0;
            this.Caption.Y = 0;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.btnRetry);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBoxForm";
            this.ShadowBorder = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MessageBoxForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
        private MButton btnOK;
        private MButton btnCancel;
        private MButton btnRetry;
    }
}