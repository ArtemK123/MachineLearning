namespace EightPuzzle
{
    internal class State
    {
        public State(Board board, State previousState)
        {
            Board = board;
            PreviousState = previousState;
        }

        public Board Board { get; }

        public State PreviousState { get; }
    }
}