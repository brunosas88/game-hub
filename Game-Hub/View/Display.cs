using Game_Hub.Model;
using Game_Hub.Model.BattleShip;
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
            Console.ForegroundColor = ConsoleColor.Black;
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
                else if (!char.IsControl(dataEntryKeyInfo.KeyChar) && dataEntry.Length < Constants.MAX_CHARACTER_ALLOWED)
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

		public static void ShowLogo()
		{
			Thread.Sleep(1000);
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(0, 4);
			Console.WriteLine((@"
	░█▀▀▀█ █  █ █▀▀█ █▀▀█ █▀▀█
	 ▀▀▀▄▄ █▀▀█ █▄▄█ █▄▄▀ █  █
	░█▄▄▄█ ▀  ▀ ▀  ▀ ▀ ▀▀ █▀▀▀"));
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine((@"
		░█▀▀█ █▀▀█ █▀▀▄ █▀▀ █▀▀█ █▀▀▀█
		░█    █  █ █  █ █▀▀ █▄▄▀ ▀▀▀▄▄
		░█▄▄█ ▀▀▀▀ ▀▀▀  ▀▀▀ ▀ ▀▀ █▄▄▄█"));
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine((@"
				░█▀▀█ █▀▀█ █▀▄▀█ █▀▀ █▀▀▀█
				░█ ▄▄ █▄▄█ █ ▀ █ █▀▀ ▀▀▀▄▄
				░█▄▄█ ▀  ▀ ▀   ▀ ▀▀▀ █▄▄▄█"));
			Console.BackgroundColor = ConsoleColor.Cyan;
			Thread.Sleep(1500);
			Console.Clear();
			Thread.Sleep(300);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.SetCursorPosition(0, 7);
			Console.WriteLine((@"
   ████████████████████████████████████████████████████████████
   █▄─▄─▀█─▄▄▄▄█─▄▄─█▄─▄███▄─██─▄█─▄─▄─█▄─▄█─▄▄─█▄─▀█▄─▄█─▄▄▄▄█
   ██─▄─▀█▄▄▄▄─█─██─██─██▀██─██─████─████─██─██─██─█▄▀─██▄▄▄▄─█
   ▀▄▄▄▄▀▀▄▄▄▄▄▀▄▄▄▄▀▄▄▄▄▄▀▀▄▄▄▄▀▀▀▄▄▄▀▀▄▄▄▀▄▄▄▄▀▄▄▄▀▀▄▄▀▄▄▄▄▄▀"));
			Console.BackgroundColor = ConsoleColor.Black;
			Thread.Sleep(1500);
			Console.Clear();
			Thread.Sleep(300);
		}

		public static void ShowTitle()
		{
			Console.BackgroundColor = ConsoleColor.Magenta;
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine((AlignMessage("╔" + new string('═', Constants.WINDOW_WIDTH_SIZE - 2) + "╗" + @"
 ░██████╗░░█████╗░███╗░░░███╗███████╗   ██╗░░██╗██╗░░░██╗██████╗░ 
║██╔════╝░██╔══██╗████╗░████║██╔════╝   ██║░░██║██║░░░██║██╔══██╗║
 ██║░░██╗░███████║██╔████╔██║█████╗░░   ███████║██║░░░██║██████╦╝ 
║██║░░╚██╗██╔══██║██║╚██╔╝██║██╔══╝░░   ██╔══██║██║░░░██║██╔══██╗║
 ╚██████╔╝██║░░██║██║░╚═╝░██║███████╗   ██║░░██║╚██████╔╝██████╦╝ 
╚░╚═════╝░╚═╝░░╚═╝╚═╝░░░░░╚═╝╚══════╝   ╚═╝░░╚═╝░╚═════╝░╚═════╝░╝")));
		}

		public static void GameInterface(string message)
        {
            Console.Clear();
            int blankSpace = Constants.WINDOW_WIDTH_SIZE - 20;
            int totalCharsHeader = Constants.WINDOW_WIDTH_SIZE;
            message = AlignMessage(message, blankSpace);
			
			ShowTitle();		

            Console.BackgroundColor = ConsoleColor.Green;
			string topEdge = "╔" + new string('═', totalCharsHeader - 2) + "╗";
			string bottomEdge = "╚" + new string('═', totalCharsHeader - 2) + "╝";

			Console.WriteLine(topEdge);
            Console.Write("║▀▄▀▄▀▄▀▄▀");
            Console.Write(message);
            Console.Write("▀▄▀▄▀▄▀▄▀║\n");
            Console.WriteLine(bottomEdge);
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
				Console.WriteLine();
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

			Console.ForegroundColor = ConsoleColor.Yellow;

			Console.WriteLine(AlignMessage($"{player.Name}"));
			
			Console.WriteLine(AlignMessage($"{matchInfo.Points} Ponto(s) ▪ " +
                                           $"{matchInfo.Victories} Vitória(s) ▪ " +
                                           $"{matchInfo.Defeats} Derrota(s) ▪ " +
                                           $"{matchInfo.Draws} Empate(s)"));

			Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;
		}

        public static void ShowMatchesDetails(Match match)
        {
            Console.WriteLine();
            string FirstHalfTitle = $"{match.PlayerOne} x";

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"{FirstHalfTitle,Constants.WINDOW_WIDTH_SIZE / 2}" + $" {match.PlayerTwo,-(Constants.WINDOW_WIDTH_SIZE / 2) - 1}");           
            
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
			ShowWarning("Insira posições utilizando notação coluna e linha (Ex.: a2)", false);
			ShowWarning("Insira E para pedir declaração de empate", false);
			ShowWarning("Insira 0 para escolher outra peça", false);
			ShowWarning("Insira R para desistir da partida", false);
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(AlignMessage($"TURNO: Jogador {playerName} " + "| PEÇAS: " + (playerOneTurn ? "BRANCAS" : "PRETAS")));
			Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;
		}

		public static void PrintChessMatchInfo(List<string> blackCapturedPieces, List<string> whiteCapturedPieces)
		{
            string output;

			Console.ForegroundColor = ConsoleColor.Yellow;
			output = ("Peças Pretas Capturadas: [");
			foreach (string piece in blackCapturedPieces)
				output += ($" {piece} ");
			output += ("]");			
			Console.WriteLine(AlignMessage(output));

			output = ($"Peças Brancas Capturadas: [");
			foreach (string piece in whiteCapturedPieces)
				output += ($" {piece} ");
			output += ("]");
			Console.WriteLine(AlignMessage(output));
			Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;
			Console.WriteLine();
		}

		public static void ShowBattleShipInstructions(string turnPlayerName, string adversaryPlayerName)
		{
			Console.Clear();
			ShowWarning("Insira posições utilizando notação coluna e linha (Ex.: a2)", false);
			ShowWarning("Insira E para pedir declaração de empate", false);
			ShowWarning("Insira R para desistir da partida", false);

			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(AlignMessage($"TURNO: Jogador {turnPlayerName} | CAMPO: Jogador {adversaryPlayerName}"));		
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
					Console.Write( " " + rowReference[row].PadLeft(padLeftToCenterBoard) + " ");
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
			ShowWarning("Aperte Enter para continuar...");
			Console.ReadLine();
		}

		public static void PrintBattleShipMatchInfo(string name, List<string> sunkenShips)
		{
			string output = "[";

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(AlignMessage($"Navios de {name} Afundados"));

			foreach (string ship in sunkenShips)
				output += ($" {ship} ");
			output += ("]");
            
			Console.WriteLine(AlignMessage(output));
			Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;
		}

		public static void ShowTicTacToeInstructions(string name)
		{
			Console.Clear();
			ShowWarning("Insira posições de 1-9", false);
			ShowWarning("Insira E para pedir declaração de empate", false);
			ShowWarning("Insira R para desistir da partida", false);

			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(AlignMessage($"TURNO: Jogador {name}"));
			Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;
		}
	}
}
