using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading; // to wait
using System.Windows.Input; // to check key press
using System.Threading.Tasks; 


namespace Snake_clone
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime lastMeasuredTime = DateTime.Now;
            double frameTime = 1000.0 / 2; // 1000ms / 2 = 2 fps (using frames for speed of snake / difficulty)

            GameBoard board = new GameBoard();
            board.genBoard();
            board.printBoard();
            char response;

            // the "do they want to play" block 
            Console.WriteLine("\nWould you like to play? (Y to play)");
            response = (char)Console.Read();
                if (response != 'Y' && response != 'y') // why dont they want to play? :(
                {
                    Console.WriteLine("\nMaybe next time...");
                    return;
                }

                // gameplay loop
                while (board.isAlive)
                {
                    // this loop will just set the new direction, no action taken until the next frame
                    // also prevents going backwards relative to current direction and running immediately into your body
                    // TODO MAYBE: add a flag to only allow changing direction once per frame and resetting flag on frame change
                    // seems unlikely to be needed but possible to go one direction then go immediately into your body if changing directions rapidly
                    while (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        if (key.KeyChar == 'w')
                        {
                            if (board.currDirection != GameBoard.Direction.up) // if not already facing this direction
                            {
                                if (board.currDirection != GameBoard.Direction.down)
                                    board.currDirection = GameBoard.Direction.up;
                            }
                        }
                        else if (key.KeyChar == 'a')
                        {
                            if (board.currDirection != GameBoard.Direction.left) // if not already facing this direction
                            {
                                if (board.currDirection != GameBoard.Direction.right)
                                    board.currDirection = GameBoard.Direction.left;
                            }
                        }
                        else if (key.KeyChar == 'd')
                        {
                            if (board.currDirection != GameBoard.Direction.right) // if not already facing this direction
                            {
                                if (board.currDirection != GameBoard.Direction.left)
                                    board.currDirection = GameBoard.Direction.right;
                            }
                        }
                        else if (key.KeyChar == 's')
                        {
                            if (board.currDirection != GameBoard.Direction.down) // if not already facing this direction
                            {
                                if (board.currDirection != GameBoard.Direction.up)
                                    board.currDirection = GameBoard.Direction.down;
                            }
                        }
                    }
                    // this is the frame
                    if ((DateTime.Now - lastMeasuredTime).TotalMilliseconds >= frameTime)
                    {
                        board.move();
                        board.printBoard();
                        lastMeasuredTime = DateTime.Now;
                    }
                }
                board.gameOver();
            
        }
    } 
}
