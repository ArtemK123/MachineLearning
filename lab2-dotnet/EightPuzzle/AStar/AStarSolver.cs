using System;
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

        public EightPuzzleResult Solve(Board startBoard)
        {
            AStarState startingState = stateFactory.CreateStartingState(startBoard);

            var queue = new PriorityQueue<AStarState>();

            queue.Add(startingState);

            return Solve(queue);
        }

        private EightPuzzleResult Solve(PriorityQueue<AStarState> queue)
        {
            HashSet<int> visitedBoardHashes = new HashSet<int>();
            int visitedNodesCount = 0;

            while (true)
            {
                AStarState state = queue.TakeLast();
                visitedNodesCount++;
                Console.WriteLine($"Step: {visitedNodesCount}");
                Console.WriteLine(state.Board);

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
                        AStarState newState = stateFactory.Create(board, state);
                        queue.Add(newState);
                        visitedBoardHashes.Add(boardHash);
                    }
                }
            }
        }
    }
}