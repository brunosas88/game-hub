using Game_Hub.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hub.Model.TicTacToe
{
    class TicTacToeBoard
    {
        private char[,] _board;

        public TicTacToeBoard()
        {
            _board = new char[,]
            {
                {'¹', '│', '²', '│', '³'},
                {'⁴', '│', '⁵', '│', '⁶'},
                {'⁷', '│', '⁸', '│', '⁹'}
            };
        }

        public char[,] GetBoard()
        {
            return _board;
        }

        public void PrintBoard()
        {
            Console.WriteLine(Display.AlignMessage("─────────────"));
            for (int row = 0; row < _board.GetLength(0); row++)
            {
                Console.Write(Display.AlignMessage($"│ {_board[row, 0]} {_board[row, 1]} {_board[row, 2]} {_board[row, 3]} {_board[row, 4]} │"));
                Console.WriteLine();
                Console.WriteLine(Display.AlignMessage("─────────────"));
            }
        }

        public void UpdateBoard(int position, int player)
        {

            char character = player == 1 ? 'X' : 'O';

            switch (position)
            {
                case 1:
                    _board[0, 0] = character;
                    break;
                case 2:
                    _board[0, 2] = character;
                    break;
                case 3:
                    _board[0, 4] = character;
                    break;
                case 4:
                    _board[1, 0] = character;
                    break;
                case 5:
                    _board[1, 2] = character;
                    break;
                case 6:
                    _board[1, 4] = character;
                    break;
                case 7:
                    _board[2, 0] = character;
                    break;
                case 8:
                    _board[2, 2] = character;
                    break;
                case 9:
                    _board[2, 4] = character;
                    break;
            }
        }
    }
}
