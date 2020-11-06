using System;
using System.Threading;
using System.Threading.Tasks;
using EightPuzzle.AStar;
using EightPuzzle.Bfs;
using EightPuzzle.PerformanceCheck;

namespace EightPuzzle
{
    internal static class Program
    {
        private static void Main()
        {
            CheckResultsOptimality();
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

        private static void CheckResultsOptimality()
        {
            int iterations = 4;
            int optimalBfsLenghtCount = 0;
            int optimalAStarLenghtCount = 0;

            var pathLengthCalculator = new PathLengthCalculator();

            Parallel.For(0, iterations, _ =>
            {
                Board board = new RandomBoardGenerator().Generate(Board.FinalBoard, 1000);

                var bfsResult = new BfsSolver().Solve(board);
                var aStarResult = new AStarSolver().Solve(board);
                var optimalResult = new AStarWithoutLoopsSolver().Solve(board);

                int bfsResultLenght = pathLengthCalculator.CalculatePathLenght(bfsResult.FinalState);
                int aStarResultLenght = pathLengthCalculator.CalculatePathLenght(aStarResult.FinalState);
                int optimalResultLenght = pathLengthCalculator.CalculatePathLenght(optimalResult.FinalState);

                if (bfsResultLenght == optimalResultLenght)
                {
                    Interlocked.Increment(ref optimalBfsLenghtCount);
                }

                if (aStarResultLenght == optimalResultLenght)
                {
                    Interlocked.Increment(ref optimalAStarLenghtCount);
                }
            });

            Console.WriteLine($"Iterations: {iterations}");
            Console.WriteLine($"Optimal results from BFS: {optimalBfsLenghtCount}");
            Console.WriteLine($"Optimal results from AStar: {optimalAStarLenghtCount}");
        }
    }
}