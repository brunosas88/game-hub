using Game_Hub.Model.Enums;
using System.Text.Json.Serialization;

namespace Game_Hub.Model
{
    class Match
    {		
		public GameTitle Game { get; set; }
		public string PlayerOne { get; set; }
		public string PlayerTwo { get; set; }
		public DateTime InitialPlayTime { get; set; }
		public DateTime FinalPlayTime { get; set; }
		public int PlayerOneVictories { get; set; }
		public int PlayerTwoVictories { get; set; }
		public int Draws { get; set; }
		public int MatchesPlayed { get { return PlayerOneVictories + PlayerTwoVictories + Draws; } }

		[JsonConstructor]
		public Match(GameTitle game, string playerOne, string playerTwo, DateTime initialPlayTime, DateTime finalPlayTime, int playerOneVictories, int playerTwoVictories, int draws) : this(game, playerOne, playerTwo)
		{
			InitialPlayTime = initialPlayTime;
			FinalPlayTime = finalPlayTime;
			PlayerOneVictories = playerOneVictories;
			PlayerTwoVictories = playerTwoVictories;
			Draws = draws;
		}

		public Match(GameTitle game, string playerOneName, string playerTwoName)
        {
            Game = game;
			PlayerOne = playerOneName;
			PlayerTwo = playerTwoName;
			PlayerOneVictories = 0;
			PlayerTwoVictories = 0;
			Draws = 0;
        }
	}
}
