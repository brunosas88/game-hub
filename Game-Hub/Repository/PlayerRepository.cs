using Game_Hub.Model;
using Game_Hub.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Game_Hub.Repository
{
	class PlayerRepository : Repository<Player>
	{
		public void Update(Player obj)
		{
			Util.CreateSaveDirectory();

			List<Player> list = Read();

			string filePath = Constants.SAVE_DATA_DIRECTORY + $@"\{typeof(Player).Name}.json";

			Player playerToRemove = list.Find(playerToFind => playerToFind.Name == obj.Name);

			list.Remove(playerToRemove);
			
			list.Add(obj);

			string json = JsonSerializer.Serialize(list, new JsonSerializerOptions() { WriteIndented = true });

			using StreamWriter outputFile = new StreamWriter(filePath);

			outputFile.WriteLine(json);
		}
	}
}
