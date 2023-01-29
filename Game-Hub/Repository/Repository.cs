using Game_Hub.Model;
using Game_Hub.Utils;
using Game_Hub.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Game_Hub.Repository
{
	public abstract class Repository<T>
	{
		public void Save(T obj)
		{
			Util.CreateSaveDirectory();

			List<T> list = new List<T>();

			string filePath = Constants.SAVE_DATA_DIRECTORY + $@"\{typeof(T).Name}.json";
			

			if (File.Exists(filePath))
				list = Read();
			
			list.Add(obj);

			string json = JsonSerializer.Serialize(list, new JsonSerializerOptions() { WriteIndented = true });

			using StreamWriter outputFile = new StreamWriter(filePath);

			outputFile.WriteLine(json);
		}

		public List<T> Read()
		{
			Util.CreateSaveDirectory();

			List<T> list = new List<T>();			

			string filePath = Constants.SAVE_DATA_DIRECTORY + $@"\{typeof(T).Name}.json";

			try
			{
				using StreamReader inputFile = new StreamReader(filePath);
				string json = inputFile.ReadToEnd();
				list = JsonSerializer.Deserialize<List<T>>(json);
			}
			catch (Exception)
			{
				Display.ShowWarning("Não existem dados a serem carregados");
			}

			return list;
		}
	}
}
