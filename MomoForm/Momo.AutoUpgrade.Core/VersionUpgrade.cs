using System;
using System.IO;
using System.Net;

namespace Momo.AutoUpgrade.Core
{
    public delegate void BeginUpgradeHandler();
    public delegate void EndUpgradeHandler();
    public delegate void FileBeginDownloadHandler(string fileName, int index);
    public delegate void FileDownloadProgressChangedHandler(long value, long size);
    public delegate void FileEndDownloadHandler(string fileName, int index);
    public delegate void UpgradeErrorHandler(string error);

    public static class VersionUpgrade
    {
        public static event BeginUpgradeHandler BeginUpgrade;
        public static event EndUpgradeHandler EndUpgrade;
        public static event FileBeginDownloadHandler FileBeginDownload;
        public static event FileDownloadProgressChangedHandler FileDownloadProgressChanged;
        public static event FileEndDownloadHandler FileEndDownload;
        public static event UpgradeErrorHandler UpgradeError;

        private static void RaiseBeginUpgrade()
        {
            try
            {
                BeginUpgrade?.Invoke();
            }
            catch { }
        }
        private static void RaiseEndUpgrade()
        {
            try
            {
                EndUpgrade?.Invoke();
            }
            catch { }
        }
        private static void RaiseFileBeginDownload(string fileName, int index)
        {
            try
            {
                FileBeginDownload?.Invoke(fileName, index);
            }
            catch { }
        }
        private static void RaiseFileDownloadProgressChanged(long value, long size)
        {
            try
            {
                FileDownloadProgressChanged?.Invoke(value, size);
            }
            catch { }
        }
        private static void RaiseFileEndDownload(string fileName, int index)
        {
            try
            {
                FileEndDownload?.Invoke(fileName, index);
            }
            catch { }
        }
        private static void RaiseUpgradeError(string error)
        {
            try
            {
                UpgradeError?.Invoke(error);
            }
            catch { }
        }

        private static void Down(string baseUrl, MFile file)
        {
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.Path);
            var filePath = Path.Combine(directory, file.Name);
            var url = baseUrl;
            if (string.IsNullOrEmpty(file.Path))
            {
                url = string.Format("{0}/{1}", baseUrl, file.Name);
            }
            else
            {
                url = string.Format("{0}/{1}/{2}", baseUrl, file.Path, file.Name);
            }

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // 文件直接覆盖原有的
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                // 记录已完成的大小 
                int completedLength = 0;
                // 文件总大小
                long fileLength = 0;

                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(url);

                //向服务器请求，获得服务器的回应数据流
                HttpWebResponse webResponse = (HttpWebResponse)myRequest.GetResponse();
                fileLength = webResponse.ContentLength;
                using (Stream myStream = webResponse.GetResponseStream())
                {
                    byte[] btContent = new byte[1024];
                    // 下载的位置
                    int position = 0;
                    while ((position = myStream.Read(btContent, 0, 1024)) > 0)
                    {
                        fs.Write(btContent, 0, position);
                        completedLength += position;
                        RaiseFileDownloadProgressChanged(completedLength, fileLength);
                    }
                }
            }
        }

        /// <summary>
        /// 升级
        /// </summary>
        /// <param name="currentVersion">当前版本号</param>
        /// <param name="newVersion">新的版本号</param>
        /// <returns></returns>
        public static void Upgrade(MUpgrade upgrade)
        {
            if (upgrade == null || upgrade.Files == null || upgrade.Files.Count == 0)
            {
                return;
            }

            RaiseBeginUpgrade();
            var index = 0;

            foreach (var file in upgrade.Files)
            {
                RaiseFileBeginDownload(file.Name, index);
                try
                {
                    Down(upgrade.BaseUrl, file);
                    RaiseFileEndDownload(file.Name, index);
                    index++;
                }
                catch (Exception ex)
                {
                    RaiseUpgradeError(ex.Message);
                    return;
                }
            }

            RaiseEndUpgrade();
        }
    }
}
