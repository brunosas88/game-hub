using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hub.Utils
{
    public static class Constants
    {
        public const int WindowHeightSize = 28;
        public const int WindowWidthSize = 66;
        public const int DrawPoints = 1;
        public const int VictoryPoints = 2;
        public const int DefeatPoints = -1;
        public const string MainMenuFirstOption = "Cadastrar Novo Jogador";
        public const string MainMenuSecondOption = "Histórico do Jogo";
        public const string MainMenuThirdption = "Jogar!";
        public const string MainMenuEndGameOption = "Sair do Jogo";
        public const string LogMenuFirstOption = "Ranking";
        public const string LogMenuSecondOption = "Histórico de Partidas";
        public const string MenuBackToOption = "Voltar ao Menu Anterior";
        public const bool IS_ENCRYPTED = true;
		public static readonly Dictionary<string, int> LINE_REFERENCE = new Dictionary<string, int>
		{
			{"8", 0}, {"7", 1}, {"6", 2}, {"5", 3}, {"4", 4}, {"3", 5}, {"2", 6}, {"1", 7}
		};
		public static readonly Dictionary<string, int> COLUMN_REFERENCE = new Dictionary<string, int>
		{
			{"a", 0}, {"b", 1}, {"c", 2}, {"d", 3}, {"e", 4}, {"f", 5}, {"g", 6}, {"h", 7}
		};
		public const string BISHOP_SPRITE = "B";
		public const string KING_SPRITE = "K";
		public const string KNIGHT_SPRITE = "T";
		public const string PAWN_SPRITE = "P";
		public const string QUEEN_SPRITE = "Q";
		public const string ROOK_SPRITE = "R";
		public const ConsoleColor MAIN_FOREGROUND_COLOR = ConsoleColor.White;
		public const ConsoleColor MAIN_BACKGROUND_COLOR = ConsoleColor.DarkCyan;

	}

}
