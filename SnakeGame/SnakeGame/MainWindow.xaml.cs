using SnakeGame.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakeGame
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      ViewModel = new SnakeBoardViewModel();
      SnakeBoardControl.DataContext = ViewModel;
    }

    private SnakeBoardViewModel ViewModel { get; set; }

    List<Key> AllDirections = new List<Key> { Key.Left, Key.Right, Key.Up, Key.Down };

    Key CurrentKey = Key.Right;

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
      if (AllDirections.Contains(e.Key))
      {
        if (CurrentKey == Key.Right && e.Key == Key.Left || CurrentKey == Key.Left && e.Key == Key.Right ||
          CurrentKey == Key.Up && e.Key == Key.Down || CurrentKey == Key.Down && e.Key == Key.Up)
        {
          return;
        }

        CurrentKey = e.Key;
        ViewModel.Direction = AllDirections.IndexOf(e.Key);
      }
    }
  }
}
