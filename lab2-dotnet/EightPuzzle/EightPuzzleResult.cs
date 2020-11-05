namespace EightPuzzle
{
    internal class EightPuzzleResult
    {
        public EightPuzzleResult(State finalState, int visitedNodesCount)
        {
            FinalState = finalState;
            VisitedNodesCount = visitedNodesCount;
        }

        public State FinalState { get; }

        public int VisitedNodesCount { get; }
    }
}