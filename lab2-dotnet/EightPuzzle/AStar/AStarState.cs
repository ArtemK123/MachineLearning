using System;

namespace EightPuzzle.AStar
{
    internal class AStarState : State, IComparable<AStarState>
    {
        public AStarState(Board board, State previousState, int currentHeuristicScore, int totalHeuristicScore)
            : base(board, previousState)
        {
            CurrentHeuristicScore = currentHeuristicScore;
            TotalHeuristicScore = totalHeuristicScore;
        }

        public int CurrentHeuristicScore { get; }

        public int TotalHeuristicScore { get; }

        public int CompareTo(AStarState other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return TotalHeuristicScore.CompareTo(other.TotalHeuristicScore);
        }
    }
}