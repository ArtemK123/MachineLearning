using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace EightPuzzle.PerformanceCheck
{
    internal class SolverPerformanceChecker
    {
        private readonly RandomBoardGenerator randomBoardGenerator;
        private readonly PathLengthCalculator pathLengthCalculator;

        public SolverPerformanceChecker()
        {
            randomBoardGenerator = new RandomBoardGenerator();
            pathLengthCalculator = new PathLengthCalculator();
        }

        public SolverPerformanceResult Check(ISolver solver, int iterations, int boardRandomisation)
        {
            int totalVisitedNodes = 0;
            int totalExecutionMilliseconds = 0;
            int totalPathLenght = 0;

            Parallel.For(0, iterations, _ =>
            {
                Board board = randomBoardGenerator.Generate(Board.FinalBoard, boardRandomisation);

                Stopwatch stopwatch = Stopwatch.StartNew();
                EightPuzzleResult result = solver.Solve(board);

                stopwatch.Stop();

                Interlocked.Add(ref totalExecutionMilliseconds, (int)stopwatch.ElapsedMilliseconds);

                if (!result.FinalState.Board.Equals(Board.FinalBoard))
                {
                    throw new Exception("Wrong result of solving during the performance measurements");
                }

                Interlocked.Add(ref totalVisitedNodes, result.VisitedNodesCount);
                Interlocked.Add(ref totalPathLenght, pathLengthCalculator.CalculatePathLenght(result.FinalState));
            });

            return new SolverPerformanceResult(
                totalVisitedNodes / iterations,
                totalExecutionMilliseconds / iterations,
                totalPathLenght / iterations);
        }
    }
}