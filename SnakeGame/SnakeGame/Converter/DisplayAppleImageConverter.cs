using SnakeGame.Model;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SnakeGame.Converter
{
  public class DisplayAppleImageConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      int apple = (int)value;
      if (apple == 0)
      {
        return new BitmapImage(new Uri(string.Format("pack://application:,,,/Resources/empty.png", value)));
      }
      else
      {
        return new BitmapImage(new Uri(string.Format("pack://application:,,,/Resources/apple{0}.png",apple)));
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
