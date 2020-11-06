using System.Collections.Generic;

namespace EightPuzzle.AStar
{
    internal class AStarWithoutVisitedCollectionSolver : ISolver
    {
        private readonly AStarStateFactory stateFactory;

        public AStarWithoutVisitedCollectionSolver()
        {
            stateFactory = new AStarStateFactory(new WrongPlacesHeuristicScoreComputer());
        }

        public EightPuzzleResult Solve(Board startBoard)
        {
            AStarState startingState = stateFactory.CreateStartingState(startBoard);

            var queue = new PriorityQueue<AStarState>();

            queue.Add(startingState);

            return Solve(queue);
        }

        private EightPuzzleResult Solve(PriorityQueue<AStarState> queue)
        {
            int visitedNodesCount = 0;

            while (true)
            {
                AStarState state = queue.TakeLast();
                visitedNodesCount++;

                if (state.Board.Equals(Board.FinalBoard))
                {
                    return new EightPuzzleResult(state, visitedNodesCount);
                }

                IReadOnlyCollection<Board> neighborBoards = state.Board.GetNeighbors();

                foreach (Board board in neighborBoards)
                {
                    AStarState newState = stateFactory.Create(board, state);
                    queue.Add(newState);
                }
            }
        }
    }
}