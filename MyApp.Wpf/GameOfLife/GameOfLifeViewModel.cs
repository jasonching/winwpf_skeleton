using Caliburn.Micro;
using MyApp.Wpf.GameOfLife.GameBoard;
using MyApp.Wpf.GameOfLife.GameControlPanel;

namespace MyApp.Wpf.GameOfLife
{
    public class GameOfLifeViewModel : Screen
    {
        public GameBoardViewModel GameBoard { get; set; }
        public GameControlPanelViewModel GameControlPanel { get; set; }

        protected override void OnDeactivate(bool close)
        {
            if (close)
                GameControlPanel.Deactivated();
        }
    }
}
