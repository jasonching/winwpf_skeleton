using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Wpf.GameOfLife.GameControlPanel
{
    public class GameControlPanelViewModel : PropertyChangedBase
    {
        public double BoardHeight { get; set; }
        public double BoardWidth { get; set; }

        public int WidthCount { get; set; }
        public int HeightCount { get; set; }

        public event EventHandler StartEvent;
        public event EventHandler StopEvent;
        public event EventHandler<Size> ResetEvent;
        
        public GameControlPanelViewModel()
        {
            WidthCount = 100;
            HeightCount = 100;
        }

        public void Start()
        {
            // How to wrap up the event?
            if (StartEvent != null)
                StartEvent(this, EventArgs.Empty);
        }

        public void Stop()
        {
            if (StopEvent != null)
                StopEvent(this, EventArgs.Empty);
        }

        public void Reset()
        {
            if (ResetEvent != null)
                ResetEvent(this, new Size(WidthCount, HeightCount));
        }
    }
}
