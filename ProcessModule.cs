using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoarLib.Natives;
using System.Runtime.InteropServices;

namespace SoarLib
{
    public class ProcessModule : ModuleNative
    {
        private int dwProcessId;
        private static int _pid;
        private static IntPtr handle;
        private static MODULEENTRY32[] modules;
        #region 属性
        public IntPtr Handle
        {
            get { return handle; }
        }
        public int PID
        {
            get { return _pid; }
        }
        public MODULEENTRY32[] Modules
        {
            get { return modules; }
        }
        #endregion
        public ProcessModule(string ProcessName)
        {
            _pid = GetProcessID(ProcessName);
            handle = OpenProcess(PID);
            GetProcessModule(PID, ref modules);
        }
        public static ProcessModule Load(string processName)
        {
            var module = new ProcessModule(processName);
            return module;
        }

        private IntPtr OpenProcess(int ProcessId)
        {
            if (handle != IntPtr.Zero) 
            {
                CloseHandle(handle);
                ProcessId = 0;
            }
            handle = NtOpenProcess((IntPtr)ProcessId);
            return handle;
        }
        private int GetProcessID(string ProcessName)
        {
            ProcessEntry32 pe32 = new ProcessEntry32();
            IntPtr handle = CreateToolhelp32Snapshot(0x2, 0);
            if (handle == IntPtr.Zero)
            { return 0; }
            pe32.dwSize = (uint)Marshal.SizeOf(pe32);
            int next = Process32First(handle, ref pe32);
            //Console.WriteLine(next.ToString());
            while (next == 1)
            {
                //Console.WriteLine(pe32.szExeFile);
                if (pe32.szExeFile == ProcessName)
                {
                    CloseHandle(handle);
                    dwProcessId = (int)pe32.th32ProcessID;
                    return (int)pe32.th32ProcessID;
                }
                next = Process32Next(handle, ref pe32);
            }
            CloseHandle(handle);
            return -1;
        }
        private IntPtr NtOpenProcess(IntPtr processID)
        {
            IntPtr handle_retn = IntPtr.Zero;
            CLIENT_ID clientid = new CLIENT_ID
            {
                UniqueProcess = processID,
                UniqueThread = IntPtr.Zero
            };
            OBJECT_ATTRIBUTES ObjeCt = new OBJECT_ATTRIBUTES
            {
                Length = (int)Marshal.SizeOf(typeof(OBJECT_ATTRIBUTES)),
                RootDirectory = IntPtr.Zero,
                ObjectName = IntPtr.Zero,
                Attributes = 0x00000040,
                SecurityDescriptor = IntPtr.Zero,
                SecurityQualityOfService = IntPtr.Zero
            };
            ZwOpenProcess(ref handle_retn, 0x1F0FFF, ref ObjeCt, ref clientid);//2035711
            return handle_retn;
        }
        private int GetProcessModule(int PID, ref MODULEENTRY32[] modules)
        {
            IntPtr Snapshothandle = CreateToolhelp32Snapshot(0x08, (uint)PID);
            if (Snapshothandle == IntPtr.Zero)
                return 0;
            MODULEENTRY32 moduleentry32 = new MODULEENTRY32();
            //Natives.MODULEENTRY32[] moduleentryS = new Natives.MODULEENTRY32[0];
            List<MODULEENTRY32> list = new List<MODULEENTRY32>();
            moduleentry32.dwSize = (uint)Marshal.SizeOf(moduleentry32);
            bool nu = Module32First(Snapshothandle, ref moduleentry32);
            while (nu)
            {
                //Console.WriteLine("模块枚举{0}", moduleentry32.szModule.ToString());
                list.Add(moduleentry32);
                nu = Module32Next(Snapshothandle, ref moduleentry32);
            }
            CloseHandle(Snapshothandle);
            modules = list.ToArray();
            return list.Count;
        }

    }
}
