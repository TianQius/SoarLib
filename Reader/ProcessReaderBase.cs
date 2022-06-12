using System;
using System.Collections.Generic;
using System.Linq;
using SoarLib.Natives;
using System.Runtime.InteropServices;

namespace SoarLib.Reader
{
    internal class ProcessReaderBase : ProcessReaderNative
    {
        private IntPtr Handle;
        private MODULEENTRY32[] modules;
        public ProcessReaderBase(ProcessModule module)
        {
            Handle = module.Handle;
            modules = module.Modules;
        }
        #region Read
        public int ReadTrait(
           string SearshText,
           List<int> list,
           int StartAddr = 0x00000000,
           int EndAddr = 0x7FFFFFFF)
        {
            list.Clear();
            int nBaseAddr = StartAddr;
            IntPtr ProcessHandle = Handle;
            MEMORY_BASIC_INFORMATION Memoress = new MEMORY_BASIC_INFORMATION();
            byte[] Searshpart = GetByteArray(SearshText);
            int Searleng = Searshpart.Length; int Index = -1;
            while (VirtualQueryEx(ProcessHandle, (IntPtr)nBaseAddr, out Memoress, Marshal.SizeOf(Memoress)) != 0)
            {
                if (Memoress.Protect != 1 && Memoress.Type != 262144 && Memoress.Protect != 16 && Memoress.Protect != 128)
                {
                    byte[] blackaddress = new byte[Memoress.RegionSize];
                    NTSTATUS ntreturn = NtReadVirtualMemory(ProcessHandle, (IntPtr)nBaseAddr, blackaddress, Memoress.RegionSize, 0);
                    if (((int)ntreturn) == 0)
                    {
                        if (blackaddress == null || Searshpart == null || blackaddress.Length == 0 || Searshpart.Length == 0 || blackaddress.Length < Searshpart.Length)
                            return -1;
                        int i, j;
                        for (i = 0; i < blackaddress.Length - Searshpart.Length + 1; i++)
                        {
                            if (blackaddress[i] == Searshpart[0])
                            {
                                for (j = 1; j < Searshpart.Length; j++)
                                {
                                    if (blackaddress[i + j] != Searshpart[j])
                                        break;
                                }
                                if (j == Searshpart.Length)
                                    Index = i;
                            }
                        }
                        if (Index != -1)
                        {
                            list.Add(nBaseAddr + Index);
                            byte[] NewBytes = blackaddress.Skip(Index + Searleng).Take(blackaddress.Length - (Index + Searleng)).ToArray();
                            int retn = -1; i = j = 0;
                            if (blackaddress == null || Searshpart == null || blackaddress.Length == 0 || Searshpart.Length == 0 || blackaddress.Length < Searshpart.Length)
                                return -1;
                            for (i = 0; i < blackaddress.Length - Searshpart.Length + 1; i++)
                            {
                                if (blackaddress[i] == Searshpart[0])
                                {
                                    for (j = 1; j < Searshpart.Length; j++)
                                    {
                                        if (blackaddress[i + j] != Searshpart[j])
                                            break;
                                    }
                                    if (j == Searshpart.Length)
                                        retn = i;
                                }
                            }
                            if (retn != -1)
                                Index = retn + Index + Searleng;
                        }
                    }
                }
                nBaseAddr += Memoress.RegionSize;
            }
            return list.Count;
        }
        public byte[] ReadBytes(int address)
        {
            IntPtr suffer = IntPtr.Zero;
            byte[] buffer = new byte[0]; int retn_int = 0x1;
            bool retn = ReadProcessMemory_bytes(Handle, (IntPtr)address, buffer, buffer.Length, out suffer);
            if (retn == true || retn_int == 0)
                return buffer;
            else
                return buffer;
        }
        public byte[] ReadBytes(int address, int length)
        {
            IntPtr suffer = IntPtr.Zero;
            byte[] buffer = new byte[0]; int retn_int = 0x1;
            bool retn = ReadProcessMemory_bytes(Handle, (IntPtr)address, buffer, length, out suffer);
            if (retn == true || retn_int == 0)
                return buffer;
            else
                return buffer;
        }
        public int ReadInt(int address)
        {
            IntPtr suffer = IntPtr.Zero;
            int buffer = 0;
            bool retn = ReadProcessMemory_int(Handle, (IntPtr)address, buffer, buffer, out suffer);
            return buffer;
        }
        public float ReadFloat(int address)
        {
            IntPtr suffer = IntPtr.Zero;
            float buffer = 0;
            bool retn = ReadProcessMemory_float(Handle, (IntPtr)address, buffer, (int)buffer, out suffer);
            return buffer;
        }

        public int ReadModuleAddressToInt(string moduleName, string ModuleOffset, string[] MemoryOffset)
        {
            int address = (int)ReadModuleAddress(moduleName.Trim(), ModuleOffset.Trim());
            address = ReadInt(address);
            for (int i = 0; i < MemoryOffset.Length; i++)
            {
                address = ReadInt(address + Convert.ToInt32(MemoryOffset[i].Trim(), 16));
            }
            return address;
        }
        public IntPtr ReadModuleAddress(string moduleName, string Offsets)
        {
            IntPtr moduleAddr = IntPtr.Zero;
            for (int i = 0; i < modules.Length; i++)
            {
                if (moduleName == modules[i].szModule.ToString())
                {
                    moduleAddr = (IntPtr)((int)modules[i].modBaseAddr + Convert.ToInt32(Offsets, 16));
                    break;
                }
            }
            return moduleAddr;
        }
        #endregion
        #region another
        private byte[] GetByteArray(string shex)
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
