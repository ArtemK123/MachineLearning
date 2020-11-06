using System;
using EightPuzzle.AStar;
using EightPuzzle.Bfs;
using EightPuzzle.PerformanceCheck;

namespace EightPuzzle
{
    internal static class Program
    {
        private static void Main()
        {
            CheckPerformance(new BfsSolver());
            Console.WriteLine();
            CheckPerformance(new AStarSolver());
            Console.WriteLine();
            CheckPerformance(new AStarWithoutLoopsSolver(), 10, 100);
            Console.WriteLine();
            CheckPerformance(new AStarWithVisitedHashesInsideNodeSolver(), 10, 100);
        }

        private static void CheckSolver(ISolver solver, int boardRandomisation = 1000)
        {
            RandomBoardGenerator randomBoardGenerator = new RandomBoardGenerator();
            Board startBoard = randomBoardGenerator.Generate(Board.FinalBoard, boardRandomisation);

            EightPuzzleResult result = solver.Solve(startBoard);
            new StatePrinter().Print(result.FinalState);

            Console.WriteLine($"Total visited nodes: {result.VisitedNodesCount}");
        }

        private static void CheckPerformance(ISolver solver, int iterations = 10, int boardRandomisation = 1000)
        {
            SolverPerformanceChecker solverPerformanceChecker = new SolverPerformanceChecker();

            SolverPerformanceResult result = solverPerformanceChecker.Check(solver, iterations, boardRandomisation);

            Console.WriteLine($"Solver: {solver.GetType()}");
            Console.WriteLine($"Board randomisation: {boardRandomisation}");
            Console.WriteLine($"Iterations: {iterations}");
            Console.WriteLine($"Average visited nodes: {result.AverageVisitedNodesCount}");
            Console.WriteLine($"Average execution milliseconds: {result.AverageExecutionMilliseconds}");
            Console.WriteLine($"Average path lenght: {result.AveragePathLength}");
        }
    }
}