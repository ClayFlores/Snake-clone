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
        public List<List<char>> Board = new List<List<char>>(); // 2d list
        public bool isAlive = true;

        public void genBoard()
        {
            int boardSize = 10; // to keep changing board size easy
            
            for(int i=0; i<boardSize; i++)
            {
                List<char> subList = new List<char>();
                for(int j=0; j<boardSize*2; j++)
                {
                    if (i == 0 || i == 9) // top or bottom row
                    {
                        subList.Add('X');
                    }
                    else if (j == 0 || j == boardSize*2-1) // left or right col
                    {
                        subList.Add('X');
                    }
                    else
                        subList.Add(' ');
                }
                Board.Add(subList);
            }
        }

        internal void move()
        {
            throw new NotImplementedException();
        }

        internal void checkAlive()
        {
            throw new NotImplementedException();
        }

        // prints out the board
        public void printBoard()
        {
            foreach(var subList in Board)
            {
                foreach(var val in subList)
                {
                    Console.Write(val);
                }
                Console.WriteLine();
            }
        }
    }
}
