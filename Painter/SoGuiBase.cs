using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using SoarLib.Natives;
using System.Drawing.Text;

namespace SoarLib.Painter
{
    public class SoGuiBase : PaintNative
    {
        private static IntPtr Handle;

        public IntPtr WindowHandle
        {
            get { return Handle; }
            set { Handle = value; }
        }

        private static WndProc wproc;
        public IntPtr CreateWindows(int Width, int Height, IntPtr Callback)
        {
            wproc = WindowProc;
            string WindowClass = "SoarWindows";
            WNDCLASSEX wndclassex = new WNDCLASSEX
            {
                cbSize = 48,
                style = ClassStyles.HorizontalRedraw,
                lpfnWndProc = wproc,
                cbClsExtra = 0,
                cbWndExtra = 0,
                hInstance = GetInstanceHwnd(),
                hIcon = GetIconHwnd(),
                hCursor = GetCursorHwnd(),
                hbrBackground = IntPtr.Zero,
                lpszMenuName = "SoarMenu",
                lpszClassName = WindowClass,
                hIconSm = IntPtr.Zero
            };
            MARGINS margins = new MARGINS();
            if (RegisterClassEx(ref wndclassex) == 0)
            {
                return IntPtr.Zero;
            }
            IntPtr hWnd = CreateWindowEx(WindowStylesEx.WS_EX_APPWINDOW, WindowClass, "SoarWindows", WindowStyles.WS_POPUP, margins.leftWidth, margins.topHeight,
                           Width, Height, IntPtr.Zero, IntPtr.Zero, wndclassex.hInstance, IntPtr.Zero);
            Console.WriteLine((int)hWnd);
            ShowWindow(hWnd, 10);
            UpdateWindow(hWnd);
            SetWindowLong(hWnd, -20, (int)GetWindowLong(hWnd, -20) | 0x20 | 0x80000);
            SetLayeredWindowAttributes(hWnd, 0, 255, 2);
            margins.leftWidth = -1;
            margins.rightWidth = 0;
            margins.topHeight = 0;
            margins.bottomHeight = 0;
            DwmExtendFrameIntoClientArea(hWnd, ref margins);
            IntPtr StartAddr = Marshal.GetFunctionPointerForDelegate((StartThread)_StartThread);
            IntPtr retn = CreateThread(IntPtr.Zero, 0, StartAddr, Callback, 0, IntPtr.Zero);
            //Console.WriteLine("线程句柄：{0}",retn);
            Handle = hWnd;
            return hWnd;
        }
        private delegate void StartThread(IntPtr callback);

        public delegate IntPtr CallBack();

        private static void _StartThread(IntPtr callback)
        {
            IntPtr _callback = (IntPtr)callback;
            while ((int)callback > 0)
            {
                CallBack _call = (CallBack)Marshal.GetDelegateForFunctionPointer(_callback, typeof(CallBack));
                _call.Invoke();
            }
        }
        private static void mod(string put)
        {
            cmd("cmd /c sc config " + "\"UxSms\"" + "start= " + put);
            Thread.Sleep(1000);
        }
        private static void OpenSever()
        {
            cmd("cmd /c net start " + "\"Desktop Window Manager Session Manager\"");
            Thread.Sleep(1000);
        }
        private static void StopSever()
        {
            cmd("cmd /c net stop " + "\"Desktop Window Manager Session Manager\"");
            Thread.Sleep(1000);
        }
        public static void FixBlack()
        {
            mod("demand");
            //theme();
            StopSever();
            OpenSever();

        }
        private static IntPtr GetInstanceHwnd()
        {
            Random next = new Random();
            return (IntPtr)next.Next(1111111, 10000000);
        }
        private static IntPtr GetIconHwnd()
        {
            Random next = new Random();
            return (IntPtr)next.Next(11111, 100000);
        }
        private static IntPtr GetCursorHwnd()
        {
            Random next = new Random();
            return (IntPtr)next.Next(11111, 100000);
        }
        private static IntPtr WindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (2 == msg)
            {
                PostQuitMessage(0);
            }
            return DefWindowProc(hWnd, msg, wParam, lParam);
        }

        public void DrawRectangle(Color color, float X, float Y, float Width, float Height)
        {
            Pen mypen = new Pen(color);
            IntPtr hwnd = Handle;
            Graphics grap = Graphics.FromHwnd(hwnd);
            grap.DrawRectangle(mypen, X, Y, Width, Height);
        }
        public void DrawText(string Text, float TextSize, Brush color, float X, float Y)
        {
            IntPtr hwnd = Handle;
            Graphics grap = Graphics.FromHwnd(hwnd);
            grap.TextRenderingHint = TextRenderingHint.AntiAlias;
            FontFamily fontFamily = new FontFamily("楷体");
            Font font = new Font(fontFamily, TextSize, FontStyle.Bold, GraphicsUnit.Pixel);
            grap.DrawString(Text, font, color, X, Y);
        }
        public void DrawEllipse(Color color, float X, float Y, float Width, float Height)
        {
            Pen mypen = new Pen(color);
            IntPtr hwnd = Handle;
            Graphics grap = Graphics.FromHwnd(hwnd);
            grap.DrawEllipse(mypen, X, Y, Width, Height);
        }
        public void DrawLine(Color color, float X1, float Y1, float X2, float Y2)
        {
            Pen mypen = new Pen(color);
            IntPtr hwnd = Handle;
            Graphics g = Graphics.FromHwnd(hwnd);
            g.DrawLine(mypen, X1, Y1, X2, Y2);
        }
        public void DrawLines(Color color, Point[] point)
        {
            Pen mypen = new Pen(color);
            IntPtr hwnd = Handle;
            Graphics g = Graphics.FromHwnd(hwnd);
            Point[] sbmoci = point;
            g.DrawLines(mypen, sbmoci);
        }

        /// <summary>
        /// 绘制角框
        /// </summary>
        /// <param name="x1"></param>左边
        /// <param name="y1"></param>顶边
        /// <param name="width"></param>宽度
        /// <param name="height"></param>高度
        /// <param name="color"></param>颜色
        public void CornerFrame(float x1, float y1, float width, float height, Color color)
        {
            DrawLine(color, x1, y1, (float)((double)(x1 + width) * 0.33), y1);
            DrawLine(color, (float)((double)(x1 + width) * 0.66), y1, x1 + width, y1);

            DrawLine(color, x1, y1, x1, (float)((double)(y1 + height) * 0.33));

            DrawLine(color, x1, (float)((double)(y1 + height) * 0.66), x1, y1 + height);

            DrawLine(color, x1 + width, y1, x1 + width, (float)((double)(y1 + height) * 0.33));

            DrawLine(color, x1 + width, (float)((double)(y1 + height) * 0.66), x1 + width, y1 + height);
            DrawLine(color, x1, y1 + height, (float)((double)(x1 + width) * 0.33), y1 + height);
            DrawLine(color, (float)((double)(x1 + width) * 0.66), y1 + height, x1 + width, y1 + height);
        }
        /// <summary>
        /// 竖向血条
        /// </summary>
        /// <param name="head"></param>
        /// <param name="health"></param>
        /// <param name="DefaultColor"></param>
        public void BloodStrip_Vertical(SquareBox head, int health, Color DefaultColor)
        {
            int X = head.X - 1;
            int Y = head.Y;
            Color color = DefaultColor;
            if (health >= 100)
                health = 100;
            if (health >= 90)
                color = Color.Green;
            else if (health >= 80)
                color = Color.DarkGreen;
            else if (health >= 70)
                color = Color.LightGreen;
            else if (health >= 60)
                color = Color.LightYellow;
            else if (health >= 40)
                color = Color.Yellow;
            else if (health >= 5)
                color = Color.Red;
            else color = Color.White;
            int height = head.Height / 100 * health;
            DrawRectangle(Color.Black, X - 6, Y, 6, head.Height);
            DrawRectangle(color, X - 5, Y + 1, 4, height - 2);
        }
        /// <summary>
        /// 光源矩形
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <param name="Shadows"></param>
        public void LightRectangle(float x, float y, float width, float height, Color color, int Shadows)
        {
            for (int i = 0; i < Shadows; i++)
            {
                DrawRectangle(color, x - i, y - i, width + i * 2, height + i * 2);
            }
            DrawRectangle(color, x, y, width, height);
        }
    }
}
