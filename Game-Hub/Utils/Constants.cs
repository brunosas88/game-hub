using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hub.Utils
{
    public static class Constants
    {
        public const int WINDOW_HEIGHT_SIZE = 32;
        public const int WINDOW_WIDTH_SIZE = 66;
        public const int DRAW_POINTS = 1;
        public const int VICTORY_POINTS = 2;
        public const int DEFEAT_POINTS = -1;
        public const string MAIN_MENU_FIRST_OPTION = "Cadastrar Novo Jogador";
        public const string MAIN_MENU_SECOND_OPTION = "Histórico do Jogo";
        public const string MAIN_MENU_THIRD_OPTION = "Jogar!";
        public const string MAIN_MENU_END_GAME_OPTION = "Sair do Jogo";
        public const string LOG_MENU_FIRST_OPTION = "Ranking";
        public const string LOG_MENU_SECOND_OPTION = "Histórico de Partidas";
        public const string MENU_BACK_TO_OPTION = "Voltar ao Menu Anterior";
        public const bool IS_ENCRYPTED = true;
		public const string MESSAGE_CHOOSE_POSITION_TO_MOVE = "Insira nova posição da peça:";
		public static readonly Dictionary<string, int> LINE_REFERENCE = new Dictionary<string, int>
		{
			{"8", 0}, {"7", 1}, {"6", 2}, {"5", 3}, {"4", 4}, {"3", 5}, {"2", 6}, {"1", 7}
		};
		public static readonly Dictionary<string, int> COLUMN_REFERENCE = new Dictionary<string, int>
		{
			{"a", 0}, {"b", 1}, {"c", 2}, {"d", 3}, {"e", 4}, {"f", 5}, {"g", 6}, {"h", 7}
		};
		public const string BISHOP_SPRITE = "B";
		public const string KING_SPRITE = "R";
		public const string KNIGHT_SPRITE = "C";
		public const string PAWN_SPRITE = "P";
		public const string QUEEN_SPRITE = "D";
		public const string ROOK_SPRITE = "T";
		public const string OUT_OF_GAME = "-1";
		public const string PLAYER_SAVE_FILE = @"\players.json";
		public const string MATCH_SAVE_FILE = @"\matches.json";
		public const ConsoleColor MAIN_FOREGROUND_COLOR = ConsoleColor.White;
		public const ConsoleColor MAIN_BACKGROUND_COLOR = ConsoleColor.DarkCyan;

	}

}
