using System;
using System.Collections.Generic;

namespace Momo.AutoUpgrade.Core
{
    /// <summary>
    /// 升级包信息
    /// </summary>
    [Serializable]
    public sealed class MUpgrade
    {
        /// <summary>
        /// 应用程序启动文件，必须在根目录
        /// </summary>
        public string AppEntry { get; set; }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 应用程序版本号
        /// </summary>
        public MVersion Version { get; set; }

        /// <summary>
        /// 版本描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 下载基本URL
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// 自动更新包含的文件列表
        /// </summary>
        public List<MFile> Files { get; set; }

        /// <summary>
        /// 是否允许忽略
        /// </summary>
        public bool AllowIgnore { get; set; }
    }
}
