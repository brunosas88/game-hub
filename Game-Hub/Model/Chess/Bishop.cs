using Game_Hub.Model.Enums;
using Game_Hub.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hub.Model.Chess
{
	public class Bishop : ChessPiece
	{
		public Bishop(ChessPieceColor color, string position, string sprite)
		{
			Color = color;
			Position = position;
			Sprite = sprite;
			IsCaptured = false;
		}

		public override List<string> Move(string currentPosition, List<ChessPieceInfo> infoGamePieces)
		{
			int[] realPosition =  Util.GetRealPosition(currentPosition);
			int currentLinePosition = realPosition[0], currentColumnPosition = realPosition[1], newLinePosition, newColumnPosition;
			int up, left, right, down;
			string newPosition;
			List<string> possibleMoves = new List<string>();

			newLinePosition = currentLinePosition;
			newColumnPosition = currentColumnPosition;
			for (up = currentLinePosition, left = currentColumnPosition; up > 0 && left > 0; up--, left--)
			{
				newLinePosition -= 1;
				newColumnPosition -= 1;
				newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
				if (!CheckMove(infoGamePieces, newPosition, possibleMoves)) break;
			}

			newLinePosition = currentLinePosition;
			newColumnPosition = currentColumnPosition;
			for (up = currentLinePosition, right = currentColumnPosition; up > 0 && right < 7; up--, right++)
			{
				newLinePosition -= 1;
				newColumnPosition += 1;
				newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
				if (!CheckMove(infoGamePieces, newPosition, possibleMoves)) break;
			}

			newLinePosition = currentLinePosition;
			newColumnPosition = currentColumnPosition;
			for (down = currentLinePosition, left = currentColumnPosition; down < 7 && left > 0; down++, left--)
			{
				newLinePosition += 1;
				newColumnPosition -= 1;
				newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
				if (!CheckMove(infoGamePieces, newPosition, possibleMoves)) break;
			}

			newLinePosition = currentLinePosition;
			newColumnPosition = currentColumnPosition;
			for (down = currentLinePosition, right = currentColumnPosition; down < 7 && right < 7; down++, right++)
			{
				newLinePosition += 1;
				newColumnPosition += 1;
				newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
				if (!CheckMove(infoGamePieces, newPosition, possibleMoves)) break;
			}

			return possibleMoves;
		}

		private bool CheckMove(List<ChessPieceInfo> infoGamePieces, string newPosition, List<string> possibleMoves)
		{
			if (infoGamePieces.Exists(piece => piece.Position == newPosition && piece.Color == this.Color))
				return false;
			else if (infoGamePieces.Exists(piece => piece.Position == newPosition && piece.Color != this.Color))
			{
				possibleMoves.Add(newPosition);
				return false;
			}
			else
			{
				possibleMoves.Add(newPosition);
				return true;
			}
		}
	}
}
