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
	public class King : ChessPiece
	{
		public King(ChessPieceColor color, string position, string sprite)
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
			int up, left, right, down;
			string newPosition;
			List<string> possibleMoves = new List<string>();

			ResetValues(currentLinePosition, currentColumnPosition, out newLinePosition, out newColumnPosition, out up, out left, out right, out down);
			if (up > 0 && left > 0)
			{
				newLinePosition -= 1;
				newColumnPosition -= 1;
				newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
				CheckMove(infoGamePieces, newPosition, possibleMoves);
			}

			ResetValues(currentLinePosition, currentColumnPosition, out newLinePosition, out newColumnPosition, out up, out left, out right, out down);
			if (up > 0 && right < 7)
			{
				newLinePosition -= 1;
				newColumnPosition += 1;
				newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
				CheckMove(infoGamePieces, newPosition, possibleMoves);
			}

			ResetValues(currentLinePosition, currentColumnPosition, out newLinePosition, out newColumnPosition, out up, out left, out right, out down);
			if (down < 7 && left > 0)
			{
				newLinePosition += 1;
				newColumnPosition -= 1;
				newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
				CheckMove(infoGamePieces, newPosition, possibleMoves);
			}

			ResetValues(currentLinePosition, currentColumnPosition, out newLinePosition, out newColumnPosition, out up, out left, out right, out down);
			if (down < 7 && right < 7)
			{
				newLinePosition += 1;
				newColumnPosition += 1;
				newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
				CheckMove(infoGamePieces, newPosition, possibleMoves);
			}

			newLinePosition = currentLinePosition;
			up = currentLinePosition;
			if (up > 0)
			{
				newLinePosition -= 1;
				newPosition = Util.NominatePosition(newLinePosition, currentColumnPosition);
				CheckMove(infoGamePieces, newPosition, possibleMoves);
			}

			newLinePosition = currentLinePosition;
			down = currentLinePosition;
			if (down < 7)
			{
				newLinePosition += 1;
				newPosition = Util.NominatePosition(newLinePosition, currentColumnPosition);
				CheckMove(infoGamePieces, newPosition, possibleMoves);
			}

			newColumnPosition = currentColumnPosition;
			right = currentColumnPosition;
			if (right < 7)
			{
				newColumnPosition += 1;
				newPosition = Util.NominatePosition(currentLinePosition, newColumnPosition);
				CheckMove(infoGamePieces, newPosition, possibleMoves);
			}

			newColumnPosition = currentColumnPosition;
			left = currentColumnPosition;
			if (left > 0)
			{
				newColumnPosition -= 1;
				newPosition = Util.NominatePosition(currentLinePosition, newColumnPosition);
				CheckMove(infoGamePieces, newPosition, possibleMoves);
			}

			return possibleMoves;
		}

		private void ResetValues(int currentLinePosition, int currentColumnPosition, out int newLinePosition, out int newColumnPosition, out int up, out int left, out int right, out int down)
		{
			newLinePosition = currentLinePosition;
			newColumnPosition = currentColumnPosition;
			up = currentLinePosition;
			left = currentColumnPosition;
			down = currentLinePosition;
			right = currentColumnPosition;
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
