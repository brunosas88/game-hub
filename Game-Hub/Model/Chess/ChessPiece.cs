using Game_Hub.Model.Enums;
using Game_Hub.Model.Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Hub.Utils;

namespace Game_Hub.Model.Chess
{
	public abstract class ChessPiece : IChessPiece
	{

		protected string _position;
		public ChessPieceColor Color { get; set; }
		public virtual string Position
		{
			get => _position;
			set
			{
				_position = value;
				if (_position != Constants.OUT_OF_GAME)
				{
					int[] realPosition = Util.GetRealPosition(value);
					RealPositionRow = realPosition[0];
					RealPositionCol = realPosition[1];
				}
			}
		}
		public string Sprite { get; set; }
		public bool IsCaptured { get; set; }
		public int RealPositionCol { get; set; }
		public int RealPositionRow { get; set; }

		public abstract List<string> Move(string currentPosition, List<ChessPieceInfo> infoGamePieces);
	}
}
