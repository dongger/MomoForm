using System;

namespace Momo.AutoUpgrade.Core
{
    /// <summary>
    /// 升级文件信息
    /// </summary>
    [Serializable]
    public sealed class MFile
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 相对路径，空表示根目录
        /// </summary>
        public string Path { get; set; }
    }
}
