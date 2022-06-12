using System;
using System.Runtime.InteropServices;

namespace SoarLib.Natives
{
    public class ProcessWriterNative : ModuleNative
    {
        #region Struct
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
        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr handle;
            public uint msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public System.Drawing.Point p;
        }
        #endregion
        #region API
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtectEx(
            IntPtr hProcess, IntPtr lpAddress, Int32 dwSize, Int32 flNewProtect, out Int32 lpflOldProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 VirtualQueryEx(
            IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, Int32 dwLength);

        [DllImport("user32.dll")]
        public static extern uint PeekMessage(out NativeMessage lpMsg, int hWnd, int wMsgFilterMin,
        int wMsgFilterMax, int wRemoveMsg);
        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessage([In] ref NativeMessage lpmsg);
        [DllImport("user32.dll")]
        public static extern bool TranslateMessage([In] ref NativeMessage lpMsg);
        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory_int(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            int[] lpBuffer,
            Int32 nSize,
            out IntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory_bytes(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            Int32 nSize,
            out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory_float(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            float[] lpBuffer,
            Int32 nSize,
            out IntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
           int dwSize, int flAllocationType, int flProtect);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress,
            int dwSize, int dwFreeType);
        #endregion
        #region NTDLL
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern NTSTATUS NtReadVirtualMemory(
             IntPtr ProcessHandle,
             IntPtr BaseAddress,
             byte[] Buffer,
             Int32 NumberOfBytesToRead,
             UInt32 NumberOfBytesRead);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern NTSTATUS NtWriteVirtualMemory(
             IntPtr ProcessHandle,
             IntPtr BaseAddress,
             byte[] Buffer,
             Int32 NumberOfBytesToWrite,
             UInt32 NumberOfBytesWritten);
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern NTSTATUS NtWow64ReadVirtualMemory64(
             IntPtr hProcess,
             UInt64 BaseAddress,
             UInt64 buffer,
             UInt64 BufferLength,
             ref UInt64 ReturnLength);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern NTSTATUS NtWow64WriteVirtualMemory64(
             IntPtr hProcess,
             UInt64 BaseAddress,
             UInt64[] Buffer,
             UInt64 BufferLength,
             ref UInt64 NumberOfBytesWritten);
        #endregion
    }
}
