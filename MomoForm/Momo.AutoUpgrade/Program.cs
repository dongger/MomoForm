using Momo.AutoUpgrade.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Momo.AutoUpgrade
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string path = Path.Combine(Application.StartupPath, "upgrade.xml");

            try
            {
                MUpgrade upgrade = null;
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(MUpgrade));
                    object obj = formatter.Deserialize(fs);
                    upgrade = obj as MUpgrade;
                }

                Application.Run(new FrmUpgrade(upgrade));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
