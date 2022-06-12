using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SoarLib.Natives;
using System.Collections;

namespace SoarLib.Hooker
{
    internal class HookBase : HookNative
    {
        #region APIHOOK
        private bool IsHook = false;
        private byte[] bytes = new byte[5];
        private byte[] newbytes;
        private IntPtr newAddress;
        private int MemoryAddress;
        private MEMORY_BASIC_INFORMATION Memories;
        private int hProcess;
        private bool State = false;

        private const int STATE_SUCCESSFUL = 0;
        private const int STATE_ISHOOK = 2;
        private const int STATE_ADDRESS_ZERO = 3;
        private const int STATE_READ_DEFEAT = 4;
        private const int STATE_WRITE_DEFEAT = 5;
        private const int STATE_REQUEST_DEFEAT = 6;
        IntPtr NumberOfBytes, NumberOfBytes2, NumberOfBytes3;

        public IntPtr GetApiAddress(string dll, string apiName)
        {
            return GetProcAddress(GetModuleHandle(dll), apiName);
        }
        public int InstallHook(int Handle, IntPtr beHookedAddress, IntPtr NewAddress, int NumberOfLeng = 1)
        {
            if (IsHook)
                return STATE_ISHOOK;
            if (beHookedAddress == IntPtr.Zero)
                return STATE_ADDRESS_ZERO;
            MemoryAddress = (int)beHookedAddress;
            hProcess = Handle;
            ModifyProperties(-1, beHookedAddress, Memories);
            if (Handle == -1)
            {
                if (NewAddress == IntPtr.Zero)
                    newbytes = AddBytes(new byte[3] { 51, 192, 194 }, BitConverter.GetBytes(NumberOfLeng * 4));
                else
                    newbytes = AddBytes(new byte[1] { 233 }, BitConverter.GetBytes((Int32)((Int32)NewAddress - (Int32)beHookedAddress - 5)));
                Marshal.Copy(beHookedAddress, bytes, 0, 5);
                if (!WriteProcessMemory_bytes(-1, beHookedAddress, newbytes, 5, out NumberOfBytes))
                { return STATE_WRITE_DEFEAT; }
                bytes = AddBytes(bytes, new byte[5] { 233, 0, 0, 0, 0 });
                newAddress = lstrcpyn(bytes, bytes, bytes.Length);
                if (!WriteProcessMemory_bytes(-1, (IntPtr)((Int32)newAddress + 6), BitConverter.GetBytes(((Int32)((Int32)beHookedAddress + 5 - ((Int32)newAddress + 10)))), 4, out NumberOfBytes))
                { return STATE_WRITE_DEFEAT; }
                IsHook = true;
                return STATE_SUCCESSFUL;
            }
            newbytes = AddBytes(new byte[3] { 51, 192, 194 }, BitConverter.GetBytes(NumberOfLeng * 4));
            bytes = new byte[5];
            if (!ReadProcessMemory_bytes(hProcess, (IntPtr)MemoryAddress, bytes, 5, out NumberOfBytes2))
            { return STATE_READ_DEFEAT; }
            newAddress = (IntPtr)CreateMemory(Handle, AddBytes(bytes, new byte[5] { 233, 0, 0, 0, 0 }));
            if (newAddress.ToInt32() == 0)
            { return STATE_REQUEST_DEFEAT; }
            byte[] temp = BitConverter.GetBytes(((Int32)((Int32)beHookedAddress + 5 - ((Int32)newAddress + 10))));
            if (!WriteProcessMemory_bytes(Handle, (IntPtr)((Int32)newAddress + 6), temp, 4, out NumberOfBytes3))
            { return STATE_WRITE_DEFEAT; }
            if (!WriteProcessMemory_bytes(Handle, (IntPtr)MemoryAddress, newbytes, 5, out NumberOfBytes3))
            { return STATE_WRITE_DEFEAT; }
            IsHook = true;
            return STATE_SUCCESSFUL;

        }
        public bool UninstallHook()
        {
            if (IsHook == false)
                return false;
            IntPtr written = IntPtr.Zero;
            WriteProcessMemory_bytes(hProcess, (IntPtr)MemoryAddress, bytes, 5, out written);

            if (hProcess != -1)
            { FreeMemory(hProcess, newAddress); }
            RestoreProerties(hProcess, Memories);
            IsHook = false;
            newbytes = new byte[] { };
            bytes = new byte[] { };
            MemoryAddress = 0;
            newAddress = IntPtr.Zero;
            hProcess = 0;
            return true;
        }
        public bool StopHook()
        {
            if (State == false)
            {
                IntPtr written = IntPtr.Zero;
                WriteProcessMemory_bytes(hProcess, (IntPtr)MemoryAddress, bytes, 5, out written);
                State = true;
                return true;
            }
            return false;
        }

        public bool GoHook()
        {
            if (State == true)
            {
                IntPtr written = IntPtr.Zero;
                WriteProcessMemory_bytes(hProcess, (IntPtr)MemoryAddress, newbytes, 5, out written);
                State = false;
                return true;
            }
            return false;
        }
        private bool ModifyProperties(int process, IntPtr address, MEMORY_BASIC_INFORMATION Memorie)
        {
            if (VirtualQueryEx(process, address, out Memorie, 28) == 0)
            {
                Console.WriteLine("Get memory's Properties is false");
                return false;
            }
            if (!VirtualProtectEx(process, (IntPtr)Memorie.BaseAddress, 8, 64, out Memorie.Protect))
            {
                Console.WriteLine("Change memory's Properties is false");
                return false;
            }
            return true;
        }
        private bool RestoreProerties(int process, MEMORY_BASIC_INFORMATION Memorie)
        {
            if (!VirtualProtectEx(process, (IntPtr)Memorie.BaseAddress, 8, 32, out Memorie.Protect))
            {
                Console.WriteLine("Change memory's Properties is false");
                return false;
            }
            return true;
        }

        private int CreateMemory(int hprocess, byte[] bytes)
        {
            IntPtr Written;
            IntPtr memory = VirtualAllocEx((IntPtr)hprocess, IntPtr.Zero, bytes.Length, 4096, 64);
            if (memory == IntPtr.Zero)
            {
                Console.WriteLine("Create memory fail");
                return 0;
            }
            if (!WriteProcessMemory_bytes(hprocess, memory, bytes, bytes.Length, out Written))
            {
                Console.WriteLine("Write memory fail");
                return 0;
            }
            Console.WriteLine("Create memory yeah");
            return memory.ToInt32();
        }
        private bool FreeMemory(int hprocess, IntPtr Address)
        {
            if (!VirtualFreeEx((IntPtr)hprocess, Address, 0, 32768))
            {
                Console.WriteLine("Free memory fail");
                return false;
            }
            return true;
        }
        #endregion
        #region ProcessHook
        public static bool Jmp(IntPtr hprocess, IntPtr Hookaddress, IntPtr Jumpaddress)
        {
            return WriteMemory_bytes(hprocess, Hookaddress.ToInt32(), AddBytes(new byte[] { 233 }, BitConverter.GetBytes((Int32)((Int32)Jumpaddress - (Int32)Hookaddress - 5))));
        }
        public static bool Jmp(IntPtr hprocess, IntPtr Hookaddress, IntPtr Jumpaddress, byte[] AttachBytes)
        {
            return WriteMemory_bytes(hprocess, Hookaddress.ToInt32(), AddBytes(AddBytes(new byte[] { 233 }, BitConverter.GetBytes((Int32)((Int32)Jumpaddress - (Int32)Hookaddress - 5))), AttachBytes));
        }
        public static IntPtr hook(IntPtr handle, IntPtr Hookaddress, string HookaddressTail, string hookHead, string hooktail, int NopLeng, string jumpoffset)
        {
            IntPtr memory = VirtualAllocEx(handle, IntPtr.Zero, 100, 4096, 64);
            IntPtr blackmemory = (IntPtr)((int)memory + 50);
            string hooksNop = "";
            for (int i = 0; i < NopLeng; i++)
            {
                if (i == NopLeng - 1)
                { hooksNop += "90"; }
                else { hooksNop += "90 "; }
            }
            WriteMemory_bytes(handle, (int)memory, AddBytes(AddBytes(GetByteArray(hookHead), BitConverter.GetBytes((int)blackmemory)), GetByteArray(HookaddressTail)));
            Jmp(handle, Hookaddress, memory, GetByteArray(hooksNop));
            Jmp(handle, (IntPtr)((int)memory + Convert.ToInt32(hooktail, 16)), (IntPtr)((int)Hookaddress + Convert.ToInt32(jumpoffset, 16)));
            return memory;
        }
        /// <summary>
        /// 代码(汇编)注入，使被hook的地址跳转到新开辟地址执行注入的汇编，之后返回原地址,特别提示：注意平衡堆栈
        /// </summary>
        /// <param name="processid"></param>进程ID
        /// <param name="Hookaddress"></param>欲Hook的地址（建议十六进制）例如0x064A0572
        /// <param name="Assembles"></param>欲注入的汇编（机器码格式）
        /// <returns>返回开辟的地址</returns>
        private static IntPtr Hook_InJectAssembles(IntPtr handle, IntPtr Hookaddress, byte[] Assembles, int NopLeng)
        {
            IntPtr memory = VirtualAllocEx(handle, IntPtr.Zero, 256, 4096, 64);
            string hooksNop = "";
            for (int i = 0; i < NopLeng; i++)
            {
                if (i == NopLeng - 1)
                { hooksNop += "90"; }
                else { hooksNop += "90 "; }
            }
            //WriteMemory_bytes(handle, (int)memory-5, AddBytes(Assembles, Getjump_back((IntPtr)((int)memory + Assembles.Length), Hookaddress)));
            /*
            WriteMemory_bytes(handle, (int)memory, AddBytes(Assembles, Getjump_back(memory, Hookaddress)));
            Jmp(handle, Hookaddress, (IntPtr)((int)memory + 5), BytesHandle.GetByteArray(hooksNop));
            */
            WriteMemory_bytes(handle, (int)memory, Assembles);
            Jmp(handle, (IntPtr)((int)memory + Assembles.Length), Hookaddress);
            Jmp(handle, Hookaddress, memory, GetByteArray(hooksNop));

            return memory;
        }
        #endregion
        #region another
        private byte[] AddBytes(byte[] a, byte[] b)
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
        private static byte[] AddBytes(byte[] a, byte[] b, int arg = 0)
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
        private static byte[] GetByteArray(string shex)
        {
            string[] ssArray = shex.Split(' ');
            List<byte> bytList = new List<byte>();
            foreach (var s in ssArray)
            {
                bytList.Add(Convert.ToByte(s, 16));
            }
            return bytList.ToArray();
        }
        private static bool WriteMemory_bytes(IntPtr handle, int address, byte[] buffer)
        {
            IntPtr suffer = IntPtr.Zero; bool retn = false;
            return WriteProcessMemory_bytes((int)handle, (IntPtr)address, buffer, buffer.Length, out suffer);
        }
        #endregion


    }
}
