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

			bool playerOneTurn = true, invalidPieceWarning = false;

			List<ChessPiece> inGamePieces = InitializeChessPieces();
			List<ChessPieceInfo> infoGamePieces = UpdateInfoChessPieces(inGamePieces);
			List<string> blackCapturedPieces = new(), whiteCapturedPieces = new(), possibleMoves = new(),
						 blackPiecesPositions = new(), whitePiecesPositions = new();

			string playerEntry, newPosition,
				   choosePieceMessage = "Insira posição da peça:",
				   choosePositionToMoveMessage = Constants.MESSAGE_CHOOSE_POSITION_TO_MOVE;

			ChessPiece whiteKing = inGamePieces.Find(piece => (piece.Sprite == Constants.KING_SPRITE) && (piece.Color == ChessPieceColor.WHITE));
			ChessPiece blackKing = inGamePieces.Find(piece => (piece.Sprite == Constants.KING_SPRITE) && (piece.Color == ChessPieceColor.BLACK));
			ChessPiece pieceToMove;

			do
			{
				blackPiecesPositions = infoGamePieces.FindAll(pieces => pieces.Color == ChessPieceColor.BLACK)
										  .Select(piece => piece.Position).ToList();
				whitePiecesPositions = infoGamePieces.FindAll(pieces => pieces.Color == ChessPieceColor.WHITE)
														  .Select(piece => piece.Position).ToList();

				newChessBoard.UpdateBoard(infoGamePieces);

				ShowChessGame(newChessBoard.Board, currentPlayer.Name, playerOneTurn, blackCapturedPieces, whiteCapturedPieces);

				playerEntry = playerOneTurn ? 
					GetPosition(whitePiecesPositions, choosePieceMessage, newChessBoard.Board, currentPlayer.Name, playerOneTurn, blackCapturedPieces, whiteCapturedPieces, invalidPieceWarning) : 
					GetPosition(blackPiecesPositions, choosePieceMessage, newChessBoard.Board, currentPlayer.Name, playerOneTurn, blackCapturedPieces, whiteCapturedPieces, invalidPieceWarning);

				if (invalidPieceWarning)
					invalidPieceWarning = false;

				if (ValidatePlayerEntry(playerEntry))
				{
					pieceToMove = inGamePieces.Find(piece => piece.Position == playerEntry);

					possibleMoves = pieceToMove.Move(playerEntry, infoGamePieces);

					if (possibleMoves.Any())
					{
						newPosition = GetPosition(possibleMoves, choosePositionToMoveMessage, newChessBoard.Board, currentPlayer.Name, playerOneTurn, blackCapturedPieces, whiteCapturedPieces);

						if (ValidatePlayerEntry(newPosition))
						{
							infoGamePieces = UpdateGamePieces(inGamePieces, pieceToMove, newPosition, ref blackCapturedPieces, ref whiteCapturedPieces);
							playerOneTurn = !playerOneTurn;
							currentPlayer = playerOneTurn ? playerOne : playerTwo;
						}

						if (newPosition == "0" && (pieceToMove is Pawn pawn) && pawn.Position == pawn.OriginalPosition)
							pawn.IsFirstMove = true;

						playerEntry = newPosition;
					}
					else
						invalidPieceWarning = true;
				}
				else if (playerEntry.ToLower() == "e")
				{
					GameHubView.ShowWarning("Outro Jogador Consente na Declaração de Empate?", false);
					GameHubView.ShowWarning("S - SIM / Qualquer outra tecla - NÃO", false);
					playerEntry = GameHubView.FormatConsoleReadLine();
				}


			} while (playerEntry.ToLower() != "r" && !blackKing.IsCaptured && !whiteKing.IsCaptured && playerEntry.ToLower() != "s");

			CalculateResults(playerOne, playerTwo, matchInfoP1, matchInfoP2, playerOneTurn, playerEntry, whiteKing, blackKing);
		}

		private static void ShowChessGame(ChessPieceInfo[,] board, string playerName, bool playerOneTurn, List<string> blackCapturedPieces, List<string> whiteCapturedPieces, List<string>? possibleMoves = null)
		{
			ChessGameView.ShowChessInstructions(playerOneTurn, playerName);

			ChessGameView.PrintChessBoard(board, possibleMoves);

			ChessGameView.PrintChessMatchInfo(blackCapturedPieces, whiteCapturedPieces);
		}

		private static void CalculateResults(Player playerOne, Player playerTwo, MatchEvaluation matchInfoP1, MatchEvaluation matchInfoP2, bool playerOneTurn, string playerEntry, ChessPiece whiteKing, ChessPiece blackKing)
		{
			if (blackKing.IsCaptured)
			{
				matchInfoP1.Victories++;
				matchInfoP2.Defeats++;
				GameHubView.ShowWarning($"Jogador {playerOne.Name} venceu!");
			}
			else if (whiteKing.IsCaptured)
			{
				matchInfoP1.Defeats++;
				matchInfoP2.Victories++;
				GameHubView.ShowWarning($"Jogador {playerTwo.Name} venceu!");
			}
			else if (playerEntry == "s")
			{
				matchInfoP1.Draws++;
				matchInfoP2.Draws++;
				GameHubView.ShowWarning("Jogadores finalizaram a partida sem vencedores");
			}
			else
			{
				if (playerOneTurn)
				{
					matchInfoP1.Defeats++;
					matchInfoP2.Victories++;
					GameHubView.ShowWarning($"Jogador {playerTwo.Name} venceu!");
				}
				else
				{
					matchInfoP1.Victories++;
					matchInfoP2.Defeats++;
					GameHubView.ShowWarning($"Jogador {playerOne.Name} venceu!");
				}
			}
		}

		private static bool ValidatePlayerEntry(string playerEntry)
		{
			return playerEntry != "0" && playerEntry.ToLower() != "r" && playerEntry.ToLower() != "e";
		}

		private static string GetPosition(List<string> positions, string message, ChessPieceInfo[,] board, string playerName, bool playerOneTurn, List<string> blackCapturedPieces, List<string> whiteCapturedPieces, bool showWarning = false)
		{
			string? position;			
			
			do
			{
				if (message == Constants.MESSAGE_CHOOSE_POSITION_TO_MOVE)
					ShowChessGame(board, playerName, playerOneTurn, blackCapturedPieces, whiteCapturedPieces, positions);
				else
					ShowChessGame(board, playerName, playerOneTurn, blackCapturedPieces, whiteCapturedPieces);

				if (showWarning)	
					GameHubView.ShowWarning("Insira uma posição válida", false);
				

				Console.WriteLine(GameHubView.AlignMessage(message));
				position = GameHubView.FormatConsoleReadLine();
				showWarning = true;

			} while ( !positions.Contains(position) && ValidatePlayerEntry(position));

			return position;
		}

		private static List<ChessPieceInfo> UpdateGamePieces(List<ChessPiece> inGamePieces, ChessPiece pieceToMove, string? newPosition, ref List<string> blackCapturedPieces, ref List<string> whiteCapturedPieces)
		{
			bool existsCapturedPiece = inGamePieces.Exists(capturedPiece => capturedPiece.Position == newPosition && capturedPiece.Color != pieceToMove.Color);
			int option;
			List<string> promotionPieces = new List<string>();
			promotionPieces.AddRange(Enum.GetValues(typeof(PromotionChessPieces))
										  .Cast<PromotionChessPieces>()
										  .Select(title => title.ToString())
										  .ToList());
			if (existsCapturedPiece)
			{
				var pieceToRemove = inGamePieces.Find(piece => piece.Position == newPosition);
				pieceToRemove.Position = Constants.OUT_OF_GAME;
				pieceToRemove.IsCaptured = true;

				if (pieceToRemove.Color != ChessPieceColor.WHITE)
					blackCapturedPieces.Add(pieceToRemove.Sprite);
				else
					whiteCapturedPieces.Add(pieceToRemove.Sprite);
			}

			pieceToMove.Position = newPosition;

			if ((pieceToMove is Pawn pawn) && pawn.IsPromoted)
			{

				option = GameHubView.ShowMenu(promotionPieces, $"Promoção de Peão {pawn.Color} na casa {pawn.Position.ToUpper()}");
				switch ((PromotionChessPieces)option)
				{
					case PromotionChessPieces.DAMA:
						Queen queen = new Queen(pawn.Color, pawn.Position, Constants.QUEEN_SPRITE);
						inGamePieces.Add(queen);
						break;
					case PromotionChessPieces.BISPO:
						Bishop bishop = new Bishop(pawn.Color, pawn.Position, Constants.BISHOP_SPRITE);
						inGamePieces.Add(bishop);
						break;
					case PromotionChessPieces.CAVALO:
						Knight knight = new Knight(pawn.Color, pawn.Position, Constants.KNIGHT_SPRITE);
						inGamePieces.Add(knight);
						break;
					case PromotionChessPieces.TORRE:
						Rook rook = new Rook(pawn.Color, pawn.Position, Constants.ROOK_SPRITE);
						inGamePieces.Add(rook);
						break;
				}
				inGamePieces.Remove(pawn);
			}

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
