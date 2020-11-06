using System.Collections.Generic;

namespace EightPuzzle.AStar
{
    internal class AStarStateWithVisitedHashesFactory
    {
        private readonly AStarStateFactory baseStateFactory;

        public AStarStateWithVisitedHashesFactory(AStarStateFactory baseStateFactory)
        {
            this.baseStateFactory = baseStateFactory;
        }

        public AStarStateWithVisitedHashes CreateStartingState(Board startingBoard)
        {
            AStarState baseState = baseStateFactory.Create(startingBoard, null);
            return new AStarStateWithVisitedHashes(baseState, new HashSet<int>(startingBoard.GetHashCode()));
        }

        public AStarStateWithVisitedHashes Create(Board board, AStarStateWithVisitedHashes previousState)
        {
            AStarState baseState = baseStateFactory.Create(board, previousState);
            var hashSet = new HashSet<int>(previousState.VisitedStatesHashes);
            hashSet.Add(board.GetHashCode());
            return new AStarStateWithVisitedHashes(baseState, hashSet);
        }
    }
}