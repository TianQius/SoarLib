using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using SoarLib.Natives;
using System.Collections;

namespace SoarLib.ASM
{
    internal class AsmBasic : AsmNative
    {
        #region help
        public static byte[] Analysis(string Asm)
        {
            string handle = Asm;
            string between; byte[] bytes = new byte[] { };
            handle = handle.Replace(",", "");
            handle = handle.Replace("{", "");
            handle = handle.Replace("}", "");
            handle = handle.Replace(" ", "");
            for (int i = 0; i < handle.Length; i++, i++) 
            {
                between = handle.Substring(i, 2);
                bytes = Add(bytes, GetByteArray(between).Skip(0).Take(1).ToArray());
            }
            return bytes;
        }
        private static byte[] Add(byte[] a, byte[] b)
        {
            ArrayList retArray = new ArrayList();
            for (int i = 0; i < a.Length; i++)
            {
                retArray.Add(a[i]);
            }
            for (int i = 0; i < b.Length; i++)
            {
                retArray.Add(b[i]);
            }
            return (byte[])retArray.ToArray(typeof(byte));
        }
        public static byte[] GetByteArray(string shex)
        {
            string[] ssArray = shex.Split(' ');
            List<byte> bytList = new List<byte>();
            foreach (var s in ssArray)
            {
                bytList.Add(Convert.ToByte(s, 16));
            }
            return bytList.ToArray();
        }
        #endregion
    }
}
