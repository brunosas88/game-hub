using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Hub.Util;

namespace Game_Hub.Model.Chess
{
    public class Pawn : IChessPiece
	{
		public string Id { get; set; }
		public ChessPieceColor Color { get; set; }
		public string Position { get; set; }
		public string Sprite { get; set; }
		public bool IsCaptured { get; set; }
		public bool IsPromoted { get; set; }
		public bool IsFirstMove { get; private set; }

		public Pawn(string id, ChessPieceColor color, string position, string sprite)
		{
			Id = id;
			Color = color;
			Position = position;
			Sprite = sprite;
			IsCaptured = false;
			IsPromoted = false;
			IsFirstMove = true;
			Color = color;
		}

		public List<string> Move(string currentPosition)
		{
			int[] realPosition = Utils.GetRealPosition(currentPosition);
			int currentLinePosition = realPosition[0], currentColumnPosition = realPosition[1], newLinePosition, newColumnPosition;
			List<string> possibleMoves = new List<string>();
			string possiblePosition;

			if (IsFirstMove)
			{
				newColumnPosition = Color.Equals(ChessPieceColor.WHITE) ? 
					currentColumnPosition - 2 :
					currentColumnPosition + 2;
				possibleMoves.Add(Utils.NominatePosition(currentLinePosition,newColumnPosition));
				IsFirstMove = false;
			}

			newColumnPosition = Color.Equals(ChessPieceColor.WHITE) ?
					currentColumnPosition - 1 :
					currentColumnPosition + 1;
			possibleMoves.Add(Utils.NominatePosition(currentLinePosition, newColumnPosition));

			return possibleMoves;
		}


	}
}
