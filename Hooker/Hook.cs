using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoarLib.Hooker
{
    public class Hook
    {
        private IntPtr Handle;
        public Hook(ProcessModule module)
        {
            Handle = module.Handle;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Hookaddress"></param>Hook地址
        /// <param name="HookaddressTail"></param>Hook地址尾
        /// <param name="hookHead"></param>Hook头
        /// <param name="hooktail"></param>Hook尾
        /// <param name="NopLeng"></param>Nop数
        /// <param name="jumpoffset"></param>跳转偏移
        public IntPtr HookAddress(IntPtr Hookaddress, string HookaddressTail, string hookHead, string hooktail, int NopLeng, string jumpoffset)
        {
            return HookBase.hook(Handle, Hookaddress, HookaddressTail, hookHead, hooktail, NopLeng, jumpoffset);
        }
        public bool Jump(IntPtr Hookaddress, IntPtr Jumpaddress)
        {
            return HookBase.Jmp(Handle, Hookaddress, Jumpaddress);
        }
        public bool Jump(IntPtr Hookaddress, IntPtr Jumpaddress, byte[] AttachBytes)
        {
            return HookBase.Jmp(Handle, Hookaddress, Jumpaddress, AttachBytes);
        }
        public static bool Jump(IntPtr handle, IntPtr Hookaddress, IntPtr Jumpaddress)
        {
            return HookBase.Jmp(handle, Hookaddress, Jumpaddress);
        }
        public static bool Jump(IntPtr handle, IntPtr Hookaddress, IntPtr Jumpaddress, byte[] AttachBytes)
        {
            return HookBase.Jmp(handle, Hookaddress, Jumpaddress, AttachBytes);
        }

    }
}
