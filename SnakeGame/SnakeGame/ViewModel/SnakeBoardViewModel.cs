using SnakeGame.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Data;

namespace SnakeGame.ViewModel
{
  class SnakeBoardViewModel : BindableBase
  {
    private ObservableCollection<ObservableCollection<Square>> _board;

    public ObservableCollection<ObservableCollection<Square>> Board
    {
      get { return _board; }

      set
      {
        if (_board == value) return;
        _board = value;
        OnPropertyChanged();
      }
    }

    public int Score
    {
      get { return _score; }

      set
      {
        if (_score == value) return;
        _score = value;
        OnPropertyChanged();
      }
    }

    List<int> column = new List<int> { -1, 1, 0, 0 };
    List<int> line = new List<int> { 0, 0, -1, 1 };


    public List<Coordinate> Snake { get; private set; }

    public int Length = 16;
    public int Width = 34;

    private Timer timer;
    public int Direction = DirectionConstants.Right;
    private int _score;

    public SnakeBoardViewModel()
    {
      InitializeBoard();
      InitializeTimer();
    }

    private void PlaceRandomApple()
    {
      var random = new Random();
      int x = random.Next(Length);
      int y = random.Next(Width);
      Board[x][y].AppleId = Score % 9 + 1;

    }

    private void InitializeTimer()
    {
      timer = new Timer(300);
      timer.Elapsed += async (sender, e) => await HandleSnakeTimer();
      timer.Start();
    }

    private async Task HandleSnakeTimer()
    {
      await Task.Run(() =>
      {
        Application.Current.Dispatcher.Invoke((Action)delegate
        {
          Move();
        });
      });
    }


    private void Move()
    {
      var coordinatesToAdd = new Coordinate(Snake.Last().X + line[Direction], Snake.Last().Y + column[Direction]);
      if (!AreCoordonatesValid(coordinatesToAdd))
      {
        timer.Stop();
        MessageBox.Show("Game over!");
        InitializeBoard();
        InitializeTimer();
        return;
      }
      Board[coordinatesToAdd.X][coordinatesToAdd.Y].IsSnake = true;
      Snake.Add(coordinatesToAdd);

      var coordinatesToRemove = Snake.First();
      if (Board[coordinatesToAdd.X][coordinatesToAdd.Y].AppleId != 0)
      {
        Board[coordinatesToAdd.X][coordinatesToAdd.Y].AppleId = 0;
        PlaceRandomApple();
        Score++;
      }
      else
      {
        Board[coordinatesToRemove.X][coordinatesToRemove.Y].IsSnake = false;
        Snake.Remove(coordinatesToRemove);
      }

      CollectionViewSource.GetDefaultView(Board).Refresh();
    }

    private bool AreCoordonatesValid(Coordinate coord)
    {
      return coord.X >= 0 && coord.Y >= 0 && coord.Y < Width && coord.X < Length && !Snake.Any(c => c.X == coord.X && c.Y == coord.Y);
    }

    private void InitializeBoard()
    {
      Direction = DirectionConstants.Right;
      Score = 0;
      CreateBoard();
      PlaceSnake();
      PlaceRandomApple();
    }

    private void CreateBoard()
    {
      Board = new ObservableCollection<ObservableCollection<Square>>();
      for (int i = 0; i < Length; i++)
      {
        Board.Add(new ObservableCollection<Square>());
        for (int j = 0; j < Width; j++)
        {
          Board.Last().Add(new Square());
        }
      }
    }

    private void PlaceSnake()
    {
      Snake = new List<Coordinate>();
      for (int i = 1; i < 10; i++)
      {
        Snake.Add(new Coordinate(0, i));
      }

      foreach (var coordinate in Snake)
      {
        Board[coordinate.X][coordinate.Y].IsSnake = true;
      }
    }
  }
}
