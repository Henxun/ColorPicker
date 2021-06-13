using System;
using System.Windows;
using System.Windows.Controls;

namespace ColorPicker.Themes
{
    public partial class Generic
    {
        private void Copy_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = string.Empty;
                var parent = LogicalTreeHelper.GetParent((DependencyObject)sender);
                var children = LogicalTreeHelper.GetChildren(parent);
                foreach (var child in children)
                {
                    if (child is TextBlock tb)
                    {
                        text = tb.Text;
                        break;
                    }
                }

               Clipboard.SetDataObject(text);
            }
            catch (Exception exception)
            {
                //Ignore
            }
        }
    }
}
