using System;
using System.Runtime.InteropServices;

namespace SoarLib.Natives
{
    public class HookNative
    {
        public struct MEMORY_BASIC_INFORMATION
        {
            public int BaseAddress;
            public int AllocationBase;
            public int AllocationProtect;
            public int RegionSize;
            public int State;
            public int Protect;
            public int Type;
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtectEx(
            int hProcess, IntPtr lpAddress, Int32 dwSize, Int32 flNewProtect, out Int32 lpflOldProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 VirtualQueryEx(
            int hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, Int32 dwLength);

        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory_bytes(
         int hProcess,
         IntPtr lpBaseAddress,
         [Out] byte[] lpBuffer,
         int dwSize,
         out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory_bytes(
         int hProcess,
         IntPtr lpBaseAddress,
         byte[] lpBuffer,
         Int32 nSize,
         out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr lstrcpyn(
            [Out] byte[] lpString1,
            [Out] byte[] lpString2,
            int iMaxLength);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
            int dwSize, int flAllocationType, int flProtect);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress,
            int dwSize, int dwFreeType);
    }
}
