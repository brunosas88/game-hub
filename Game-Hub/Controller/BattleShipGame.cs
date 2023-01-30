using Game_Hub.Model;
using Game_Hub.Model.BattleShip;
using Game_Hub.Model.Enums;
using Game_Hub.Utils;
using Game_Hub.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Game_Hub.Controller
{
	class BattleShipGame
	{
		public static void PlayBattleShipGame(Player playerOne, Player playerTwo)
		{
			string field = string.Empty, playerEntry = string.Empty;
			bool showWarning = false, playerOneTurn = true;

			BattleShipFieldInfo newField = new BattleShipFieldInfo();
			Ship shotShip;

			BattleShipBoard battleShipBoardP1 = new BattleShipBoard();
			BattleShipBoard battleShipBoardP2 = new BattleShipBoard();

			MatchEvaluation matchInfoP1 = playerOne.MatchesInfo.FirstOrDefault(match => match.Game == GameTitle.BATALHA_NAVAL),
							matchInfoP2 = playerTwo.MatchesInfo.FirstOrDefault(match => match.Game == GameTitle.BATALHA_NAVAL);

			List<Ship> shipsP1 = InitializeShips();
			List<Ship> shipsP2 = InitializeShips();

			List<BattleShipFieldInfo> occupiedFieldsP1 = InitializeFields(shipsP1);
			List<BattleShipFieldInfo> occupiedFieldsP2 = InitializeFields(shipsP2);

			battleShipBoardP1.UpdateBoard(occupiedFieldsP1);
			battleShipBoardP2.UpdateBoard(occupiedFieldsP2);

			Player currentPlayer = playerOne;
			Player adversaryPlayer = playerTwo;
			BattleShipBoard boardToBomb;
			List<BattleShipFieldInfo> fieldsToBomb;
			List<Ship> shipsToBomb;

			do
			{
				boardToBomb = playerOneTurn ? battleShipBoardP2 : battleShipBoardP1;
				fieldsToBomb = playerOneTurn ? occupiedFieldsP2 : occupiedFieldsP1;
				shipsToBomb = playerOneTurn ? shipsP2 : shipsP1;

				Display.ShowBattleShipInstructions(currentPlayer.Name, adversaryPlayer.Name);

				Display.PrintBattleFieldBoard(boardToBomb.Board);

				BattleShipMatchInfo(playerOne.Name, playerTwo.Name, shipsP1, shipsP2);

				if (showWarning)
					Display.ShowWarning("Posição Inválida", false);

				Console.WriteLine();
				Console.WriteLine(Display.AlignMessage("Insira uma posição:"));
				playerEntry = Display.FormatConsoleReadLine();

				if (ValidatePlayerEntry(playerEntry, fieldsToBomb))
				{

					if (fieldsToBomb.Exists(field => field.Position == playerEntry))
					{
						newField = fieldsToBomb.Find(field => field.Position == playerEntry);

						newField.IsShot = true;

						if (newField.IsShip)
						{
							shotShip = shipsToBomb.Find(ship => ship.Position.Contains(playerEntry));
							shotShip.GotShot();
						}
					}
					else
					{
						newField.IsShip = false;
						newField.IsShot = true;
						newField.Position = playerEntry;
					}

					fieldsToBomb.Add(newField);

					boardToBomb.UpdateBoard(fieldsToBomb);

					Display.ShowBatlheShipFieldCurrentPlay(boardToBomb.Board);

					playerOneTurn = !playerOneTurn;
					currentPlayer = playerOneTurn ? playerOne : playerTwo;
					adversaryPlayer = playerOneTurn ? playerTwo : playerOne;

					showWarning = false;
				}
				else if (playerEntry.ToLower() == "e")
				{
					Display.ShowWarning("Outro Jogador Consente na Declaração de Empate?", false);
					Display.ShowWarning("S - SIM / Qualquer outra tecla - NÃO", false);
					playerEntry = Display.FormatConsoleReadLine();
					showWarning = false;
				}
				else
					showWarning = true;


			} while (playerEntry.ToLower() != "e" && playerEntry.ToLower() != "s" && shipsP1.Exists(ship => ship.IsSunk == false) && shipsP2.Exists(ship => ship.IsSunk == false));

			CalculateResults(playerOne, playerTwo, playerEntry, playerOneTurn, matchInfoP1, matchInfoP2, shipsP1, shipsP2);
		}

		private static void BattleShipMatchInfo(string nameP1, string nameP2, List<Ship> shipsP1, List<Ship> shipsP2)
		{
			List<string> sunkenShipsP1 = shipsP1.Where(ship => ship.IsSunk).Select(ship => ship.Name + $"({ship.ShipSize})").ToList();
			List<string> sunkenShipsP2 = shipsP2.Where(ship => ship.IsSunk).Select(ship => ship.Name + $"({ship.ShipSize})").ToList();

			Display.PrintBattleShipMatchInfo(nameP1, sunkenShipsP1);
			Display.PrintBattleShipMatchInfo(nameP2, sunkenShipsP2);
			
		}

		private static void CalculateResults(Player playerOne, Player playerTwo, string playerEntry, bool playerOneTurn, MatchEvaluation matchInfoP1, MatchEvaluation matchInfoP2, List<Ship> shipsP1, List<Ship> shipsP2)
		{
			if (!shipsP2.Exists(ship => ship.IsSunk == false))
			{
				matchInfoP1.Victories++;
				matchInfoP2.Defeats++;
				Display.ShowWarning($"Jogador {playerOne.Name} venceu!");
			}
			else if (!shipsP1.Exists(ship => ship.IsSunk == false))
			{
				matchInfoP1.Defeats++;
				matchInfoP2.Victories++;
				Display.ShowWarning($"Jogador {playerTwo.Name} venceu!");
			}
			else if (playerEntry == "s")
			{
				matchInfoP1.Draws++;
				matchInfoP2.Draws++;
				Display.ShowWarning("Jogadores finalizaram a partida sem vencedores");
			}
			else
			{
				if (playerOneTurn)
				{
					matchInfoP1.Defeats++;
					matchInfoP2.Victories++;
					Display.ShowWarning($"Jogador {playerTwo.Name} venceu!");
				}
				else
				{
					matchInfoP1.Victories++;
					matchInfoP2.Defeats++;
					Display.ShowWarning($"Jogador {playerOne.Name} venceu!");
				}
			}
		}

		private static bool ValidatePlayerEntry(string playerEntry, List<BattleShipFieldInfo> occupiedFields)
		{
			Regex colRegex = new Regex(@"([A-Ja-j])");
			Regex rowRegex = new Regex(@"^([0-9]){1}$|^(10)$");

			try
			{
				string col = playerEntry[0].ToString();
				string row = playerEntry[1..];

				bool validCol = colRegex.IsMatch(col);
				bool validRow = rowRegex.IsMatch(row);

				if (playerEntry.ToLower() == "r" || playerEntry.ToLower() == "e")
					return false;

				if (validCol && validRow && !occupiedFields.Exists(field => field.Position == playerEntry && field.IsShot == true))
					return true;
				else
					return false;
			}
			catch (Exception)
			{
				return false;
			}

		}

		private static List<BattleShipFieldInfo> InitializeFields(List<Ship> ships)
		{
			List<BattleShipFieldInfo> occupiedFields = new();
			BattleShipFieldInfo newField = new();

			foreach (Ship ship in ships)
			{
				foreach (string position in ship.Position)
				{
					newField.IsShip = true;
					newField.IsShot = false;
					newField.Position = position;
					occupiedFields.Add(newField);
				}
			}
			return occupiedFields;
		}

		private static List<Ship> InitializeShips()
		{
			List<string> occupiedPositions = new List<string>();
			List<Ship> ships = new List<Ship>();

			Ship carrier = new Ship("Carrier", 5);
			carrier.SetPosition();

			occupiedPositions.AddRange(carrier.Position);

			Ship battleShip = new Ship("BattleShip", 4);
			battleShip.SetPosition(occupiedPositions);

			occupiedPositions.AddRange(battleShip.Position);

			Ship cruiser = new Ship("Cruiser", 3);
			cruiser.SetPosition(occupiedPositions);

			occupiedPositions.AddRange(cruiser.Position);

			Ship submarine = new Ship("Submarine", 3);
			submarine.SetPosition(occupiedPositions);

			occupiedPositions.AddRange(submarine.Position);

			Ship destroyer = new Ship("Destroyer", 2);
			destroyer.SetPosition(occupiedPositions);

			ships.Add(carrier);
			ships.Add(battleShip);
			ships.Add(cruiser);
			ships.Add(submarine);
			ships.Add(destroyer);

			return ships;		
		}
	}
}
