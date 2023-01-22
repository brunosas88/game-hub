using Game_Hub.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Game_Hub.Model
{
    class Player
    {
		public string Name { get; set; }
		public int PlayOrder { get; set; }
		public string Password { get; private set; }
		public List<MatchEvaluation> MatchesInfo { get; set; }

		[JsonConstructor]
		public Player(string name, int playOrder, string password, List<MatchEvaluation> matchesInfo)
		{
			Name = name;
			PlayOrder = playOrder;
			Password = password;
			MatchesInfo = matchesInfo;
		}

		public Player(string nome, string password)
		{
			Name = nome;			
			Password = password;
			MatchesInfo = new List<MatchEvaluation>();
			foreach (GameTitle game in Enum.GetValues(typeof(GameTitle)) )			
				MatchesInfo.Add(new MatchEvaluation(game));
		}
	}
}
