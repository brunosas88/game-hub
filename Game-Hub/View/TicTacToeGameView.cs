using Game_Hub.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hub.View
{
	class TicTacToeGameView
	{
		public static void ShowTicTacToeInstructions(string name)
		{
			Console.Clear();
			GameHubView.ShowWarning("Insira posições de 1-9", false);
			GameHubView.ShowWarning("Insira E para pedir declaração de empate", false);
			GameHubView.ShowWarning("Insira R para desistir da partida", false);

			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(GameHubView.AlignMessage($"TURNO: Jogador {name}"));
			Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;
		}
	}
}
