using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Game_Hub.Model;

namespace Game_Hub.Utils
{
    class Util
    {

        public static void WriteJSON(List<Player> players, List<Match> matches)
        {
            string dataDirectory = CheckPathDirectory();

			string filePath = dataDirectory + Constants.PLAYER_SAVE_FILE;
			string jsonStringPlayers = JsonSerializer.Serialize(players, new JsonSerializerOptions() { WriteIndented = true });

            using (StreamWriter outputFile = new StreamWriter(filePath))
                outputFile.WriteLine(jsonStringPlayers);

			filePath = dataDirectory + Constants.MATCH_SAVE_FILE;
			string jsonStringMatches = JsonSerializer.Serialize(matches, new JsonSerializerOptions() { WriteIndented = true });

            using (StreamWriter outputFile = new StreamWriter(filePath))
                outputFile.WriteLine(jsonStringMatches);
        }

		private static string CheckPathDirectory()
		{
			string workingDirectory = Environment.CurrentDirectory;
			string dataDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName + @"\Data";
			if (!Directory.Exists(dataDirectory))
				Directory.CreateDirectory(dataDirectory);

			return dataDirectory;
		}

		public static void ReadJSON(ref List<Player> players, ref List<Match> matches)
        {
			string dataDirectory = CheckPathDirectory();

			string filePath = dataDirectory + Constants.PLAYER_SAVE_FILE;
			if (File.Exists(filePath))
            {
                using (StreamReader inputFile = new StreamReader(filePath))
                {
                    string json = inputFile.ReadToEnd();
                    players = JsonSerializer.Deserialize<List<Player>>(json);
                }
            }

			filePath = dataDirectory + Constants.MATCH_SAVE_FILE;
			if (File.Exists(filePath))
            {
                using (StreamReader inputFile = new StreamReader(filePath))
                {
                    string json = inputFile.ReadToEnd();
                    matches = JsonSerializer.Deserialize<List<Match>>(json);
                }
            }
        }

		public static int[] GetRealPosition(string position)
		{
			int row = Constants.LINE_REFERENCE[position[1].ToString()];
			int column = Constants.COLUMN_REFERENCE[position[0].ToString()];
			int[] realPosition = { row, column };
			return realPosition;
		}

		public static string NominatePosition(int line, int column)
		{
			string nominatedLine = Constants.LINE_REFERENCE.First(entries => entries.Value == line).Key;
			string nominatedColumn = Constants.COLUMN_REFERENCE.First(entries => entries.Value == column).Key;

			return nominatedColumn + nominatedLine;
		}


	}
}
