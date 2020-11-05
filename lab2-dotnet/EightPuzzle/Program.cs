using System;
using EightPuzzle.AStar;

namespace EightPuzzle
{
    internal static class Program
    {
        private static void Main()
        {
            RandomBoardGenerator randomBoardGenerator = new RandomBoardGenerator();
            Board startBoard = randomBoardGenerator.Generate(Board.FinalBoard, 50);

            ISolver solver = new AStarSolver();

            EightPuzzleResult result = solver.Solve(startBoard);
            new StatePrinter().Print(result.FinalState);

            Console.WriteLine($"Total visited nodes: {result.VisitedNodesCount}");
        }
    }
}