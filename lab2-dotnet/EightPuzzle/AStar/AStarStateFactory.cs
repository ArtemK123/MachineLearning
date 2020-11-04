namespace EightPuzzle.AStar
{
    internal class AStarStateFactory
    {
        private readonly WrongPlacesHeuristicScoreComputer wrongPlacesHeuristicScoreComputer;

        public AStarStateFactory(WrongPlacesHeuristicScoreComputer wrongPlacesHeuristicScoreComputer)
        {
            this.wrongPlacesHeuristicScoreComputer = wrongPlacesHeuristicScoreComputer;
        }

        public AStarState CreateStartingState(Board startingBoard)
        {
            return Create(startingBoard, null);
        }

        public AStarState Create(Board board, AStarState previousState)
        {
            int currentScore = wrongPlacesHeuristicScoreComputer.Compute(board);
            int previousScore = previousState?.TotalHeuristicScore ?? 0;
            int totalScore = previousScore + currentScore;
            return new AStarState(board, previousState, currentScore, totalScore);
        }
    }
}