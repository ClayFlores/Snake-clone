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
        int boardSize = 10; // to keep changing board size easy

        public enum Direction
        {
            up, down, left, right
        }

        internal void playSnake()
        {
            DateTime lastMeasuredTime = DateTime.Now;
            double frameTime = 1000.0 / 4; // 1000ms / 4 = 4 fps (using frames for speed of snake / difficulty)

            genBoard();
            printBoard();
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
            while (isAlive)
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
                        if (currDirection != GameBoard.Direction.up) // if not already facing this direction
                        {
                            if (currDirection != GameBoard.Direction.down)
                                currDirection = GameBoard.Direction.up;
                        }
                    }
                    else if (key.KeyChar == 'a')
                    {
                        if (currDirection != GameBoard.Direction.left) // if not already facing this direction
                        {
                            if (currDirection != GameBoard.Direction.right)
                                currDirection = GameBoard.Direction.left;
                        }
                    }
                    else if (key.KeyChar == 'd')
                    {
                        if (currDirection != GameBoard.Direction.right) // if not already facing this direction
                        {
                            if (currDirection != GameBoard.Direction.left)
                                currDirection = GameBoard.Direction.right;
                        }
                    }
                    else if (key.KeyChar == 's')
                    {
                        if (currDirection != GameBoard.Direction.down) // if not already facing this direction
                        {
                            if (currDirection != GameBoard.Direction.up)
                                currDirection = GameBoard.Direction.down;
                        }
                    }
                }
                // this is the frame
                if ((DateTime.Now - lastMeasuredTime).TotalMilliseconds >= frameTime)
                {
                    move();
                    printBoard();
                    lastMeasuredTime = DateTime.Now;
                }
            }
            gameOver();
        }

        public Direction currDirection = Direction.right; // default direction

        // this generates the board as a 2d List of chars
        // the borders are displayed as 'X' chars, the snake as '='
        // and the apples as 'o'
        public void genBoard()
        {
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
            // this is going to be a huge function. could break it into diff functions for each direction
            switch (currDirection)
            {
                case Direction.right:
                    // monster of an if statement
                    // checks if the value 1 space up is a wall or part of the snake
                    if (Board[Snake.Last().xCoord][Snake.Last().yCoord + 1] == 'X' || Board[Snake.Last().xCoord][Snake.Last().yCoord + 1] == '=')
                    {
                        isAlive = false;
                    }
                    // not a death but an apple
                    else if (Board[Snake.Last().xCoord][Snake.Last().yCoord + 1] == 'o')
                    {
                        snake temp = new snake();
                        temp.xCoord = Snake.Last().xCoord;
                        temp.yCoord = Snake.Last().yCoord + 1;
                        Snake.Add(temp);
                        Board[temp.xCoord][temp.yCoord] = '=';
                        applesEaten++;
                        randApple();
                    }
                    else
                    {
                        snake temp = new snake();
                        temp.xCoord = Snake.Last().xCoord;
                        temp.yCoord = Snake.Last().yCoord + 1;
                        Snake.Add(temp);
                        Board[temp.xCoord][temp.yCoord] = '=';
                        // remove oldest snake
                        Board[Snake[0].xCoord][Snake[0].yCoord] = ' ';
                        Snake.RemoveAt(0); // the first item in the list should be the oldest snake piece
                    }
                    break;
                case Direction.left:
                    if (Board[Snake.Last().xCoord][Snake.Last().yCoord - 1] == 'X' || Board[Snake.Last().xCoord][Snake.Last().yCoord - 1] == '=')
                    {
                        isAlive = false;
                    }
                    // not a death but an apple
                    else if (Board[Snake.Last().xCoord][Snake.Last().yCoord - 1] == 'o')
                    {
                        snake temp = new snake();
                        temp.xCoord = Snake.Last().xCoord;
                        temp.yCoord = Snake.Last().yCoord - 1;
                        Snake.Add(temp);
                        Board[temp.xCoord][temp.yCoord] = '=';
                        applesEaten++;
                        randApple();
                    }
                    else
                    {
                        snake temp = new snake();
                        temp.xCoord = Snake.Last().xCoord;
                        temp.yCoord = Snake.Last().yCoord - 1;
                        Snake.Add(temp);
                        Board[temp.xCoord][temp.yCoord] = '=';
                        // remove oldest snake
                        Board[Snake[0].xCoord][Snake[0].yCoord] = ' ';
                        Snake.RemoveAt(0); // the first item in the list should be the oldest snake piece
                    }
                    break;
                case Direction.up:
                    if (Board[Snake.Last().xCoord - 1][Snake.Last().yCoord] == 'X' || Board[Snake.Last().xCoord - 1][Snake.Last().yCoord] == '=')
                    {
                        isAlive = false;
                    }
                    // not a death but an apple
                    else if (Board[Snake.Last().xCoord - 1][Snake.Last().yCoord] == 'o')
                    {
                        snake temp = new snake();
                        temp.xCoord = Snake.Last().xCoord - 1;
                        temp.yCoord = Snake.Last().yCoord;
                        Snake.Add(temp);
                        Board[temp.xCoord][temp.yCoord] = '=';
                        applesEaten++;
                        randApple();
                    }
                    else
                    {
                        snake temp = new snake();
                        temp.xCoord = Snake.Last().xCoord - 1;
                        temp.yCoord = Snake.Last().yCoord;
                        Snake.Add(temp);
                        Board[temp.xCoord][temp.yCoord] = '=';
                        // remove oldest snake
                        Board[Snake[0].xCoord][Snake[0].yCoord] = ' ';
                        Snake.RemoveAt(0); // the first item in the list should be the oldest snake piece
                    }
                    break;
                case Direction.down:
                    if (Board[Snake.Last().xCoord + 1][Snake.Last().yCoord] == 'X' || Board[Snake.Last().xCoord + 1][Snake.Last().yCoord] == '=')
                    {
                        isAlive = false;
                    }
                    // not a death but an apple
                    else if (Board[Snake.Last().xCoord + 1][Snake.Last().yCoord] == 'o')
                    {
                        snake temp = new snake();
                        temp.xCoord = Snake.Last().xCoord + 1;
                        temp.yCoord = Snake.Last().yCoord;
                        Snake.Add(temp);
                        Board[temp.xCoord][temp.yCoord] = '=';
                        applesEaten++;
                        randApple();
                    }
                    else
                    {
                        snake temp = new snake();
                        temp.xCoord = Snake.Last().xCoord + 1;
                        temp.yCoord = Snake.Last().yCoord;
                        Snake.Add(temp);
                        Board[temp.xCoord][temp.yCoord] = '=';
                        // remove oldest snake
                        Board[Snake[0].xCoord][Snake[0].yCoord] = ' ';
                        Snake.RemoveAt(0); // the first item in the list should be the oldest snake piece
                    }
                    break;
                default:
                    Console.WriteLine("Something went wrong here...");
                    break;
            } 
        }

        private void randApple()
        {
            var rand = new Random();
            var xVal = 0;
            var yVal = 0;

                while (Board[xVal][yVal] == '=' || Board[xVal][yVal] == 'X') // keeps going until it randoms a non x or snake
                {
                    xVal = rand.Next() % boardSize;
                    yVal = rand.Next() % boardSize * 2;
                }
                Board[xVal][yVal] = 'o';

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
            Console.WriteLine("\nW A S D to move");
        }

        internal void gameOver()
        {
            Board[4][7] = ' ';
            Board[4][8] = 'G';
            Board[4][9] = 'A';
            Board[4][10] = 'M';
            Board[4][11] = 'E';
            Board[4][12] = ' ';
            Board[5][7] = ' ';
            Board[5][8] = 'O';
            Board[5][9] = 'V';
            Board[5][10] = 'E';
            Board[5][11] = 'R';
            Board[5][12] = ' ';
            printBoard();
        }
    }
}
