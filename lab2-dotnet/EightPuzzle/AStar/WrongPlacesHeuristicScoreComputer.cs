namespace EightPuzzle.AStar
{
    internal class WrongPlacesHeuristicScoreComputer
    {
        public int Compute(Board board)
        {
            int wrongPlacesCount = 0;

            for (int i = 0; i < board.RowsCount; i++)
            {
                for (int j = 0; j < board.ColumnsCount; j++)
                {
                    if (board.GetValue(i, j) != Board.FinalBoard.GetValue(i, j))
                    {
                        wrongPlacesCount++;
                    }
                }
            }

            return wrongPlacesCount;
        }
    }
}