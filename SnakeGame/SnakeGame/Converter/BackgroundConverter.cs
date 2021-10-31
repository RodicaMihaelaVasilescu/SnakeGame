using SnakeGame.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SnakeGame.Converter
{
  public class BackgroundConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var IsSnake = (bool)value;
      if (IsSnake)
      {
       return new BrushConverter().ConvertFrom("#2ee809");
      }
      else
      {
        return new SolidColorBrush(Colors.Transparent);
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value;
    }
  }
}
