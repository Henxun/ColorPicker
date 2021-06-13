using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace ColorPicker.ViewModel
{
    public class ColorViewModel : INotifyPropertyChanged
    {
        public ColorViewModel()
        {
            _colors = new ObservableCollection<ColorModel>();
        }

        private ObservableCollection<ColorModel> _colors;

        public ObservableCollection<ColorModel> Colors
        {
            get => _colors;
            set
            {
                _colors = value;
                OnPropertyChanged("Colors");
            }
        }

        private ColorModel _selectedItem;

        public ColorModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (null != _selectedItem)
                {
                    HexText = _selectedItem.Color.Color.ToString();
                    RgbText =
                        $"rgb({_selectedItem.Color.Color.R},{_selectedItem.Color.Color.G},{_selectedItem.Color.Color.B})";
                    HslText = ComputeHslFromColor(_selectedItem.Color.Color);
                }
                IsVisible = _selectedItem != null;
                OnPropertyChanged("SelectedItem");
            }
        }

        private bool _isVisible;

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        private string _rgbText;

        public string RgbText
        {
            get => _rgbText;
            set
            {
                _rgbText = value;
                OnPropertyChanged("RgbText");
            }
        }

        private string _hexText;

        public string HexText
        {
            get => _hexText;
            set
            {
                _hexText = value;
                OnPropertyChanged("HexText");
            }
        }

        private string _hslText;

        public string HslText
        {
            get => _hslText;
            set
            {
                _hslText = value;
                OnPropertyChanged("HslText");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 计算hsl
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private string ComputeHslFromColor(Color color)
        {
            byte r = color.R, g = color.G, b = color.G;
            byte min = Math.Min(Math.Min(r, g), b);
            byte max = Math.Max(Math.Max(r, g), b);

            int h;
            if (min == max)
            {
                h = 0;
            }
            else if (max == r && g >= b)
            {
                h = 60 * (g - b) / (max - min) + 0;
            }
            else if (max == r && g < b)
            {
                h = 60 * (g - b) / (max - min) + 360;
            }
            else if (max == g)
            {
                h = 60 * (b - r) / (max - min) + 120;
            }
            else
            {
                h = 60 * (r - g) / (max - min) + 240;
            }

            int s;
            if (max == 0)
            {
                s = 0;
            }
            else
            {
                s = 1 - min / max;
            }

            int l = (min + max) / 2;
            return $"hsl({h},{s},{l})";
        }
    }

    public class ColorModel
    {
        public SolidColorBrush Color { get; set; }
    }
}