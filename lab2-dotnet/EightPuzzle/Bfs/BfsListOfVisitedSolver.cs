using System.Collections.Generic;

namespace EightPuzzle.Bfs
{
    internal class BfsListOfVisitedSolver : ISolver
    {
        public EightPuzzleResult Solve(Board startBoard)
        {
            var queue = new Queue<State>();
            queue.Enqueue(new State(startBoard, null));

            return Solve(queue);
        }

        private EightPuzzleResult Solve(Queue<State> queue)
        {
            List<Board> visitedBoards = new List<Board>();
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
                    if (!IsBoardStored(visitedBoards, board))
                    {
                        State newState = new State(board, state);
                        queue.Enqueue(newState);
                        visitedBoards.Add(board);
                    }
                }
            }
        }

        private bool IsBoardStored(List<Board> storedBoards, Board board)
        {
            foreach (Board storedBoard in storedBoards)
            {
                if (board.Equals(storedBoard))
                {
                    return true;
                }
            }

            return false;
        }
    }
}