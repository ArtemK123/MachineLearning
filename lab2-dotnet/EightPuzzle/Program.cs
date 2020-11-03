namespace EightPuzzle
{
    internal static class Program
    {
        private static void Main()
        {
            RandomBoardGenerator randomBoardGenerator = new RandomBoardGenerator();
            Board startBoard = randomBoardGenerator.Generate(Board.FinalBoard, 1000);

            BfsSolver solver = new BfsSolver();

            State finalState = solver.Solve(startBoard);
            new StatePrinter().Print(finalState);
        }
    }
}