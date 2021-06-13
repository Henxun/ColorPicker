using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ColorPicker.Components"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ColorPicker.Components;assembly=ColorPicker.Components"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:TextBlockWithLabel/>
    ///
    /// </summary>
    public class TextBlockWithLabel : Control
    {
        static TextBlockWithLabel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBlockWithLabel), new FrameworkPropertyMetadata(typeof(TextBlockWithLabel)));
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof(string), typeof(TextBlockWithLabel), new PropertyMetadata(string.Empty));



        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBlockWithLabel), new PropertyMetadata(string.Empty));



        public Thickness CornerRadius
        {
            get => (Thickness)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(Thickness), typeof(TextBlockWithLabel), new PropertyMetadata(default(Thickness)));




        public SolidColorBrush LabelBrush
        {
            get => (SolidColorBrush)GetValue(LabelBrushProperty);
            set => SetValue(LabelBrushProperty, value);
        }

        public static readonly DependencyProperty LabelBrushProperty =
            DependencyProperty.Register("LabelBrush", typeof(SolidColorBrush), typeof(TextBlockWithLabel), new PropertyMetadata(default(SolidColorBrush)));




        public SolidColorBrush TextBrush
        {
            get => (SolidColorBrush)GetValue(TextBrushProperty);
            set => SetValue(TextBrushProperty, value);
        }

        public static readonly DependencyProperty TextBrushProperty =
            DependencyProperty.Register("TextBrush", typeof(SolidColorBrush), typeof(TextBlockWithLabel), new PropertyMetadata(default(SolidColorBrush)));



    }
}
