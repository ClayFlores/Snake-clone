using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_clone
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard board = new GameBoard();
            board.genBoard();
            board.printBoard();
            char response;

            var direction = "r";

            // the "do they want to play" block 
            Console.WriteLine("\nWould you like to play? (Y to play)");
            response = (char) Console.Read();
            if (response != 'Y' && response != 'y') // why dont they want to play? :(
            {
                Console.WriteLine("\nMaybe next time...");
                return;
            }

            // gameplay starts here
            while (board.isAlive)
            {
                board.move();


                board.checkAlive();
            }
        }
    } 
}
