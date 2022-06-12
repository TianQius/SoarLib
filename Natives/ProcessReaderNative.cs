using System;
using System.Runtime.InteropServices;

namespace SoarLib.Natives
{
    public class ProcessReaderNative : ProcessWriterNative
    {
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory_bytes(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out] byte[] lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory_int(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out] int lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory_float(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out] float lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead);
    }
}
