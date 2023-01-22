using Game_Hub.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hub.Model.Chess
{
	public class ChessBoard
	{
		private ChessPieceInfo[,] _board = new ChessPieceInfo[8,8];

		public ChessPieceInfo[,] Board { get => _board; }

		public void UpdateBoard(List<ChessPieceInfo> chessPieces)
		{
			CleanBoard();
			foreach (ChessPieceInfo chessPiece in chessPieces)
			{
				int[] position = Util.GetRealPosition(chessPiece.Position);
				int row = position[0], col = position[1];

				_board[row, col] = chessPiece;
			}
		}

		private void CleanBoard()
		{
			ChessPieceInfo cleanPiece = new();
			cleanPiece.Sprite = " ";
			for (int row = 0; row < _board.GetLength(0); row++)
			{
				for (int col = 0; col < _board.GetLength(1); col++)				
					_board[row, col] = cleanPiece;				
			}
		}


	}
}
