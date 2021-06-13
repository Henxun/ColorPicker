using System;
using System.Runtime.InteropServices;

namespace ColorPicker.Helper
{
    public static class NativeHelper
    {
        /// <summary>
        /// 获取当前鼠标位置
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out Point pt);

        /// <summary>
        /// 获取点的像素
        /// </summary>
        /// <param name="hdc">设备环境句柄</param>
        /// <param name="nXPos">指定要检查的像素点的逻辑X轴坐标</param>
        /// <param name="nYPos">指定要检查的像素点的逻辑Y轴坐标</param>
        /// <returns>返回值是该象像点的RGB值。如果指定的像素点在当前剪辑区之外；那么返回值是CLR_INVALID</returns>
        [DllImport("gdi32")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        /// <summary>
        /// 检索设备上下文环境（使用完上下文环境请及时释放）
        /// </summary>
        /// <param name="hwnd">设备上下文环境被检索的窗口的句柄，如果该值为IntPtr.Zero，GetDC则检索整个屏幕的设备上下文环境</param>
        /// <returns>返回指定窗口客户区的设备上下文环境</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        /// <summary>
        /// 释放设备上下文环境
        /// </summary>
        /// <param name="hwnd">指向要释放的设备上下文环境所在的窗口的句柄</param>
        /// <param name="hdc">指向要释放的设备上下文环境的句柄。</param>
        /// <returns>如果释放成功，则返回值为1；如果没有释放成功，则返回值为0</returns>
        [DllImport("user32.dll")]
        public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        ///   <summary>
        ///  设置目标窗体大小，位置
        ///   </summary>
        ///   <param name="hWnd"> 目标句柄 </param>
        ///   <param name="x"> 目标窗体新位置X轴坐标 </param>
        ///   <param name="y"> 目标窗体新位置Y轴坐标 </param>
        ///   <param name="nWidth"> 目标窗体新宽度 </param>
        ///   <param name="nHeight"> 目标窗体新高度 </param>
        ///   <param name="bRepaint"> 是否刷新窗体 </param>
        ///   <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);
    }


    public struct Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

    }
}
