using Game_Hub.Model.Chess;
using Game_Hub.Model.Enums;
using Game_Hub.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hub.Model.BattleShip
{
	public class BattleShipBoard
	{
		private BattleShipFieldInfo[,] _board = new	BattleShipFieldInfo[10,10];

		public BattleShipFieldInfo[,] Board { get => _board; }

		public BattleShipBoard()
		{
			CreateBoard();
		}

		public void UpdateBoard(List<BattleShipFieldInfo> fields)
		{
			int row, col;
			foreach (BattleShipFieldInfo field in fields)
			{
				col = Constants.COLUMN_REFERENCE[field.Position[0].ToString()];
				row = int.Parse(field.Position.Substring(1)) - 1;

				_board[row, col] = field;
			}
		}

		private void CreateBoard()
		{
			BattleShipFieldInfo cleanField = new();
			cleanField.IsShot = false;
			cleanField.IsShip = false;
			cleanField.Position = string.Empty;
			for (int row = 0; row < _board.GetLength(0); row++)
			{
				for (int col = 0; col < _board.GetLength(1); col++)
					_board[row, col] = cleanField;
			}
		}

	}
}
