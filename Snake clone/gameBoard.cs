using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake_clone
{
    public class GameBoard
    {
        public List<snake> Snake = new List<snake>(); // holds all the snake pieces
        public List<List<char>> Board = new List<List<char>>(); // 2d list
        public bool isAlive = true;
        int applesEaten = 0;

        enum Direction
        {
            up, down, left, right
        }

        // this generates the board as a 2d List of chars
        // the borders are displayed as 'X' chars, the snake as '='
        // and the apples as 'o'
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
            Board[5][5] = '='; // player
            addSnake(5, 5);
            Board[5][15] = 'o'; // apple
        }

        // adds a sections coords to the Snake List
        public void addSnake(int a, int b)
        {
            snake temp = new snake();
            temp.xCoord = a;
            temp.yCoord = b;
            Snake.Add(temp);
        }
        internal void move()
        {
            // if next space == ' ' || next space == 'o' then everything is alright

            // else isAlive = false

            isAlive = false;
        }
        

        // prints out the board
        public void printBoard()
        {
            Console.Clear();
            foreach(var subList in Board)
            {
                foreach(var val in subList)
                {
                    Console.Write(val);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Apples eaten: {0}", applesEaten);
        }

        internal void gameOver()
        {
            Board[5][7] = ' ';
            Board[5][8] = 'O';
            Board[5][9] = 'V';
            Board[5][10] = 'E';
            Board[5][11] = 'R';
            Board[5][12] = ' ';
            Board[4][7] = ' ';
            Board[4][8] = 'G';
            Board[4][9] = 'A';
            Board[4][10] = 'M';
            Board[4][11] = 'E';
            Board[4][12] = ' ';
            printBoard();
        }
    }
}
