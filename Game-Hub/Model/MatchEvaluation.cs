using Game_Hub.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Game_Hub.Model
{
	public class MatchEvaluation
	{
		public GameTitle Game { get; set; }
		public int Points { get; set; }
		public int Victories { get; set; }
		public int Draws { get; set ; }
		public int Defeats { get; set; }		

		[JsonConstructor]
		public MatchEvaluation(GameTitle game, int points, int victories, int draws, int defeats)
		{
			Game = game;
			Points = points;
			Victories = victories;
			Draws = draws;
			Defeats = defeats;
		}

		public MatchEvaluation(GameTitle game) 
		{
			Game = game;
			Points = 0;
			Victories = 0;
			Draws= 0;
			Defeats = 0;
		}
	}
}
