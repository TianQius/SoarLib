using System;
using System.Collections;
using System.Runtime.InteropServices;
using static SoarLib.Natives.AsmNative;

namespace SoarLib.ASM
{
    public class 置入汇编
    {
        public static IntPtr 置入代码(byte[] _asm)
        {
            return ActAsm(_asm);
        }
        private static IntPtr ActAsm(byte[] buf_asm)
        {
            IntPtr ptr_asm = SetHandleCount(buf_asm);
            VirtualProtect(ptr_asm, buf_asm.Length);
            CallMethod call_method = Marshal.GetDelegateForFunctionPointer(ptr_asm, typeof(CallMethod)) as CallMethod;
            return call_method.Invoke();
        }

        #region 辅助
        private static void VirtualProtect(IntPtr ptr, int size)
        {
            int outMemProtect;
            if (!Natives.AsmNative.VirtualProtect(ptr, size, 64, out outMemProtect))
                throw new Exception("Unable to modify memory protection.");
        }
        #endregion
    }
}
