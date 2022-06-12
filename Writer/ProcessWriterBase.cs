using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using SoarLib.Natives;


namespace SoarLib.Writer
{
    internal class ProcessWriterBase : ProcessWriterNative
    {
        private IntPtr Handle;
        private TraitOption to = new TraitOption();
        //Reader.ProcessReaderBase rb;
        public ProcessWriterBase(ProcessModule module)
        {
            Handle = module.Handle;
        }
        public TraitOption TraitOptions
        {
            get { return to; }
            set { to = value; }
        }
        #region Write
        public int WriteTrait(string score, string buffer)
        {
            if (to.All)
                return WriteTrait_All(score, buffer);
            else if (to.Static)
                return WriteTrait_Static(score, buffer);
            else 
                return WriteTrait_NonStatic(score, buffer);
        }
        private int WriteTrait_NonStatic(string SearshText, string change)
        {
            int nBaseAddr = 0x00000000;
            IntPtr ProcessHandle = Handle;
            MEMORY_BASIC_INFORMATION Memoress = new MEMORY_BASIC_INFORMATION();
            nBaseAddr = 0x00000000;
            List<int> list = new List<int>();
            byte[] Searshpart = GetByteArray(SearshText);
            byte[] Changepart = GetByteArray(change);
            int Searleng = Searshpart.Length; int Index = -1; ;
            while (VirtualQueryEx(ProcessHandle, (IntPtr)nBaseAddr, out Memoress, 28) != 0)
            {
                if (to.Writable)
                {
                    if (Memoress.Type == 0x20000 && Memoress.State == 0x1000 && Memoress.Protect != 1 && Memoress.Protect != 2 && Memoress.Protect != 0x10 && Memoress.Protect != 0x20 && Memoress.Protect != 0x80)
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
                    for (int i = 0; i < list.Count; i++)
                    {
                       NtWriteVirtualMemory(ProcessHandle, (IntPtr)list[i], Changepart, Changepart.Length, 0);
                    }
                }
                else
                {
                    if (Memoress.Type == 0x20000 && Memoress.State == 0x1000 && Memoress.Protect != 4 && Memoress.Protect != 8 && Memoress.Protect != 0x40)
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
                    int Protect;
                    for (int i = 0; i < list.Count; i++)
                    {
                        VirtualProtectEx(ProcessHandle, (IntPtr)list[i], Changepart.Length, 64, out Protect);
                        NtWriteVirtualMemory(ProcessHandle, (IntPtr)list[i], Changepart, Changepart.Length, 0);
                        VirtualProtectEx(ProcessHandle, (IntPtr)list[i], Changepart.Length, Protect, out Protect);
                    }
                }
                nBaseAddr += Memoress.RegionSize;
            }
            return list.Count;
        }
        private int WriteTrait_Static(string SearshText, string change)
        {
            int nBaseAddr = 0x000000;
            IntPtr ProcessHandle = Handle;
            MEMORY_BASIC_INFORMATION Memoress = new MEMORY_BASIC_INFORMATION();
            nBaseAddr = 0x00000000;
            List<int> list = new List<int>();
            byte[] Searshpart = GetByteArray(SearshText);
            byte[] Changepart = GetByteArray(change);
            int Searleng = Searshpart.Length; int Index = -1;
            while (VirtualQueryEx(ProcessHandle, (IntPtr)nBaseAddr, out Memoress, 28) != 0)
            {
                if (to.Writable)
                {
                    if (Memoress.Type == 0x1000000 && Memoress.State == 0x1000 && Memoress.Protect != 1 && Memoress.Protect != 2 && Memoress.Protect != 8 && Memoress.Protect != 0x10 && Memoress.Protect != 0x20 && Memoress.Protect != 0x80)
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
                    for (int i = 0; i < list.Count; i++)
                    {
                        NtWriteVirtualMemory(ProcessHandle, (IntPtr)list[i], Changepart, Changepart.Length, 0);
                    }
                }
                else if (!to.Writable)
                {
                    if (Memoress.State == 0x1000 && Memoress.Protect != 0x4 && Memoress.Type == 0x1000000 && Memoress.Protect != 0x8 && Memoress.Protect != 0x40)
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
                    int Protect;
                    for (int i = 0; i < list.Count; i++)
                    {
                        VirtualProtectEx(ProcessHandle, (IntPtr)list[i], Changepart.Length, 64, out Protect);
                        NtWriteVirtualMemory(ProcessHandle, (IntPtr)list[i], Changepart, Changepart.Length, 0);
                        VirtualProtectEx(ProcessHandle, (IntPtr)list[i], Changepart.Length, Protect, out Protect);
                    }
                }
                nBaseAddr += Memoress.RegionSize;
            }
            return list.Count;
        }
        public int WriteTrait_All(string SearshText, string change)
        {
            int nBaseAddr = 0;
            List<int> list = new List<int>();
            IntPtr ProcessHandle = Handle;
            MEMORY_BASIC_INFORMATION Memoress = new MEMORY_BASIC_INFORMATION();
            byte[] Searshpart = GetByteArray(SearshText);
            byte[] Changepart = GetByteArray(change);
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
                    int Protect;
                    for (int i = 0; i < list.Count; i++)
                    {
                        VirtualProtectEx(ProcessHandle, (IntPtr)list[i], Changepart.Length, 64, out Protect);
                        NtWriteVirtualMemory(ProcessHandle, (IntPtr)list[i], Changepart, Changepart.Length, 0);
                        VirtualProtectEx(ProcessHandle, (IntPtr)list[i], Changepart.Length, Protect, out Protect);
                    }
                }
                nBaseAddr += Memoress.RegionSize;
            }
            return list.Count;
        }
        public bool WriteBytes(int address, byte[] buffer)
        {
            IntPtr suffer = IntPtr.Zero; bool retn = false; int retn_int = 0x1;
            retn = WriteProcessMemory_bytes(Handle, (IntPtr)address, buffer, buffer.Length, out suffer);
            if (retn_int == 0)
                retn = true;
            return retn;
        }
        public bool WriteBytes(int address, byte[] buffer, int Writelength)
        {
            IntPtr suffer = IntPtr.Zero; bool retn = false; int retn_int = 0x1;
            WriteProcessMemory_bytes(Handle, (IntPtr)address, buffer, Writelength, out suffer);
            if (retn_int == 0)
                retn = true;
            return retn;
        }
        public bool WriteInt(int address, int buffer)
        {
            IntPtr suffer = IntPtr.Zero;
            UInt64 Suffer = 0; NTSTATUS nt = new NTSTATUS();
            bool retn = false;
            retn = WriteProcessMemory_int(Handle, (IntPtr)address, new int[] { buffer }, buffer, out suffer);
            return retn;
        }
        public bool WriteFloat(int address, float buffer)
        {
            IntPtr suffer = IntPtr.Zero;
            bool retn = false;
            retn = WriteProcessMemory_float(Handle, (IntPtr)address, new float[] { buffer }, (int)buffer, out suffer);
            return retn;
        }
        #endregion
        public IntPtr Create(int leng)
        {
            IntPtr memory = VirtualAllocEx(Handle, IntPtr.Zero, leng, 4096, 64);
            if (memory == IntPtr.Zero)
            {
                Console.WriteLine("Create memory fail");
                return IntPtr.Zero;
            }
            Console.WriteLine("Create memory yeah");
            return memory;
        }
        public IntPtr Create(int leng, byte[] bytes)
        {
            IntPtr Written;
            IntPtr memory = VirtualAllocEx(Handle, IntPtr.Zero, leng, 4096, 64);
            if (memory == IntPtr.Zero)
            {
                Console.WriteLine("Create memory fail");
                return IntPtr.Zero;
            }
            if (!WriteProcessMemory_bytes(Handle, memory, bytes, bytes.Length, out Written))
            {
                Console.WriteLine("Write memory fail");
                return IntPtr.Zero;
            }
            Console.WriteLine("Create memory yeah");
            return memory;
        }
        public bool Free(IntPtr Address)
        {
            if (!VirtualFreeEx(Handle, Address, 0, 32768))
            {
                Console.WriteLine("Free memory fail");
                return false;
            }
            return true;
        }
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
