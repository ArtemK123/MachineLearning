namespace EightPuzzle.PerformanceCheck
{
    internal class SolverPerformanceResult
    {
        public SolverPerformanceResult(int averageVisitedNodesCount, int averageExecutionMilliseconds)
        {
            AverageVisitedNodesCount = averageVisitedNodesCount;
            AverageExecutionMilliseconds = averageExecutionMilliseconds;
        }

        public int AverageVisitedNodesCount { get; }

        public int AverageExecutionMilliseconds { get; }
    }
}