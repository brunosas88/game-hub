using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Game_Hub.Model;

namespace Game_Hub.Utils
{
    class Util
    {
		public static void CreateSaveDirectory()
		{
			if (!Directory.Exists(Constants.SAVE_DATA_DIRECTORY))
				Directory.CreateDirectory(Constants.SAVE_DATA_DIRECTORY);
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

		public static string CaptalizeString(string input)
		{
			if (string.IsNullOrEmpty(input))			
				return string.Empty;

			string[] str = input.ToLower().Split();
			string result = string.Empty;

			foreach (string item in str)
			{
				if (item.Length > 3)
					result += item[0].ToString().ToUpper() + item[1..] + " ";
				else				
					result += item + " "; 				
			}							
			
			return result.TrimEnd();
		}
	}
}
