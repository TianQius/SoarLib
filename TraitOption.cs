using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoarLib
{
    /// <summary>
    /// 十六进制字节数组读写设置
    /// </summary>
    public class TraitOption
    {
        private static bool _Writable = false;
        private static bool _Static = true;
        private static bool _All = false;
        /// <summary>
        /// 欲读写地址是否可写，默认为false
        /// </summary>
        public bool Writable
        { get { return _Writable; } set { _Writable = value; } }
        /// <summary>
        /// 欲读写地址是否静态，默认为true
        /// </summary>
        public bool Static
        { get { return _Static; } set { _Static = value; } }
        /// <summary>
        /// 所有类型
        /// </summary>
        public bool All
        { get { return _All; } set { _All = value; } }
    }
}
