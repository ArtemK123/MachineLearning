using System.Collections.Generic;

namespace EightPuzzle.AStar
{
    internal class AStarWithVisitedHashesInsideNodeSolver : ISolver
    {
        private readonly AStarStateWithVisitedHashesFactory stateFactory;

        public AStarWithVisitedHashesInsideNodeSolver()
        {
            stateFactory = new AStarStateWithVisitedHashesFactory(new AStarStateFactory(new WrongPlacesHeuristicScoreComputer()));
        }

        public EightPuzzleResult Solve(Board startBoard)
        {
            AStarStateWithVisitedHashes startingState = stateFactory.CreateStartingState(startBoard);

            var queue = new PriorityQueue<AStarStateWithVisitedHashes>();

            queue.Add(startingState);

            return Solve(queue);
        }

        private EightPuzzleResult Solve(PriorityQueue<AStarStateWithVisitedHashes> queue)
        {
            int visitedNodesCount = 0;

            while (true)
            {
                AStarStateWithVisitedHashes state = queue.TakeLast();
                visitedNodesCount++;

                if (state.Board.Equals(Board.FinalBoard))
                {
                    return new EightPuzzleResult(state, visitedNodesCount);
                }

                IReadOnlyCollection<Board> neighborBoards = state.Board.GetNeighbors();

                foreach (Board board in neighborBoards)
                {
                    if (!state.BoardWasVisited(board))
                    {
                        AStarStateWithVisitedHashes newState = stateFactory.Create(board, state);
                        queue.Add(newState);
                    }
                }
            }
        }
    }
}