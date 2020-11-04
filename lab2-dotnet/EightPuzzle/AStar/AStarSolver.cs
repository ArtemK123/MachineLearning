using System.Collections.Generic;

namespace EightPuzzle.AStar
{
    internal class AStarSolver : ISolver
    {
        private readonly AStarStateFactory stateFactory;

        public AStarSolver()
        {
            stateFactory = new AStarStateFactory(new WrongPlacesHeuristicScoreComputer());
        }

        public State Solve(Board startBoard)
        {
            AStarState startingState = stateFactory.CreateStartingState(startBoard);

            var queue = new PriorityQueue<AStarState>();

            queue.Add(startingState);

            return Solve(queue);
        }

        private AStarState Solve(PriorityQueue<AStarState> queue)
        {
            while (true)
            {
                AStarState state = queue.TakeLast();

                if (state.Board.Equals(Board.FinalBoard))
                {
                    return state;
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