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
        private static readonly Random rnd = new Random();
        private static readonly double rndDensity = 0.5;

        public IList<BoardUnit> Units { get; set; }
        public Size GridSize { get; set; }

        private double boardWidth;
        private double boardHeight;

        public double BoardWidth 
        {
            get { return boardWidth; }
            set
            {
                boardWidth = value;
                SetUnitSize();
            }
        }

        public double BoardHeight
        {
            get { return boardHeight; }
            set
            {
                boardHeight = value;
                SetUnitSize();
            }
        }

        public double UnitWidth { get; set; }
        public double UnitHeight { get; set; }

        private DispatcherTimer timer;

        public GameBoardViewModel()
        {
            Units = new ObservableCollection<BoardUnit>();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Generate();
        }

        private void Generate()
        {
            var currentState = Units.Select(u => u.IsAlive).ToList();

            for(var i=0; i<Units.Count; i++)
            {
                var neighborhood = GetNeighborhood(currentState, i);
                var aliveCount = neighborhood.Where(n => n).Count();

                if (Units[i].IsAlive)
                {
                    if (aliveCount <= 1 || aliveCount >= 4)
                        Units[i].IsAlive = false;
                }
                else
                {
                    if (aliveCount == 3)
                        Units[i].IsAlive = true;
                }
            }
        }

        private IEnumerable<bool> GetNeighborhood(List<bool> currentState, int position)
        {
            int[] positions;

            var edgeCheck = (position + 1) % GridSize.Width;

            if (edgeCheck == 1)
            {
                positions = new int[] {
                    position + 1,
                    position + GridSize.Width,
                    position + GridSize.Width + 1,
                    position - GridSize.Width,
                    position - GridSize.Width + 1
                };
            }
            else if (edgeCheck == 0)
            {
                positions = new int[] {
                    position - 1,
                    position + GridSize.Width,
                    position + GridSize.Width - 1,
                    position - GridSize.Width,
                    position - GridSize.Width - 1
                };
            }
            else
            {
                positions = new int[] {
                    position + 1,
                    position - 1,
                    position + GridSize.Width,
                    position + GridSize.Width + 1,
                    position + GridSize.Width - 1,
                    position - GridSize.Width,
                    position - GridSize.Width + 1,
                    position - GridSize.Width - 1
                };
            }

            return positions.Where(p => p>0 && p<currentState.Count).Select(p => currentState[p]).ToArray();
        }

        public void Init(Size gridSize)
        {
            GridSize = gridSize;

            Units.Clear();

            for (var i = 0; i < gridSize.Width * gridSize.Height; i++)
                Units.Add(new BoardUnit 
                { 
                    Position = new System.Windows.Point(GetXPos(i), GetYPos(i)),
                    IsAlive = rnd.NextDouble() <= rndDensity
                });

            SetUnitSize();
        }

        private void SetUnitSize()
        {
            UnitWidth = BoardWidth / GridSize.Width;
            UnitHeight = BoardHeight / GridSize.Height;

            //UnitWidth = 2;
            //UnitHeight = 2;

            NotifyOfPropertyChange("UnitWidth");
            NotifyOfPropertyChange("UnitHeight");
        }

        public void Start(object sender, EventArgs e)
        {
            timer.Start();
        }

        public void Stop(object sender, EventArgs e)
        {
            timer.Stop();
        }

        public void Reset(object sender, Size gridSize)
        {
            Init(gridSize);
            timer.Stop();
        }

        private int GetXPos(int i)
        {
            return i - (GetYPos(i) * GridSize.Height);
        }

        private int GetYPos(int i)
        {
            return Convert.ToInt32(Math.Truncate(Convert.ToDouble(i / GridSize.Height)));
        }
    }
}
