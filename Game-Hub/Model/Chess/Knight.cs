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
	public class Knight : ChessPiece
	{
		public Knight(ChessPieceColor color, string position, string sprite)
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

			// posições acima 
			newLinePosition = currentLinePosition - 2;
			if (newLinePosition >= 0)
			{
				newColumnPosition = currentColumnPosition + 1;
				if (newColumnPosition <= 7)
				{
					newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
					CheckMove(infoGamePieces, newPosition, possibleMoves);
				}

				newColumnPosition = currentColumnPosition - 1;
				if (newColumnPosition >= 0)
				{
					newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
					CheckMove(infoGamePieces, newPosition, possibleMoves);
				}
				;
			}

			newLinePosition = currentLinePosition - 1;
			if (newLinePosition >= 0)
			{
				newColumnPosition = currentColumnPosition + 2;
				if (newColumnPosition <= 7)
				{
					newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
					CheckMove(infoGamePieces, newPosition, possibleMoves);
				}

				newColumnPosition = currentColumnPosition - 2;
				if (newColumnPosition >= 0)
				{
					newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
					CheckMove(infoGamePieces, newPosition, possibleMoves);
				}
			}

			// posições abaixo 
			newLinePosition = currentLinePosition + 1;
			if (newLinePosition <= 7)
			{
				newColumnPosition = currentColumnPosition + 2;
				if (newColumnPosition <= 7)
				{
					newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
					CheckMove(infoGamePieces, newPosition, possibleMoves);
				}

				newColumnPosition = currentColumnPosition - 2;
				if (newColumnPosition >= 0)
				{
					newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
					CheckMove(infoGamePieces, newPosition, possibleMoves);
				}
			}

			newLinePosition = currentLinePosition + 2;
			if (newLinePosition <= 7)
			{
				newColumnPosition = currentColumnPosition + 1;
				if (newColumnPosition <= 7)
				{
					newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
					CheckMove(infoGamePieces, newPosition, possibleMoves);
				}

				newColumnPosition = currentColumnPosition - 1;
				if (newColumnPosition >= 0)
				{
					newPosition = Util.NominatePosition(newLinePosition, newColumnPosition);
					CheckMove(infoGamePieces, newPosition, possibleMoves);
				}
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
