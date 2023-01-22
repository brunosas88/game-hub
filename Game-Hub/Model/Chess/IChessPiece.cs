using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hub.Model.Chess
{
	public interface IChessPiece
	{
		List<string> Move(string currentPosition, List<ChessPieceInfo> infoGamePieces);
	}
}
