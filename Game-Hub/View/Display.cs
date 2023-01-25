using Game_Hub.Model;
using Game_Hub.Model.Chess;
using Game_Hub.Model.Enums;
using Game_Hub.Utils;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hub.View
{
    class Display
    {
        public static string FormatConsoleReadLine(bool encrypt = false)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string dataEntry = string.Empty;
            ConsoleKey dataEntryKey;
            int screenCenterValue = (Constants.WINDOW_WIDTH_SIZE / 2) - 1;
			Console.CursorLeft = screenCenterValue;

			do
            {
                ConsoleKeyInfo dataEntryKeyInfo = Console.ReadKey(intercept: true);
                dataEntryKey = dataEntryKeyInfo.Key;

                if (dataEntryKey == ConsoleKey.Backspace && dataEntry.Length > 0)
                {
                    dataEntry = dataEntry[0..^1];
                    Console.CursorLeft = 0;
                    Console.Write(encrypt ? AlignMessage(new string('*', dataEntry.Length)) : AlignMessage(dataEntry));
				}
                else if (!char.IsControl(dataEntryKeyInfo.KeyChar))
                {
                    dataEntry += dataEntryKeyInfo.KeyChar;
                    Console.CursorLeft = 0;
					Console.Write(encrypt ? AlignMessage(new string('*', dataEntry.Length)) : AlignMessage(dataEntry));
				}
            } while (dataEntryKey != ConsoleKey.Enter);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            return dataEntry;
        }

        public static void ShowWarning(string message, bool skipLine = true)
        {
            if (skipLine) Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine(AlignMessage(message));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
			if (skipLine) Console.WriteLine();
		}

        public static void BackToMenu()
        {
            Console.WriteLine();
            Console.WriteLine(AlignMessage("Pressione Enter para voltar ao menu anterior."));
            Console.ReadLine();
        }

        public static string AlignMessage(string message, int blankSpace = Constants.WINDOW_WIDTH_SIZE)
        {
            return string.Format($"{{0,-{blankSpace}}}", string.Format("{0," + ((blankSpace + message.Length) / 2).ToString() + "}", message));
        }

        public static void GameInterface(string message)
        {
            Console.Clear();
            int blankSpace = Constants.WINDOW_WIDTH_SIZE - 20;
            int totalCharsHeader = Constants.WINDOW_WIDTH_SIZE;
            message = AlignMessage(message, blankSpace);
            string title = AlignMessage("Hub de Jogos", blankSpace);
            Console.BackgroundColor = ConsoleColor.Green;

            Console.WriteLine(new string('=', totalCharsHeader));
            Console.Write("=========|");
            Console.Write(title);
            Console.Write("|=========\n");
            Console.WriteLine(new string('=', totalCharsHeader));

            Console.BackgroundColor = ConsoleColor.Magenta;

            Console.WriteLine(new string('-', totalCharsHeader));
            Console.Write("---------|");
            Console.Write(message);
            Console.Write("|---------\n");
            Console.WriteLine(new string('-', totalCharsHeader));
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine();
        }

        public static void ColorizeMessageBackground(string message)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(AlignMessage(message));
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static int ShowMenu(List<string> menuOptions, string menuTitle)
        {
            int indexToColorize = 1;
            ConsoleKey dataEntry;
            do
            {
                GameInterface(menuTitle);

                for (int option = 1; option < menuOptions.Count; option++)
                {
                    if (option == indexToColorize)
                        ColorizeMessageBackground(menuOptions[option]);
                    else
                        Console.WriteLine(AlignMessage(menuOptions[option]));
                }
                if (indexToColorize == 0)
                    ColorizeMessageBackground(menuOptions[indexToColorize]);
                else
                    Console.WriteLine(AlignMessage(menuOptions[0]));

                ShowWarning(AlignMessage("Utilize ↑ ↓ para navegar e Enter para selecionar a opção"));

                Console.Write(AlignMessage(""));
                dataEntry = Console.ReadKey().Key;

                if (dataEntry == ConsoleKey.DownArrow)
                    indexToColorize = indexToColorize == menuOptions.Count - 1 ? 0 : indexToColorize += 1;
                else if (dataEntry == ConsoleKey.UpArrow)
                    indexToColorize = indexToColorize == 0 ? menuOptions.Count - 1 : indexToColorize -= 1;

            } while (dataEntry != ConsoleKey.Enter);

            return indexToColorize;
        }

        public static void ShowPlayerDetails(Player player, GameTitle game)
        {
            MatchEvaluation matchInfo = player.MatchesInfo.FirstOrDefault(match => match.Game == game);
			Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(AlignMessage($"{player.Name}"));
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(AlignMessage($"{matchInfo.Points} Ponto(s) | " +
                                           $"{matchInfo.Victories} Vitória(s) | " +
                                           $"{matchInfo.Defeats} Derrota(s) | " +
                                           $"{matchInfo.Draws} Empate(s)"));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ShowMatchesDetails(Match match)
        {
            Console.WriteLine();
            string FirstHalfTitle = $"{match.PlayerOne} x";
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{FirstHalfTitle,Constants.WINDOW_WIDTH_SIZE / 2}" + $" {match.PlayerTwo,-(Constants.WINDOW_WIDTH_SIZE / 2) - 1}");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(AlignMessage($"{match.PlayerOneVictories} x {match.PlayerTwoVictories}"));
            if (match.Draws > 0) Console.WriteLine(AlignMessage($"Empate(s) : {match.Draws}"));
            if (match.MatchesPlayed > 1) Console.WriteLine(AlignMessage($"Partidas Consecutivas: {match.MatchesPlayed}"));
            Console.ForegroundColor = ConsoleColor.White;
        }

		public static void PrintChessBoard(ChessPieceInfo[,] chessBoard, List<string>? possibleMoves = null)
		{
			char[] colReference = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
			char[] rowReference = { '8', '7', '6', '5', '4', '3', '2', '1' };
			List<ConsoleColor> colors = new List<ConsoleColor> { ConsoleColor.Gray, ConsoleColor.DarkGray };
			List<ConsoleColor> highlightColors = new List<ConsoleColor> { ConsoleColor.Red, ConsoleColor.DarkRed };
			bool isDarkerColor = true, isHighlightPositions = false;
			int padLeftToCenterBoard = 19;
			int[,] highlightPositions = new int[1,1]; 
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

			Console.WriteLine();
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

		public static void PrintChessMatchInfo(List<string> blackCapturedPieces, List<string> whiteCapturedPieces, bool playerOneTurn, string playerName)
		{
            string output;
           
			output = ("Peças Pretas Capturadas: [");
			foreach (var item in blackCapturedPieces)
				output += ($" {item} ");
			output += ("]");
			Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();
			Console.WriteLine(AlignMessage(output));

			output = ($"Peças Brancas Capturadas: [");
			foreach (var item in whiteCapturedPieces)
				output += ($" {item} ");
			output += ("]");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(AlignMessage(output));

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(AlignMessage("TURNO: Peças " + (playerOneTurn ? "Brancas" : "Pretas") + $" | Jogador {playerName}"));
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}
