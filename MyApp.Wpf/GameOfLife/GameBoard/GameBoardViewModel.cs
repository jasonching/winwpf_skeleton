using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MyApp.Wpf.GameOfLife.GameBoard
{
    public class GameBoardViewModel : PropertyChangedBase
    {
        public GameBoard GameBoard { get; set; }

        private DispatcherTimer timer;

        public GameBoardViewModel()
        {
            GameBoard = new GameBoard();
        }

        public void Init(Size gridSize)
        {
            GameBoard.Init(gridSize);
            NotifyOfPropertyChange("GameBoard");
        }

        public void Generate(object sender, EventArgs e)
        {
            GameBoard.Generate();
            NotifyOfPropertyChange("GameBoard");
        }

        public void Reset(object sender, Size gridSize)
        {
            Init(gridSize);
        }
    }
}
