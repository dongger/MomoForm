using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace Momo.AutoUpgrade.Core
{
    public static class VersionCheck
    {
        public static bool Check(string url, out MUpgrade upgrade)
        {
            try
            {
                var request = new WebClient();
                request.Encoding = Encoding.UTF8;
                var content = request.DownloadString(url);
                var formatter = new XmlSerializer(typeof(MUpgrade));
                using (var reader = new StringReader(content))
                {
                    object obj = formatter.Deserialize(reader);
                    upgrade = obj as MUpgrade;
                }

                using (var fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "upgrade.xml"), FileMode.Create))
                {
                    formatter.Serialize(fs, upgrade);
                }
            }
            catch
            {
                upgrade = null;
            }

            return upgrade != null;
        }
    }
}
