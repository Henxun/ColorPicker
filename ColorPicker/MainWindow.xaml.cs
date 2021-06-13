using ColorPicker.Helper;
using ColorPicker.ViewModel;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using Point = ColorPicker.Helper.Point;

namespace ColorPicker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Unloaded += MainWindow_Unloaded;
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            _hook.Stop();
            _hook = null;
        }

        private const int OFFSET = 20;

        private MouseHook _hook;

        private ColorPickerUI _colorPickerUi;

        private ColorViewModel _viewModel;

        private IntPtr _ptr;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _ptr = new WindowInteropHelper(this).Handle;
            this.Dispatcher.Invoke(SetColor);
            this.KeyDown += MainWindow_KeyDown;

            _hook = new MouseHook();
            _hook.OnLeftMouseButtonDown += _hook_OnLeftMouseButtonDown;
            _hook.OnMouseMove += _hook_OnMouseMove;
            _hook.Start();
        }

        private void _hook_OnMouseMove(object sender, EventArgs e)
        {
            Task.Delay(100);
            this.Dispatcher.Invoke(SetColor);
        }

        private void _hook_OnLeftMouseButtonDown(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (this.Visibility == Visibility.Visible)
                {
                    _colorPickerUi ??= new ColorPickerUI(this);
                    _viewModel ??= new ColorViewModel();
                    var colorModel = new ColorModel()
                    {
                        Color = (SolidColorBrush) ColorRect.Fill
                    };
                    _viewModel.Colors.Add(colorModel);
                    _viewModel.SelectedItem = colorModel;
                    _colorPickerUi.DataContext = _viewModel;
                    _colorPickerUi.Show();
                    this.Hide();
                }
            });
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            //ESC键退出
            if (e.Key == Key.Escape)
            {
                if(this.Visibility == Visibility.Visible)
                    Application.Current.Shutdown(); 
            }
        }

        private void SetColor()
        {
            if (this.Visibility == Visibility.Visible)
            {
                NativeHelper.GetCursorPos(out Point point);

                int left, top;
                if (point.X >= SystemParameters.WorkArea.Width - SystemParameters.Border - this.ActualWidth)
                {
                    left = point.X - (int)this.ActualWidth;
                }
                else
                {
                    left = point.X + OFFSET;
                }

                if (point.Y >= SystemParameters.WorkArea.Height - SystemParameters.Border - this.ActualHeight)
                {
                    top = point.Y - (int)this.ActualHeight;
                }
                else
                {
                    top = point.Y + OFFSET;
                }

                NativeHelper.MoveWindow(_ptr, left, top, (int)(this.ActualWidth), (int)(this.ActualHeight), false);
                
                var color = GetPiexlColor(point.X, point.Y);
                ColorRect.Fill = new SolidColorBrush(color);
                ColorText.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            }
        }

        private Color GetPiexlColor(int x, int y)
        {
            IntPtr ptr = NativeHelper.GetDC(IntPtr.Zero);
            uint pixel = NativeHelper.GetPixel(ptr, x, y);
            NativeHelper.ReleaseDC(IntPtr.Zero, ptr);
            byte b = (byte)((pixel >> 0x10) & 0xffL);
            byte g = (byte)((pixel >> 8) & 0xffL);
            byte r = (byte)(pixel & 0xffL);
            return Color.FromRgb(r, g, b);
        }
    }
}
