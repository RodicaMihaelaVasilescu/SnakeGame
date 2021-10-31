
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
  class Square
  {
    public double Size { get; set; } = 15;
    public int AppleId { get; set; }
    public bool IsSnake { get; internal set; }
  }
}
