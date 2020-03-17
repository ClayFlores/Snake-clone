using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_clone
{
    public class GameBoard
    {
        List<int> PlayerPos;
        public List<char> Board = new List<char>();


        public void genBoard()
        {
            for (int i = 0; i < 400; i++) // 400 is total spaces in board
            {
                if (i < 40 || i >= 360)
                {
                    Board.Add('-');
                }
                else if (i % 40 == 0)
                {
                    Board.Add('|');
                }
                else if (i % 40 == 39)
                {
                    Board.Add('|');
                }
                else
                {
                    Board.Add(' ');
                }
            }
                Board[125] = '='; // player start
                Board[150] = 'o'; // first apple
            
        }

        // prints out the board
        public void printBoard()
        {
            for (int i = 0; i < 400; i++) // 400 is total spaces in board
            {
                    Console.Write(Board[i]);
                    if (i > 0 && i % 40 == 39)
                        Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
