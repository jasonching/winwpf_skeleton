using Caliburn.Micro;
using System.Windows;

namespace MyApp.Wpf.GameOfLife.GameBoard
{
    public class BoardUnit : PropertyChangedBase
    {
        public bool IsAlive { get; set; }
    }
}
