﻿using Game_Hub.Model;
using Game_Hub.Model.Enums;
using Game_Hub.Util;
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
            int screenCenterValue = (Constants.WindowWidthSize / 2) - 1;
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
            return string.Format($"{{0,-{blankSpace}}}", string.Format("{0," + ((blankSpace + message.Length) / 2).ToString() + "}", message));
        }

        public static void GameInterface(string message)
        {
            Console.Clear();
            int blankSpace = Constants.WindowWidthSize - 20;
            int totalCharsHeader = Constants.WindowWidthSize;
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
            Console.WriteLine(AlignMessage($"{player.Nome}"));
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
            Console.WriteLine($"{FirstHalfTitle,Constants.WindowWidthSize / 2}" + $" {match.PlayerTwo,-(Constants.WindowWidthSize / 2) - 1}");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(AlignMessage($"{match.PlayerOneVictories} x {match.PlayerTwoVictories}"));
            if (match.Draws > 0) Console.WriteLine(AlignMessage($"Empate(s) : {match.Draws}"));
            if (match.MatchesPlayed > 1) Console.WriteLine(AlignMessage($"Partidas Consecutivas: {match.MatchesPlayed}"));
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
