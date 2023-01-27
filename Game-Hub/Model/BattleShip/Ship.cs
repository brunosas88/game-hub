using Game_Hub.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hub.Model.BattleShip
{
	public class Ship
	{
		private List<string> _position;
		public string Name { get; set; }		
		public int ShotTimes { get; set; }
		public bool IsSunk { get; set; }
		public int ShipSize { get; set; }

		public Ship(string name, int shipSize)
		{
			Name = name;
			ShipSize = shipSize;
			ShotTimes = shipSize;
			_position = new List<string>();
		}

		public List<string> Position { get => _position; }

		public void SetPosition (List<string>? prohibitedPositions = null)
		{
			Random randomPosition = new Random();
			int direction;
			int fixedPosition, variablePosition, anchorVariablePosition;

			string newPosition;
			bool generateNewPosition;

			do
			{
				generateNewPosition = false;
				direction = randomPosition.Next(2);
				fixedPosition = randomPosition.Next(10);				
				anchorVariablePosition = randomPosition.Next(10);
				variablePosition = anchorVariablePosition;

				for (int i = 0; i < ShipSize; i++)
				{
					newPosition = (direction == Constants.HORIZONTAL_POSITION) ?
						Constants.COLUMN_REFERENCE.First(entries => entries.Value == variablePosition).Key + (fixedPosition + 1).ToString()
						:
						Constants.COLUMN_REFERENCE.First(entries => entries.Value == fixedPosition).Key + (variablePosition + 1).ToString()
						;
					
					if (prohibitedPositions != null && prohibitedPositions.Contains(newPosition))
					{
						generateNewPosition = true;
						break;
					}

					_position.Add(newPosition);

					variablePosition = (anchorVariablePosition + ShipSize > 10) ? variablePosition - 1 : variablePosition + 1;
				}

			} while (generateNewPosition);
		}

		public void GotShot()
		{
			ShotTimes--;
		}
	}
}
