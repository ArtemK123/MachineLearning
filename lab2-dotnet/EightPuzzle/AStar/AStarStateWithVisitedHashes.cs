using System;
using System.Collections.Generic;

namespace EightPuzzle.AStar
{
    internal class AStarStateWithVisitedHashes : AStarState, IComparable<AStarStateWithVisitedHashes>
    {
        public AStarStateWithVisitedHashes(Board board, State previousState, int currentHeuristicScore, int totalHeuristicScore, HashSet<int> visitedStatesHashes)
            : base(board, previousState, currentHeuristicScore, totalHeuristicScore)
        {
            VisitedStatesHashes = visitedStatesHashes;
        }

        public AStarStateWithVisitedHashes(AStarState baseState, HashSet<int> visitedStatesHashes)
            : base(baseState.Board, baseState.PreviousState, baseState.CurrentHeuristicScore, baseState.TotalHeuristicScore)
        {
            VisitedStatesHashes = visitedStatesHashes;
        }

        public HashSet<int> VisitedStatesHashes { get; }

        public bool BoardWasVisited(Board board)
        {
            return VisitedStatesHashes.Contains(board.GetHashCode());
        }

        public override int GetHashCode()
        {
            return Board.GetHashCode();
        }

        public int CompareTo(AStarStateWithVisitedHashes other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var aStarStateComparison = base.CompareTo(other);
            if (aStarStateComparison != 0) return aStarStateComparison;
            var currentHeuristicScoreComparison = CurrentHeuristicScore.CompareTo(other.CurrentHeuristicScore);
            if (currentHeuristicScoreComparison != 0) return currentHeuristicScoreComparison;
            return TotalHeuristicScore.CompareTo(other.TotalHeuristicScore);
        }
    }
}