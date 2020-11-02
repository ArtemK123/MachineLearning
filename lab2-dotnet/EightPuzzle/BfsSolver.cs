using System.Collections.Generic;

namespace EightPuzzle
{
    internal class BfsSolver
    {
        public State Solve(Board startBoard)
        {
            var queue = new Queue<State>();
            queue.Enqueue(new State(startBoard, null));

            return Solve(queue);
        }

        private State Solve(Queue<State> queue)
        {
            HashSet<int> visitedBoardHashes = new HashSet<int>();

            while (true)
            {
                State state = queue.Dequeue();

                if (state.Board.Equals(Board.FinalBoard))
                {
                    return state;
                }

                IReadOnlyCollection<Board> neighborBoards = state.Board.GetNeighbors();

                foreach (Board board in neighborBoards)
                {
                    int boardHash = board.GetHashCode();

                    if (!visitedBoardHashes.Contains(boardHash))
                    {
                        State newState = new State(board, state);
                        queue.Enqueue(newState);
                        visitedBoardHashes.Add(boardHash);
                    }
                }
            }
        }
    }
}