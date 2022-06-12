using System;
using System.Runtime.InteropServices;

namespace SoarLib.Natives
{
    public class AsmNative
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetHandleCount(byte[] value);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtect(IntPtr lpAddress, int dwSize, int flNewProtect, out int lpflOldProtect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr CallMethod();
    }
}
