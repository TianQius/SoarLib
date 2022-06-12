using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SoarLib.Natives
{
    public class PaintNative
    {
        #region API
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr CreateWindowEx(
            WindowStylesEx dwExStyle,
            string lpClassName,
            string lpWindowName,
            WindowStyles dwStyle,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hWndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            IntPtr lpParam);
        [DllImport("kernel32", CharSet = CharSet.Ansi)]
        public static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress,
      IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);
        [DllImport("user32.dll")]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        public static extern void PostQuitMessage(int nExitCode);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern Int32 RegisterClassEx([In] ref WNDCLASSEX lpwcx);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
        #endregion
        #region Struct
        [StructLayout(LayoutKind.Sequential)]
        public struct WNDCLASSEX
        {
            public uint cbSize;
            public ClassStyles style;
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public WndProc lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
            public IntPtr hIconSm;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; // 左上角的 x 位置
            public int Top; // 左上角的y位置
            public int Right; // 右下角的 x 位置
            public int Bottom; // 右下角的 y 位置
        }
        public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        #endregion
        #region enum
        [Flags]
        public enum WindowStyles : uint
        {
            WS_BORDER = 0x800000,
            WS_CAPTION = 0xc00000,
            WS_CHILD = 0x40000000,
            WS_CLIPCHILDREN = 0x2000000,
            WS_CLIPSIBLINGS = 0x4000000,
            WS_DISABLED = 0x8000000,
            WS_DLGFRAME = 0x400000,
            WS_GROUP = 0x20000,
            WS_HSCROLL = 0x100000,
            WS_MAXIMIZE = 0x1000000,
            WS_MAXIMIZEBOX = 0x10000,
            WS_MINIMIZE = 0x20000000,
            WS_MINIMIZEBOX = 0x20000,
            WS_OVERLAPPED = 0x0,
            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_POPUP = 0x80000000u,
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
            WS_SIZEFRAME = 0x40000,
            WS_SYSMENU = 0x80000,
            WS_TABSTOP = 0x10000,
            WS_VISIBLE = 0x10000000,
            WS_VSCROLL = 0x200000
        }
        [Flags]
        public enum ClassStyles : uint
        {
            ByteAlignClient = 0x1000,

            ByteAlignWindow = 0x2000,

            ClassDC = 0x40,

            DoubleClicks = 0x8,

            DropShadow = 0x20000,

            GlobalClass = 0x4000,

            HorizontalRedraw = 0x2,

            NoClose = 0x200,

            OwnDC = 0x20,

            ParentDC = 0x80,

            SaveBits = 0x800,

            VerticalRedraw = 0x1
        }
        [Flags]
        public enum WindowStylesEx : uint
        {
            WS_EX_ACCEPTFILES = 0x00000010,

            WS_EX_APPWINDOW = 0x00040000,

            WS_EX_CLIENTEDGE = 0x00000200,

            WS_EX_COMPOSITED = 0x02000000,

            WS_EX_CONTEXTHELP = 0x00000400,

            WS_EX_CONTROLPARENT = 0x00010000,

            WS_EX_DLGMODALFRAME = 0x00000001,

            WS_EX_LAYERED = 0x00080000,

            WS_EX_LAYOUTRTL = 0x00400000,

            WS_EX_LEFT = 0x00000000,

            WS_EX_LEFTSCROLLBAR = 0x00004000,

            WS_EX_LTRREADING = 0x00000000,

            WS_EX_MDICHILD = 0x00000040,

            WS_EX_NOACTIVATE = 0x08000000,

            WS_EX_NOINHERITLAYOUT = 0x00100000,

            WS_EX_NOPARENTNOTIFY = 0x00000004,

            WS_EX_NOREDIRECTIONBITMAP = 0x00200000,

            WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,

            WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,

            WS_EX_RIGHT = 0x00001000,

            WS_EX_RIGHTSCROLLBAR = 0x00000000,

            WS_EX_RTLREADING = 0x00002000,

            WS_EX_STATICEDGE = 0x00020000,

            WS_EX_TOOLWINDOW = 0x00000080,

            WS_EX_TOPMOST = 0x00000008,

            WS_EX_TRANSPARENT = 0x00000020,

            WS_EX_WINDOWEDGE = 0x00000100
        }
        #endregion

        public static string cmd(string OutPut)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(OutPut + "&exit");
            p.StandardInput.AutoFlush = true;
            string Strout = p.StandardOutput.ReadToEnd();
            Console.WriteLine(Strout);
            p.WaitForExit();
            p.Close();
            return Strout;
        }
        public struct SquareBox
        {
            public int X;
            public int Y;
            public int Width;
            public int Height;
        }
        public struct D3D
        {
            public float X;
            public float Y;
            public float Z;
        }
        public struct D2D
        {
            public float X;
            public float Y;
        }
    }
}
