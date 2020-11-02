using System;

namespace EightPuzzle
{
    internal static class Program
    {
        private static void Main()
        {
            Board startBoard = new Board(new[,]
            {
                { 1, 0, 3 },
                { 4, 2, 5 },
                { 7, 8, 6 }
            });

            BfsSolver solver = new BfsSolver();

            State finalState = solver.Solve(startBoard);
            PrintStateHistory(finalState);
        }

        private static void PrintStateHistory(State state)
        {
            if (state == null)
            {
                return;
            }

            PrintStateHistory(state.PreviousState);
            Console.WriteLine("________________________________");
            Console.WriteLine(state.Board.ToString());
        }
    }
}