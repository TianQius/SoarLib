using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoarLib.Hooker
{
    public class ApiHook
    {
        private IntPtr Handle;
        private HookBase hook = new HookBase();
        public ApiHook(ProcessModule module)
        {
            Handle = module.Handle;
        }
        #region APIHook
        public IntPtr GetApiAddress(string dll, string apiName)
        {
            return HookBase.GetProcAddress(HookBase.GetModuleHandle(dll), apiName);
        }
        /// <summary>
        /// 安装hook,成功返回0，已被hook返回2，欲hook地址为空返回3，读入失败返回4，写入失败返回5，开辟内存失败返回6
        /// </summary>
        /// <param name="process"></param>进程句柄，hook自身填-1
        /// <param name="beHookedAddress"></param>欲hook地址
        /// <param name="NewAddress"></param>回调函数地址
        /// <param name="NumberOfLeng"></param>参数数目，默认为1
        /// <returns></returns>
        public int InstallApiHook(IntPtr beHookedAddress, IntPtr NewAddress, int NumberOfLeng = 1)
        {
            return hook.InstallHook((int)Handle, beHookedAddress, NewAddress, NumberOfLeng);
        }
        public bool UnInstallApiHook()
        {
            return hook.UninstallHook();
        }
        public bool StopApiHook()
        {
            return hook.StopHook();
        }
        public bool GoApiHook()
        {
            return hook.GoHook();
        }
        #endregion
    }
}
