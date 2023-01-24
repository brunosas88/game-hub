using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Hub.Model.Enums;
using Game_Hub.Utils;

namespace Game_Hub.Model.Chess
{
    public class Pawn : ChessPiece
	{		
		public override string Position
		{
			get => _position;
			set
			{
				_position = value;
				if (_position != "-1")
				{
					int[] realPosition = Util.GetRealPosition(value);
					RealPositionRow = realPosition[0];
					RealPositionCol = realPosition[1];
				}
				if (RealPositionRow == 0 && Color == ChessPieceColor.WHITE)				
				IsPromoted = true;
				else if (RealPositionRow == 7 && Color == ChessPieceColor.BLACK)
				IsPromoted = true;
			}
		}
		public bool IsPromoted { get; set; }
		public bool IsFirstMove { get; set; }
		public string OriginalPosition { get; set; }

		public Pawn(ChessPieceColor color, string position, string sprite)
		{
			Color = color;
			Position = position;
			OriginalPosition = position;
			Sprite = sprite;
			IsCaptured = false;
			IsPromoted = false;
			IsFirstMove = true;
			Color = color;
		}

		public override List<string> Move(string currentPosition, List<ChessPieceInfo> infoGamePieces)
		{
			int[] realPosition = Util.GetRealPosition(currentPosition);
			int currentLinePosition = realPosition[0], currentColumnPosition = realPosition[1], newLinePosition, newColumnPosition;
			string newPosition, leftDiagonalPosition, rightDiagonalPosition;
			bool isMovimentPossible;
			List<string> possibleMoves = new List<string>();

			newLinePosition = Color == ChessPieceColor.WHITE ?
			currentLinePosition - 1 :
			currentLinePosition + 1;
			newPosition = Util.NominatePosition(newLinePosition, currentColumnPosition);
			isMovimentPossible = CheckMove(infoGamePieces, newPosition, possibleMoves);

			if (IsFirstMove && isMovimentPossible)
			{
				newLinePosition = Color == ChessPieceColor.WHITE ?
					currentLinePosition - 2 :
					currentLinePosition + 2;
				newPosition = Util.NominatePosition(newLinePosition, currentColumnPosition);
				CheckMove(infoGamePieces, newPosition, possibleMoves);
				IsFirstMove = false;
			}

			CheckAtack(infoGamePieces, currentLinePosition, currentColumnPosition, possibleMoves);

			return possibleMoves;
		}

		private void CheckAtack(List<ChessPieceInfo> infoGamePieces, int currentLinePosition, int currentColumnPosition, List<string> possibleMoves)
		{
			string leftDiagonalPosition;
			string rightDiagonalPosition;

			if (currentColumnPosition > 0 && currentColumnPosition < 8)
			{

				leftDiagonalPosition = Color == ChessPieceColor.WHITE ?
					Util.NominatePosition(currentLinePosition - 1, currentColumnPosition - 1) :
					Util.NominatePosition(currentLinePosition + 1, currentColumnPosition - 1);

				if ((infoGamePieces.Exists(piece => piece.Position == leftDiagonalPosition && piece.Color != this.Color)))
					possibleMoves.Add(leftDiagonalPosition);
			}

			if (currentColumnPosition >= 0 && currentColumnPosition < 7)
			{
				rightDiagonalPosition = Color == ChessPieceColor.WHITE ?
					Util.NominatePosition(currentLinePosition - 1, currentColumnPosition + 1) :
					Util.NominatePosition(currentLinePosition + 1, currentColumnPosition + 1);
				if ((infoGamePieces.Exists(piece => piece.Position == rightDiagonalPosition && piece.Color != this.Color)))
					possibleMoves.Add(rightDiagonalPosition);
			}
		}

		private bool CheckMove(List<ChessPieceInfo> infoGamePieces, string newPosition, List<string> possibleMoves)
		{

			if (!infoGamePieces.Exists(piece => piece.Position == newPosition))
			{
				possibleMoves.Add(newPosition);
				return true;
			}
			else
				return false;
		}


	}
}
