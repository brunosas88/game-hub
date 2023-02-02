using Game_Hub.Model.TicTacToe;
using Game_Hub.Model;
using Game_Hub.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Hub.Model.Enums;
using System.Text.RegularExpressions;

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

			string playerEntry;

			int winner;
			bool playerOneTurn = true, showWarning = false;
			bool[] moveCount = new bool[9];

			Console.WriteLine(GameHubView.AlignMessage($"Jogador {currentPlayer.Name}, insira posição: "));

			TicTacToeGameView.ShowTicTacToeInstructions(currentPlayer.Name);

			gameBoard.PrintBoard();

			do
			{

				Console.WriteLine();
				if (showWarning)
					GameHubView.ShowWarning("Posição Inválida", false);

				Console.WriteLine(GameHubView.AlignMessage("Insira uma posição:"));
				playerEntry = GameHubView.FormatConsoleReadLine();

				if (ValidateMove(moveCount, playerEntry))
				{
					gameBoard.UpdateBoard(int.Parse(playerEntry), currentPlayer.PlayOrder);
					playerOneTurn = !playerOneTurn;
					currentPlayer = playerOneTurn ? playerOne : playerTwo;
					showWarning = false;
				}
				else if (playerEntry.ToLower() == "s")
				{
					playerEntry = "-1";
					showWarning = true;
				}										
				else if (playerEntry.ToLower() == "e")
				{
					GameHubView.ShowWarning("Outro Jogador Consente na Declaração de Empate?", false);
					GameHubView.ShowWarning("S - SIM / Qualquer outra tecla - NÃO", false);
					playerEntry = GameHubView.FormatConsoleReadLine();
					showWarning = false;
				}
				else
					showWarning = true;

				Console.WriteLine(GameHubView.AlignMessage($"Jogador {currentPlayer.Name}, insira posição: "));

				TicTacToeGameView.ShowTicTacToeInstructions(currentPlayer.Name);

				gameBoard.PrintBoard();

				winner = CheckTicTacToeWinner(gameBoard.GetBoard());

				if (!moveCount.Contains(false))
					playerEntry = "s";

			} while (playerEntry.ToLower() != "r" && playerEntry.ToLower() != "s" && winner == 0);

			CalculateResults(playerOne, playerTwo, matchInfoP1, matchInfoP2, playerEntry, winner, playerOneTurn);
		}

		private static void CalculateResults(Player playerOne, Player playerTwo, MatchEvaluation matchInfoP1, MatchEvaluation matchInfoP2, string playerEntry, int winner, bool playerOneTurn)
		{
			if (winner == 1)
			{
				matchInfoP1.Victories++;
				matchInfoP2.Defeats++;
				GameHubView.ShowWarning($"Jogador {playerOne.Name} venceu!");
			}
			else if (winner == 2)
			{
				matchInfoP1.Defeats++;
				matchInfoP2.Victories++;
				GameHubView.ShowWarning($"Jogador {playerTwo.Name} venceu!");
			}
			else if (playerEntry == "s")
			{
				matchInfoP1.Draws++;
				matchInfoP2.Draws++;
				GameHubView.ShowWarning("Jogadores finalizaram a partida sem vencedores");
			}
			else
			{
				if (playerOneTurn)
				{
					matchInfoP1.Defeats++;
					matchInfoP2.Victories++;
					GameHubView.ShowWarning($"Jogador {playerTwo.Name} venceu!");
				}
				else
				{
					matchInfoP1.Victories++;
					matchInfoP2.Defeats++;
					GameHubView.ShowWarning($"Jogador {playerOne.Name} venceu!");
				}
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

		private static bool ValidateMove(bool[] moveCount, string position)
		{						
			Regex numberRegex = new Regex(@"^([1-9]){1}$");

			if (numberRegex.IsMatch(position) && !moveCount[int.Parse(position) - 1])
			{
				moveCount[int.Parse(position) - 1] = true;
				return true;
			}
			else
				return false;
		}
	}
}
