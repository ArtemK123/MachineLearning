using System;

namespace EightPuzzle
{
    internal class StatePrinter
    {
        public void Print(State currentState)
        {
            if (currentState == null)
            {
                return;
            }

            Print(currentState.PreviousState);
            Console.WriteLine("________________________________");
            Console.WriteLine(currentState.Board.ToString());
        }
    }
}