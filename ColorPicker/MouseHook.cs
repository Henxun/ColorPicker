using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace ColorPicker
{
    public class MouseHook
    {
        private const int WM_MOUSEMOVE = 0x200;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_RBUTTONUP = 0x205;
        private const int WM_MBUTTONUP = 0x208;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int WM_RBUTTONDBLCLK = 0x206;
        private const int WM_MBUTTONDBLCLK = 0x209;

        /// <summary>
        /// 点
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;
        }

        /// <summary>
        /// 钩子结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            public POINT pt;
            public int hWnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        public const int WH_MOUSE_LL = 14; // mouse hook constant

        // 装置钩子的函数
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        // 卸下钩子的函数
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        // 下一个钩挂的函数
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        // 全局的鼠标事件
        public event MouseEventHandler OnMouseActivity;

        // 钩子回调函数
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        public event EventHandler OnLeftMouseButtonDown;

        public event EventHandler OnMouseMove;

        // 声明鼠标钩子事件类型
        private HookProc _mouseHookProcedure;
        private static int _hMouseHook = 0; // 鼠标钩子句柄

        /// <summary>
        /// 构造函数
        /// </summary>
        public MouseHook()
        {

        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~MouseHook()
        {
            Stop();
        }

        /// <summary>
        /// 启动全局钩子
        /// </summary>
        public void Start()
        {
            // 安装鼠标钩子
            if (_hMouseHook == 0)
            {
                // 生成一个HookProc的实例.
                _mouseHookProcedure = new HookProc(MouseHookProc);

                _hMouseHook = SetWindowsHookEx(WH_MOUSE_LL, _mouseHookProcedure,
                    Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]), 0);

                //假设装置失败停止钩子
                if (_hMouseHook == 0)
                {
                    Stop();
                    throw new Exception("SetWindowsHookEx failed.");
                }
            }
        }

        /// <summary>
        /// 停止全局钩子
        /// </summary>
        public void Stop()
        {
            bool retMouse = true;

            if (_hMouseHook != 0)
            {
                retMouse = UnhookWindowsHookEx(_hMouseHook);
                _hMouseHook = 0;
            }

            // 假设卸下钩子失败
            if (!(retMouse))
                throw new Exception("UnhookWindowsHookEx failed.");
        }

        /// <summary>
        /// 鼠标钩子回调函数
        /// </summary>
        private int MouseHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            // 假设正常执行而且用户要监听鼠标的消息
            if ((nCode >= 0))
            {
                switch (wParam)
                {
                    case WM_LBUTTONDOWN:
                        OnLeftMouseButtonDown?.Invoke(null,null);
                        break;
                    case WM_MOUSEMOVE:
                        OnMouseMove?.Invoke(null,null);
                        break;
                }

                // 从回调函数中得到鼠标的信息
                //MouseHookStruct MyMouseHookStruct =
                //    (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
                //MouseEventArgs e = new MouseEventArgs(Mouse.PrimaryDevice, (int)DateTime.Now.Ticks);

                // 假设想要限制鼠标在屏幕中的移动区域能够在此处设置
                // 后期须要考虑实际的x、y的容差.
                //if (!Screen.PrimaryScreen.Bounds.Contains(e.X, e.Y))
                //{
                //    //return 1;
                //}

                //OnMouseActivity(this, e);
            }

            // 启动下一次钩子
            return CallNextHookEx(_hMouseHook, nCode, wParam, lParam);
        }
    }
}