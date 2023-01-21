using Game_Hub.Model;
using Game_Hub.Model.Chess;
using Game_Hub.Model.Enums;
using Game_Hub.Utils;
using Game_Hub.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hub.Controller
{
	class ChessGame
	{
		public static void PlayChessGame(Player playerOne, Player playerTwo)
		{
			
			ChessBoard newChessBoard = new ChessBoard();
			MatchEvaluation matchInfoP1 = playerOne.MatchesInfo.FirstOrDefault(match => match.Game == GameTitle.XADREZ),
							matchInfoP2 = playerTwo.MatchesInfo.FirstOrDefault(match => match.Game == GameTitle.XADREZ);
			Player currentPlayer = playerOne;
			int position, winner;

			bool playerOneTurn = true;

			Display.ShowWarning("Para sair da partida aperte a tecla 0 (zero)");

			// iniciar posições
			List<ChessPiece> inGamePieces = InitializeChessPieces();
			List<ChessPieceInfo> infoGamePieces = UpdateInfoChessPieces(inGamePieces);
			List<string> blackCapturedPieces = new List<string>();
			List<string> whiteCapturedPieces = new List<string>();
			List<string> possibleMovesWithoutExistentPieces = new List<string>();


			// jogo 
			do
			{
				Display.GameInterface("Jogar!");
				Display.ShowWarning("Para sair da partida aperte a tecla 0 (zero)");
				possibleMovesWithoutExistentPieces.Clear();
				// mostrar tabuleiro com peças
				newChessBoard.UpdateBoard(infoGamePieces);
				Display.PrintChessBoard(newChessBoard.Board);
				Display.PrintCapturedChessPieces(blackCapturedPieces, whiteCapturedPieces);
				// pegar posição do usuario
				Console.Write("\nInsira posição: ");
				string originalPosition = Console.ReadLine();
				// encontrar peça na posição
				var pieceToMove = inGamePieces.Find(piece => piece.Position == originalPosition);
				// encontrar os possíveis movimentos
				List<string> allPossibleMoves = pieceToMove.Move(originalPosition, infoGamePieces);
				// mostrar possiveis movimentos
				Console.Write("\nMovimentos Possiveis: [");
				foreach (var item in allPossibleMoves)
					Console.Write($" -{item}- ");
				Console.Write("]\n");
				// escolher entre os movimentos possiveis
				string newPosition = Console.ReadLine();
				// verificar se houve captura e mudar posição da peça
				infoGamePieces = UpdateGamePieces(inGamePieces, pieceToMove, originalPosition, newPosition, ref blackCapturedPieces, ref whiteCapturedPieces);

			} while (true);
		}



		private static List<ChessPieceInfo> UpdateGamePieces(List<ChessPiece> inGamePieces, ChessPiece pieceToMove, string originalPosition, string? newPosition, ref List<string> blackCapturedPieces, ref List<string> whiteCapturedPieces)
		{
			bool existsCapturedPiece = inGamePieces.Exists(capturedPiece => capturedPiece.Position == newPosition && capturedPiece.Color != pieceToMove.Color);

			if (existsCapturedPiece)
			{
				var pieceToRemove = inGamePieces.Find(piece => piece.Position == newPosition);
				pieceToRemove.Position = "-1";
				pieceToRemove.IsCaptured = true;

				if (pieceToRemove.Color != ChessPieceColor.WHITE)
					blackCapturedPieces.Add(pieceToRemove.Sprite);
				else
					whiteCapturedPieces.Add(pieceToRemove.Sprite);
			}

			pieceToMove.Position = newPosition;

			return UpdateInfoChessPieces(inGamePieces);
		}

		public static List<ChessPieceInfo> UpdateInfoChessPieces(List<ChessPiece> inGamePieces)
		{
			List<ChessPieceInfo> piecesInfoList = new();
			ChessPieceInfo newPiece = new();

			foreach (ChessPiece piece in inGamePieces)
			{
				if (!piece.IsCaptured)
				{
					newPiece.Position = piece.Position;
					newPiece.Color = piece.Color;
					newPiece.Sprite = piece.Sprite;
					piecesInfoList.Add(newPiece);
				}

			}
			return piecesInfoList;
		}

		public static List<ChessPiece> InitializeChessPieces()
		{
			// Creating king pieces
			King kingW = new King(ChessPieceColor.WHITE, "e1", Constants.KING_SPRITE);
			King kingB = new King(ChessPieceColor.BLACK, "e8", Constants.KING_SPRITE);
			// Creating queen pieces
			Queen queenW = new Queen(ChessPieceColor.WHITE, "d1", Constants.QUEEN_SPRITE);
			Queen queenB = new Queen(ChessPieceColor.BLACK, "d8", Constants.QUEEN_SPRITE);
			// Creating bishops pieces
			Bishop bishopWC = new Bishop(ChessPieceColor.WHITE, "c1", Constants.BISHOP_SPRITE);
			Bishop bishopWF = new Bishop(ChessPieceColor.WHITE, "f1", Constants.BISHOP_SPRITE);
			Bishop bishopBC = new Bishop(ChessPieceColor.BLACK, "c8", Constants.BISHOP_SPRITE);
			Bishop bishopBF = new Bishop(ChessPieceColor.BLACK, "f8", Constants.BISHOP_SPRITE);
			// Creating knight pieces
			Knight knightWB = new Knight(ChessPieceColor.WHITE, "b1", Constants.KNIGHT_SPRITE);
			Knight knightWG = new Knight(ChessPieceColor.WHITE, "g1", Constants.KNIGHT_SPRITE);
			Knight knightBB = new Knight(ChessPieceColor.BLACK, "b8", Constants.KNIGHT_SPRITE);
			Knight knightBG = new Knight(ChessPieceColor.BLACK, "g8", Constants.KNIGHT_SPRITE);
			// Creating rook pieces
			Rook rookWA = new Rook(ChessPieceColor.WHITE, "a1", Constants.ROOK_SPRITE);
			Rook rookWH = new Rook(ChessPieceColor.WHITE, "h1", Constants.ROOK_SPRITE);
			Rook rookBA = new Rook(ChessPieceColor.BLACK, "a8", Constants.ROOK_SPRITE);
			Rook rookBH = new Rook(ChessPieceColor.BLACK, "h8", Constants.ROOK_SPRITE);
			// Creating pawn pieces
			Pawn pawnWA = new Pawn(ChessPieceColor.WHITE, "a2", Constants.PAWN_SPRITE);
			Pawn pawnWB = new Pawn(ChessPieceColor.WHITE, "b2", Constants.PAWN_SPRITE);
			Pawn pawnWC = new Pawn(ChessPieceColor.WHITE, "c2", Constants.PAWN_SPRITE);
			Pawn pawnWD = new Pawn(ChessPieceColor.WHITE, "d2", Constants.PAWN_SPRITE);
			Pawn pawnWE = new Pawn(ChessPieceColor.WHITE, "e2", Constants.PAWN_SPRITE);
			Pawn pawnWF = new Pawn(ChessPieceColor.WHITE, "f2", Constants.PAWN_SPRITE);
			Pawn pawnWG = new Pawn(ChessPieceColor.WHITE, "g2", Constants.PAWN_SPRITE);
			Pawn pawnWH = new Pawn(ChessPieceColor.WHITE, "h2", Constants.PAWN_SPRITE);
			Pawn pawnBA = new Pawn(ChessPieceColor.BLACK, "a7", Constants.PAWN_SPRITE);
			Pawn pawnBB = new Pawn(ChessPieceColor.BLACK, "b7", Constants.PAWN_SPRITE);
			Pawn pawnBC = new Pawn(ChessPieceColor.BLACK, "c7", Constants.PAWN_SPRITE);
			Pawn pawnBD = new Pawn(ChessPieceColor.BLACK, "d7", Constants.PAWN_SPRITE);
			Pawn pawnBE = new Pawn(ChessPieceColor.BLACK, "e7", Constants.PAWN_SPRITE);
			Pawn pawnBF = new Pawn(ChessPieceColor.BLACK, "f7", Constants.PAWN_SPRITE);
			Pawn pawnBG = new Pawn(ChessPieceColor.BLACK, "g7", Constants.PAWN_SPRITE);
			Pawn pawnBH = new Pawn(ChessPieceColor.BLACK, "h7", Constants.PAWN_SPRITE);

			return new List<ChessPiece>()
			{   kingW, kingB, queenW, queenB, bishopWC, bishopWF, bishopBC, bishopBF, knightWB, knightWG,
				knightBB, knightBG, rookWA, rookWH, rookBA, rookBH, pawnWA, pawnWB, pawnWC, pawnWD, pawnWE,
				pawnWF, pawnWG, pawnWH, pawnBA, pawnBB, pawnBC, pawnBD, pawnBE, pawnBF, pawnBG, pawnBH
			};
		}
	}
}
