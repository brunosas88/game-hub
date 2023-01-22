using Game_Hub.Model.Enums;
using Game_Hub.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hub.Model.Chess
{
	public class Rook : ChessPiece
	{
		public Rook(ChessPieceColor color, string position, string sprite)
		{
			Color = color;
			Position = position;
			Sprite = sprite;
			IsCaptured = false;
		}

		public override List<string> Move(string currentPosition, List<ChessPieceInfo> infoGamePieces)
		{
			int[] realPosition = Util.GetRealPosition(currentPosition);
			int currentLinePosition = realPosition[0], currentColumnPosition = realPosition[1], newLinePosition, newColumnPosition;
			string newPosition;
			List<string> possibleMoves = new List<string>();

			newLinePosition = currentLinePosition;
			for (int up = currentLinePosition; up > 0; up--)
			{
				newLinePosition -= 1;
				newPosition = Util.NominatePosition(newLinePosition, currentColumnPosition);

				if (!CheckMove(infoGamePieces, newPosition, possibleMoves)) break;
			}

			newLinePosition = currentLinePosition;
			for (int down = currentLinePosition; down < 7; down++)
			{
				newLinePosition += 1;
				newPosition = Util.NominatePosition(newLinePosition, currentColumnPosition);

				if (!CheckMove(infoGamePieces, newPosition, possibleMoves)) break;
			}

			newColumnPosition = currentColumnPosition;
			for (int right = currentColumnPosition; right < 7; right++)
			{
				newColumnPosition += 1;
				newPosition = Util.NominatePosition(currentLinePosition, newColumnPosition);
				if (!CheckMove(infoGamePieces, newPosition, possibleMoves)) break;
			}

			newColumnPosition = currentColumnPosition;
			for (int left = currentColumnPosition; left > 0; left--)
			{
				newColumnPosition -= 1;
				newPosition = Util.NominatePosition(currentLinePosition, newColumnPosition);
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
