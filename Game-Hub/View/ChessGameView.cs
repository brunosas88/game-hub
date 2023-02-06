using Game_Hub.Model.Chess;
using Game_Hub.Model.Enums;
using Game_Hub.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hub.View
{
	class ChessGameView
	{
		public static void PrintChessBoard(ChessPieceInfo[,] chessBoard, List<string>? possibleMoves = null)
		{
			char[] colReference = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
			char[] rowReference = { '8', '7', '6', '5', '4', '3', '2', '1' };
			List<ConsoleColor> colors = new List<ConsoleColor> { ConsoleColor.Gray, ConsoleColor.DarkGray };
			List<ConsoleColor> highlightColors = new List<ConsoleColor> { ConsoleColor.Red, ConsoleColor.DarkRed };
			bool isDarkerColor = true, isHighlightPositions = false;
			int padLeftToCenterBoard = 19;
			int[,] highlightPositions = new int[1, 1];
			int[] realPosition;

			if ((possibleMoves != null) && possibleMoves.Count > 0)
			{
				highlightPositions = new int[possibleMoves.Count, 2];
				isHighlightPositions = true;

				for (int row = 0; row < highlightPositions.GetLength(0); row++)
				{
					realPosition = Util.GetRealPosition(possibleMoves[row]);

					for (int col = 0; col < highlightPositions.GetLength(1); col++)
					{
						highlightPositions[row, col] = realPosition[col];
					}
				}
			}

			Console.Write(" ".PadLeft(padLeftToCenterBoard) + " " + " ");
			foreach (char reference in colReference)
				Console.Write(" " + reference + " ");

			Console.WriteLine();

			for (int row = 0; row < chessBoard.GetLength(0); row++)
			{
				Console.Write(" ".PadLeft(padLeftToCenterBoard) + rowReference[row] + " ");

				for (int col = 0; col < chessBoard.GetLength(1); col++)
				{
					if (isHighlightPositions)
					{
						for (int i = 0; i < highlightPositions.GetLength(0); i++)
						{
							if (row == highlightPositions[i, 0] && col == highlightPositions[i, 1])
							{
								Console.BackgroundColor = isDarkerColor ? highlightColors[0] : highlightColors[1];
								break;
							}
							else
								Console.BackgroundColor = isDarkerColor ? colors[0] : colors[1];
						}
					}
					else
						Console.BackgroundColor = isDarkerColor ? colors[0] : colors[1];

					Console.Write(" ");
					Console.ForegroundColor = chessBoard[row, col].Color == ChessPieceColor.WHITE ? ConsoleColor.White : ConsoleColor.Black;
					Console.Write(chessBoard[row, col].Sprite);
					Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;
					Console.Write(" ");

					isDarkerColor = !isDarkerColor;
				}
				Console.BackgroundColor = Constants.MAIN_BACKGROUND_COLOR;
				colors.Reverse();
				highlightColors.Reverse();
				Console.WriteLine();
			}

			Console.BackgroundColor = Constants.MAIN_BACKGROUND_COLOR;
			Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;
		}

		public static void ShowChessInstructions(bool playerOneTurn, string playerName)
		{
			Console.Clear();
			GameHubView.ShowWarning("Insira posições utilizando notação coluna e linha (Ex.: a2)", false);
			GameHubView.ShowWarning("Insira E para pedir declaração de empate", false);
			GameHubView.ShowWarning("Insira 0 para escolher outra peça", false);
			GameHubView.ShowWarning("Insira R para desistir da partida", false);
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(GameHubView.AlignMessage($"TURNO: Jogador {playerName} " + "| PEÇAS: " + (playerOneTurn ? "BRANCAS" : "PRETAS")));
			Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;
		}

		public static void PrintChessMatchInfo(List<string> blackCapturedPieces, List<string> whiteCapturedPieces)
		{
			string output = "[";

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(GameHubView.AlignMessage("Peças Pretas Capturadas:"));
			foreach (string piece in blackCapturedPieces)
				output += ($" {piece} ");
			output += ("]");
			Console.WriteLine(GameHubView.AlignMessage(output));

			Console.WriteLine(GameHubView.AlignMessage("Peças Brancas Capturadas:"));
			output = "[";
			foreach (string piece in whiteCapturedPieces)
				output += ($" {piece} ");
			output += ("]");
			Console.WriteLine(GameHubView.AlignMessage(output));
			Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;
			Console.WriteLine();
		}

	}
}
