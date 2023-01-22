using Game_Hub.Model.TicTacToe;
using Game_Hub.Model;
using Game_Hub.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Hub.Model.Enums;

namespace Game_Hub.Controller
{
	class TicTacToeGame
	{
		public static void PlayTicTacToeGame(Player playerOne, Player playerTwo)
		{
			TicTacToeBoard gameBoard = new TicTacToeBoard();
			MatchEvaluation matchInfoP1 = playerOne.MatchesInfo.FirstOrDefault(match => match.Game == GameTitle.JOGO_DA_VELHA),
							matchInfoP2 = playerTwo.MatchesInfo.FirstOrDefault(match => match.Game == GameTitle.JOGO_DA_VELHA);
			Player currentPlayer = playerOne;

			int position, winner;
			bool playerOneTurn = true;
			bool[] moveCount = new bool[9];
			Display.ShowWarning("Para sair da partida aperte a tecla 0 (zero)");
			gameBoard.PrintBoard();

			do
			{
				Console.WriteLine(Display.AlignMessage($"Jogador {currentPlayer.Name}, insira posição: "));
				position = CheckTicTacToeMove(moveCount);
				if (position != 0)
				{
					gameBoard.UpdateBoard(position, currentPlayer.PlayOrder);
					playerOneTurn = !playerOneTurn;
					currentPlayer = playerOneTurn ? playerOne : playerTwo;
				}
				Display.GameInterface("Jogar!");
				Display.ShowWarning("Para sair da partida aperte a tecla 0 (zero)");
				gameBoard.PrintBoard();
				winner = CheckTicTacToeWinner(gameBoard.GetBoard());
				if (!moveCount.Contains(false))
					position = 0;
			} while (position != 0 && winner == 0);

			if (winner == 1)
			{
				matchInfoP1.Victories++;
				matchInfoP2.Defeats++;
				Display.ShowWarning($"Jogador {playerOne.Name} venceu!");
			}
			else if (winner == 2)
			{
				matchInfoP1.Defeats++;
				matchInfoP2.Victories++;
				Display.ShowWarning($"Jogador {playerTwo.Name} venceu!");
			}
			else
			{
				matchInfoP1.Draws++;
				matchInfoP2.Draws++;
				Display.ShowWarning("Jogadores finalizaram a partida sem vencedores");
			}
		}

		private static int CheckTicTacToeWinner(char[,] board)
		{
			char result = ' ';

			for (int row = 0; row < board.GetLength(0); row++) // verifica linhas
			{
				if ((board[row, 0] == board[row, 2] && board[row, 0] == board[row, 4]))
					result = board[row, 0];

				for (int column = 0; column < board.GetLength(1); column += 2) // verifica colunas
				{
					if ((board[0, column] == board[1, column] && board[0, column] == board[2, column]))
						result = board[0, column];
				}
			}

			if ((board[0, 0] == board[1, 2] && board[0, 0] == board[2, 4]) || // verifica diagonais
				 (board[0, 4] == board[1, 2] && board[0, 4] == board[2, 0]))
			{ result = board[1, 2]; }

			return result == 'X' ? 1 : result == 'O' ? 2 : 0;
		}

		private static int CheckTicTacToeMove(bool[] moveCount)
		{
			int position;
			bool validEndtry;
			do
			{
				validEndtry = int.TryParse(Display.FormatConsoleReadLine(), out position);

				if (position > 0 && position < 10)
				{
					if (!moveCount[position - 1])
					{
						moveCount[position - 1] = true;
						break;
					}
					else
						Console.WriteLine(Display.AlignMessage("Posição já ocupada, escolha outra: "));
				}
				else if (validEndtry && position == 0) // sair do jogo
					break;
				else
					Console.WriteLine(Display.AlignMessage("Insira uma posição válida (1 - 9): "));

			} while (true);

			return position;
		}
	}
}
