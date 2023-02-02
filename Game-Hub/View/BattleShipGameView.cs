using Game_Hub.Model.Enums;
using Game_Hub.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hub.View
{
	class BattleShipGameView
	{
		public static void ShowBattleShipInstructions(string turnPlayerName, string adversaryPlayerName)
		{
			Console.Clear();
			GameHubView.ShowWarning("Insira posições utilizando notação coluna e linha (Ex.: a2)", false);
			GameHubView.ShowWarning("Insira E para pedir declaração de empate", false);
			GameHubView.ShowWarning("Insira R para desistir da partida", false);

			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(GameHubView.AlignMessage($"TURNO: Jogador {turnPlayerName} | CAMPO: Jogador {adversaryPlayerName}"));
			Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;
		}

		public static void PrintBattleFieldBoard(BattleShipFieldInfo[,] board)
		{
			char[] colReference = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
			string[] rowReference = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
			int padLeftToCenterBoard = 16;

			Console.Write(" ".PadLeft(padLeftToCenterBoard) + " " + " ");
			foreach (char reference in colReference)
				Console.Write(" " + reference + " ");

			Console.WriteLine();

			for (int row = 0; row < board.GetLength(0); row++)
			{
				if (row == 9)
					Console.Write(" " + rowReference[row].PadLeft(padLeftToCenterBoard) + " ");
				else
					Console.Write(" ".PadLeft(padLeftToCenterBoard) + rowReference[row] + " ");

				for (int col = 0; col < board.GetLength(1); col++)
				{
					Console.BackgroundColor = ConsoleColor.Blue;

					if (!board[row, col].IsShot)
						Console.Write(" ~ ");
					else
					{
						if (board[row, col].IsShip)
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.Write(" ● ");
						}
						else
							Console.Write(" ○ ");
					}
					Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;

				}
				Console.BackgroundColor = Constants.MAIN_BACKGROUND_COLOR;
				Console.WriteLine();
			}
			Console.BackgroundColor = Constants.MAIN_BACKGROUND_COLOR;
			Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;
		}

		public static void ShowBatlheShipFieldCurrentPlay(BattleShipFieldInfo[,] board, string turnPlayerName, string adversaryPlayerName)
		{
			Console.Clear();
			ShowBattleShipInstructions(turnPlayerName, adversaryPlayerName);
			PrintBattleFieldBoard(board);
			GameHubView.ShowWarning("Aperte Enter para continuar...");
			Console.ReadLine();
		}

		public static void PrintBattleShipMatchInfo(string name, List<string> sunkenShips)
		{
			string output = "[";

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(GameHubView.AlignMessage($"Navios de {name} Afundados"));

			foreach (string ship in sunkenShips)
				output += ($" {ship} ");
			output += ("]");

			Console.WriteLine(GameHubView.AlignMessage(output));
			Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;
		}
	}
}
