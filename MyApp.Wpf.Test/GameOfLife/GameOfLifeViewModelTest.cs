using MyApp.Wpf.GameOfLife;
using MyApp.Wpf.Test.Nunit;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MyApp.Wpf.Test.GameOfLife
{
    [TestFixture]
    public class GameOfLifeViewModelTest : AbstractTest
    {
        [Test]
        public void ResetTest()
        {
            // Given
            var gameOfLifeViewModel = WindsorContainer.Resolve<GameOfLifeViewModel>();

            // When
            gameOfLifeViewModel.GameControlPanel.Reset();
            var currentBoard = gameOfLifeViewModel.GameBoard.GameBoard.Units.Select(u => u.IsAlive).ToArray();
            
            gameOfLifeViewModel.GameControlPanel.Reset();
            var newBoard = gameOfLifeViewModel.GameBoard.GameBoard.Units.Select(u => u.IsAlive).ToArray();

            // Then
            Assert.IsTrue(currentBoard.Any(u => u), "Some units has to be alive");
            Assert.AreEqual(currentBoard.Count(), newBoard.Count(), "GameBoard should have the same size after reset");
            Assert.IsFalse(ListAreEqual(currentBoard, newBoard), "GameBoard should have changed after reset");
        }

        [Test]
        public void StartTest()
        {
            // Given
            var gameOfLifeViewModel = WindsorContainer.Resolve<GameOfLifeViewModel>();

            // When
            gameOfLifeViewModel.GameControlPanel.Reset();
            var currentBoard = gameOfLifeViewModel.GameBoard.GameBoard.Units.Select(u => u.IsAlive).ToArray();

            gameOfLifeViewModel.GameBoard.Generate(null, null);
            var newBoard = gameOfLifeViewModel.GameBoard.GameBoard.Units.Select(u => u.IsAlive).ToArray();

            gameOfLifeViewModel.Close();

            // Then
            Assert.AreEqual(currentBoard.Count(), newBoard.Count(), "GameBoard should have the same size after reset");
            Assert.IsFalse(ListAreEqual(currentBoard, newBoard), "GameBoard should have changed after reset");
        }

        private bool ListAreEqual(IList<bool> listA, IList<bool> listB)
        {
            for(var i=0; i<listA.Count(); i++)
            {
                if (listA[i] != listB[i])
                    return false;
            }

            return true;
        }
    }
}
