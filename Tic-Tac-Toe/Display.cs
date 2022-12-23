﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
	class Display
	{
		public static string FormatConsoleReadLine()
		{
			Console.ForegroundColor= ConsoleColor.Yellow;
			string dataEntry = string.Empty;
			ConsoleKey dataEntryKey;
			do
			{
				ConsoleKeyInfo dataEntryKeyInfo = Console.ReadKey(intercept: true);
				dataEntryKey = dataEntryKeyInfo.Key;

				if (dataEntryKey == ConsoleKey.Backspace && dataEntry.Length > 0)
				{					
					dataEntry = dataEntry[0..^1];
					Console.CursorLeft = 0;
					Console.Write(AlignMessage(dataEntry));
				}
				else if (!char.IsControl(dataEntryKeyInfo.KeyChar))
				{
					dataEntry += dataEntryKeyInfo.KeyChar;
					Console.CursorLeft = 0;					
					Console.Write(AlignMessage(dataEntry));					
				}
			} while (dataEntryKey != ConsoleKey.Enter);
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.White;
			return dataEntry;
		}

		public static void ShowWarning(string message)
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.Yellow;
			Console.Write(AlignMessage(message) + "\n");
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine();
		}

		public static void BackToMenu()
		{
			Console.WriteLine();
			Console.WriteLine(AlignMessage("Pressione Enter para voltar ao menu anterior."));
			Console.ReadLine();
		}

		public static string AlignMessage(string message, int blankSpace = Constants.WindowWidthSize)
		{
			return String.Format($"{{0,-{blankSpace}}}", String.Format("{0," + ((blankSpace + message.Length) / 2).ToString() + "}", message));
		}

		public static void GameInterface(string message)
		{
			Console.Clear();
			int blankSpace = Constants.WindowWidthSize-20;
			int totalCharsHeader = Constants.WindowWidthSize;
			message = AlignMessage(message, blankSpace);
			string title = AlignMessage("Tic-Tac-Toe", blankSpace);
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

		public static void ShowMenu()
		{
			Console.WriteLine(AlignMessage("1 - Cadastrar Novo Jogador"));
			Console.WriteLine(AlignMessage("2 - Histórico do Jogo"));
			Console.WriteLine(AlignMessage("3 - Jogar!"));
			Console.WriteLine(AlignMessage("0 - Sair do Jogo"));
			Console.WriteLine();
		}

		public static void ShowLogMenu()
		{
			Console.WriteLine(AlignMessage("1 - Ver Histórico de Jogadores"));
			Console.WriteLine(AlignMessage("2 - Ver Histórico de Partidas"));
			Console.WriteLine(AlignMessage("0 - Voltar ao Menu Principal"));
			Console.WriteLine();
		}

		public static void ShowPlayerDetails(Player player)
		{
			Console.WriteLine();
			Console.BackgroundColor = ConsoleColor.Yellow;
			Console.ForegroundColor = ConsoleColor.Black;			
			Console.WriteLine(AlignMessage($"{player.Nome}"));
			Console.BackgroundColor = ConsoleColor.DarkCyan;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(AlignMessage($"{player.Points} pontos | {player.Victories} vitórias | {player.Defeats} derrotas | {player.Draws} empates"));			
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void ShowMatchesDetails(Match match)
		{
			Console.WriteLine();
			string FirstHalfTitle = $"{match.PlayerOne} x";
			Console.BackgroundColor = ConsoleColor.Yellow;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine($"{FirstHalfTitle, Constants.WindowWidthSize/2}" + $" {match.PlayerTwo,- (Constants.WindowWidthSize / 2) - 1}");
			Console.BackgroundColor = ConsoleColor.DarkCyan;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(AlignMessage($"{match.PlayerOneVictories} x {match.PlayerTwoVictories}"));			
			if (match.Draws > 0)	Console.WriteLine(AlignMessage($"Empates : {match.Draws}"));
			if (match.MatchesPlayed > 1)	Console.WriteLine(AlignMessage($"Partidas Consecutivas: {match.MatchesPlayed}"));
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}
