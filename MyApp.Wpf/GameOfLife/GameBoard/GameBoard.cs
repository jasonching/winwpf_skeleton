using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyApp.Wpf.GameOfLife.GameBoard
{
    public class GameBoard
    {
        private static readonly Random rnd = new Random();
        private static readonly double rndDensity = 0.5;

        public IList<BoardUnit> Units { get; set; }
        public Size GridSize { get; set; }

        public void Init(Size gridSize)
        {
            Units = new List<BoardUnit>();

            GridSize = gridSize;

            for (var i = 0; i < gridSize.Width * gridSize.Height; i++)
                Units.Add(new BoardUnit
                {
                    IsAlive = rnd.NextDouble() <= rndDensity
                });
        }

        public void Generate()
        {
            var currentState = Units.Select(u => u.IsAlive).ToList();

            for (var i = 0; i < Units.Count; i++)
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

            return positions.Where(p => p > 0 && p < currentState.Count).Select(p => currentState[p]).ToArray();
        }
    }
}
