using ColorPicker.ViewModel;
using System.Linq;
using System.Windows;

namespace ColorPicker
{
    /// <summary>
    /// ColorPickerUI.xaml 的交互逻辑
    /// </summary>
    public partial class ColorPickerUI : Window
    {
        private readonly MainWindow mainWindow;

        public ColorPickerUI(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Icon_OnClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as ColorViewModel;

            if (null == viewModel)
                return;

            viewModel.Colors.Remove(viewModel.SelectedItem);
            viewModel.SelectedItem = viewModel.Colors.FirstOrDefault();
        }
    }
}
