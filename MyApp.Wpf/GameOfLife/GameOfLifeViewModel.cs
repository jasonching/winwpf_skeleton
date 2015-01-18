using Caliburn.Micro;
using MyApp.Wpf.GameOfLife.GameBoard;
using MyApp.Wpf.GameOfLife.GameControlPanel;

namespace MyApp.Wpf.GameOfLife
{
    public class GameOfLifeViewModel : Screen
    {
        public GameBoardViewModel GameBoard { get; set; }
        public GameControlPanelViewModel GameControlPanel { get; set; }

        // for testing
        public void Close()
        {
            GameControlPanel.Deactivated();
        }

        protected override void OnDeactivate(bool close)
        {
            if (close)
                Close();
        }
    }
}
