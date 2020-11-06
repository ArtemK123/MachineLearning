namespace EightPuzzle.PerformanceCheck
{
    internal class SolverPerformanceResult
    {
        public SolverPerformanceResult(int averageVisitedNodesCount, int averageExecutionMilliseconds, int averagePathLength)
        {
            AverageVisitedNodesCount = averageVisitedNodesCount;
            AverageExecutionMilliseconds = averageExecutionMilliseconds;
            AveragePathLength = averagePathLength;
        }

        public int AverageVisitedNodesCount { get; }

        public int AverageExecutionMilliseconds { get; }

        public int AveragePathLength { get; }
    }
}