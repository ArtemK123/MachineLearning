using System.Collections.Generic;

namespace EightPuzzle.Bfs
{
    internal class BfsSolver : ISolver
    {
        public EightPuzzleResult Solve(Board startBoard)
        {
            var queue = new Queue<State>();
            queue.Enqueue(new State(startBoard, null));

            return Solve(queue);
        }

        private EightPuzzleResult Solve(Queue<State> queue)
        {
            HashSet<int> visitedBoardHashes = new HashSet<int>();
            int visitedNodesCount = 0;

            while (true)
            {
                State state = queue.Dequeue();
                visitedNodesCount++;

                if (state.Board.Equals(Board.FinalBoard))
                {
                    return new EightPuzzleResult(state, visitedNodesCount);
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