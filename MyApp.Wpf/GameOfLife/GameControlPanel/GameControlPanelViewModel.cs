using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MyApp.Wpf.GameOfLife.GameControlPanel
{
    public class GameControlPanelViewModel : PropertyChangedBase
    {
        public double BoardHeight { get; set; }
        public double BoardWidth { get; set; }

        public int WidthCount { get; set; }
        public int HeightCount { get; set; }

        public event EventHandler GenerateEvent;
        public event EventHandler<Size> ResetEvent;

        private DispatcherTimer timer;

        public GameControlPanelViewModel()
        {
            WidthCount = 100;
            HeightCount = 100;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick += timer_Tick;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (GenerateEvent != null)
                GenerateEvent(this, EventArgs.Empty);
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public void Reset()
        {
            timer.Stop();

            if (ResetEvent != null)
                ResetEvent(this, new Size(WidthCount, HeightCount));
        }

        public void Deactivated()
        {
            timer.Stop();
        }
    }
}
