using Caliburn.Micro;
using System.Windows;

namespace MyApp.Wpf.GameOfLife.GameBoard
{
    public class BoardUnit : PropertyChangedBase
    {
        public Point Position { get; set; }

        private bool isAlive;
        public bool IsAlive 
        {
            get { return isAlive; }
            set
            {
                isAlive = value;
                NotifyOfPropertyChange("IsAlive");
            }
        }
    }
}
