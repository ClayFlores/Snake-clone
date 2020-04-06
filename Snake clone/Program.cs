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
          

            GameBoard board = new GameBoard();

            board.playSnake();

            
        }
    } 
}
