using EightPuzzle.AStar;

namespace EightPuzzle
{
    internal static class Program
    {
        private static void Main()
        {
            RandomBoardGenerator randomBoardGenerator = new RandomBoardGenerator();
            // Board startBoard = randomBoardGenerator.Generate(Board.FinalBoard, 1000);
            Board startBoard = randomBoardGenerator.Generate(Board.FinalBoard, 50);

            ISolver solver = new AStarSolver();

            State finalState = solver.Solve(startBoard);
            new StatePrinter().Print(finalState);
        }
    }
}