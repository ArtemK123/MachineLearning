using System;
using System.Collections.Generic;
using System.Linq;

namespace EightPuzzle
{
    internal class RandomBoardGenerator
    {
        private readonly Random random;

        public RandomBoardGenerator()
        {
            random = new Random();
        }

        public Board Generate(Board startBoard, int steps)
        {
            Board currentBoard = startBoard;

            for (int i = 0; i < steps; i++)
            {
                IReadOnlyCollection<Board> neighbors = currentBoard.GetNeighbors();
                int nextBoardIndex = random.Next(neighbors.Count);
                currentBoard = neighbors.ElementAt(nextBoardIndex);
            }

            return currentBoard;
        }
    }
}