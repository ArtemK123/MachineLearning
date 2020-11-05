using System;
using System.Diagnostics;

namespace EightPuzzle.PerformanceCheck
{
    internal class SolverPerformanceChecker
    {
        private readonly RandomBoardGenerator randomBoardGenerator;

        public SolverPerformanceChecker()
        {
            randomBoardGenerator = new RandomBoardGenerator();
        }

        public SolverPerformanceResult Check(ISolver solver, int iterations, int boardRandomisation)
        {
            Stopwatch stopwatch = new Stopwatch();
            int totalVisitedNodes = 0;
            long totalExecutionMilliseconds = 0;

            for (int i = 0; i < iterations; i++)
            {
                Board board = randomBoardGenerator.Generate(Board.FinalBoard, boardRandomisation);

                stopwatch.Restart();
                EightPuzzleResult result = solver.Solve(board);

                totalExecutionMilliseconds += stopwatch.ElapsedMilliseconds;

                if (!result.FinalState.Board.Equals(Board.FinalBoard))
                {
                    throw new Exception("Wrong result of solving during the performance measurements");
                }

                totalVisitedNodes += result.VisitedNodesCount;
            }

            return new SolverPerformanceResult(totalVisitedNodes / iterations, (int)totalExecutionMilliseconds / iterations);
        }
    }
}