using System;

namespace Momo.AutoUpgrade.Core
{
    /// <summary>
    /// 版本信息
    /// </summary>
    [Serializable]
    public sealed class MVersion : IComparable<MVersion>
    {
        public MVersion()
        {
        }

        public MVersion(int major, int minor, int build, int revision)
        {
            this.Major = major;
            this.Minor = minor;
            this.Build = build;
            this.Revision = revision;
        }

        #region 操作符重写
        public override bool Equals(object obj)
        {
            if (obj is MVersion)
            {
                return this == (obj as MVersion);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator <(MVersion v1, MVersion v2)
        {
            return v1.CompareTo(v2) == -1;
        }

        public static bool operator >(MVersion v1, MVersion v2)
        {
            return v1.CompareTo(v2) == 1;
        }

        public static bool operator !=(MVersion v1, MVersion v2)
        {
            return v1.CompareTo(v2) != 0;
        }

        public static bool operator ==(MVersion v1, MVersion v2)
        {
            return v1.CompareTo(v2) == 0;
        }
        #endregion

        public override string ToString()
        {
            if (this.Build < 0 && this.Revision >= 0)
            {
                return string.Format("{0}.{1}.{2}", this.Major, this.Minor, this.Revision);
            }
            else if (this.Revision < 0 && this.Build >= 0)
            {
                return string.Format("{0}.{1}.{2}", this.Major, this.Minor, this.Build);
            }

            return string.Format("{0}.{1}.{2}.{3}", this.Major, this.Minor, this.Build, this.Revision);
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="other"></param>
        /// <returns>
        /// 一个 32 位有符号整数，指示要比较的对象的相对顺序。
        /// 小于零：此对象小于 other 参数。 
        /// 零：此对象等于 other。
        /// 大于零：此对象大于 other。
        /// </returns>
        public int CompareTo(MVersion other)
        {
            if (this.ToString() == other.ToString())
            {
                return 0;
            }

            if (this.Major > other.Major)
            {
                return 1;
            }
            else if (this.Major < other.Major)
            {
                return -1;
            }

            if (this.Minor > other.Minor)
            {
                return 1;
            }
            else if (this.Minor < other.Minor)
            {
                return -1;
            }

            if (this.Build > other.Build)
            {
                return 1;
            }
            else if (this.Build < other.Build)
            {
                return -1;
            }

            if (this.Revision > other.Revision)
            {
                return 1;
            }
            else if (this.Revision < other.Revision)
            {
                return -1;
            }

            return 0;
        }

        private int build;
        private int major;
        private int minor;
        private int revision;

        /// <summary>
        /// 获取或设置当前 System.Version 对象版本号的内部版本号部分的值。内部版本号或为 -1（如果未定义内部版本号）。
        /// </summary>
        public int Build { get { return this.build; } set { this.build = value; } }

        /// <summary>
        ///  获取或设置当前 System.Version 对象版本号的主要版本号部分的值。
        /// </summary>
        public int Major { get { return this.major; } set { this.major = value < 0 ? 0 : value; } }

        /// <summary>
        /// 获取或设置当前 System.Version 对象版本号的次要版本号部分的值。
        /// </summary>
        public int Minor { get { return this.minor; } set { this.minor = value < 0 ? 0 : value; } }

        /// <summary>
        /// 获取当前 System.Version 对象版本号的修订号部分的值。修订号或为 -1（如果未定义修订号）。
        /// </summary>
        public int Revision { get { return this.revision; } set { this.revision = value; } }
    }
}