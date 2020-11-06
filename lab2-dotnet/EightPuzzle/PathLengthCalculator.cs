namespace EightPuzzle
{
    internal class PathLengthCalculator
    {
        public int CalculatePathLenght(State state)
        {
            int pathLenght = 0;
            var currentState = state;
            while (currentState != null)
            {
                pathLenght++;
                currentState = currentState.PreviousState;
            }

            return pathLenght;
        }
    }
}