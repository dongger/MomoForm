using Momo.AutoUpgrade.Core;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Momo.AutoUpgrade
{
    public partial class FrmUpgrade : Form
    {
        delegate void SetTextHandler(string msg, Control ctrl);

        private MUpgrade upgrade;
        public FrmUpgrade(MUpgrade upgrade)
        {
            InitializeComponent();
            this.Text = this.lblTitle.Text = string.Format("{0}在线升级", upgrade.AppName);
            this.lblMessage.Text = "正在加载";
            this.lblProgress.Text = string.Format("（0/{0}）", upgrade.Files.Count);
            this.upgrade = upgrade;
            this.txtDescription.Text = upgrade.Description;
            VersionUpgrade.BeginUpgrade += VersionUpgrade_BeginUpgrade;
            VersionUpgrade.EndUpgrade += VersionUpgrade_EndUpgrade;
            VersionUpgrade.FileBeginDownload += VersionUpgrade_FileBeginDownload;
            VersionUpgrade.FileDownloadProgressChanged += VersionUpgrade_FileDownloadProgressChanged;
            VersionUpgrade.FileEndDownload += VersionUpgrade_FileEndDownload;
            VersionUpgrade.UpgradeError += VersionUpgrade_UpgradeError;
        }

        private void VersionUpgrade_UpgradeError(string error)
        {
            MessageBox.Show(error, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(System.Environment.ExitCode);
        }

        private void VersionUpgrade_FileEndDownload(string fileName, int index)
        {

        }

        private void VersionUpgrade_FileDownloadProgressChanged(long value, long size)
        {
            SetProgress(value, size);
        }

        private void VersionUpgrade_FileBeginDownload(string fileName, int index)
        {
            SetText(string.Format("正在下载文件：{0}", fileName), lblMessage);
            SetText(string.Format("（{0}/{1}）", index + 1, upgrade.Files.Count), lblProgress);
            SetProgress(0, 0);
        }

        private void VersionUpgrade_EndUpgrade()
        {
            try
            {
                Process.Start(Path.Combine(Application.StartupPath, upgrade.AppEntry));
            }
            finally
            {

                var formatter = new XmlSerializer(typeof(MUpgrade));

                using (var fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "version.xml"), FileMode.Create))
                {
                    formatter.Serialize(fs, upgrade);
                }

                System.Environment.Exit(System.Environment.ExitCode);
            }
        }

        private void SetProgress(long value, long size)
        {
            if (pbProgress.InvokeRequired)
            {
                pbProgress.Invoke(new FileDownloadProgressChangedHandler(SetProgress), value, size);
                return;
            }

            pbProgress.Value = (int)value;
            pbProgress.Total = size > 0 ? (int)size : 100;
        }

        private void SetText(string msg, Control ctrl)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(new SetTextHandler(SetText), msg, ctrl);
                return;
            }

            ctrl.Text = msg;
        }

        private void VersionUpgrade_BeginUpgrade()
        {
            SetText("开始更新", lblMessage);
        }

        private void pbClose_MouseEnter(object sender, EventArgs e)
        {
            pbClose.BackColor = Color.LightGreen;
        }

        private void pbClose_MouseLeave(object sender, EventArgs e)
        {
            pbClose.BackColor = Color.Transparent;
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

            Win32.ReleaseCapture();
            //*********************调用移动无窗体控件函数  
            Win32.SendMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MOVE + Win32.HTCAPTION, 0);
        }

        private void FrmUpgrade_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            VersionUpgrade.Upgrade(this.upgrade);
        }
    }
}
