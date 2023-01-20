using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Hub.Controller;
using Game_Hub.Model;
using Game_Hub.Model.Enums;
using Game_Hub.Model.TicTacToe;
using Game_Hub.Util;
using Game_Hub.View;

namespace Game_Hub
{
    class Game
	{

		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.Unicode;
			Console.BackgroundColor = ConsoleColor.DarkCyan;
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetWindowSize(Constants.WindowWidthSize, Constants.WindowHeightSize);
			//Console.CursorVisible = false;

			List<Player> players = new List<Player>();
			List<Match> matches = new List<Match>();
			List<string> mainMenuOptions = new List<string>
			{Constants.MainMenuEndGameOption, Constants.MainMenuFirstOption, Constants.MainMenuSecondOption,
			Constants.MainMenuThirdption};

			Utils.ReadJSON(ref players, ref matches);

			int option;

			do
			{					
				option = Display.ShowMenu(mainMenuOptions, "Menu Inicial");

				switch (option)
				{
					case 1:
						RegisterPlayer(players);
						break;
					case 2:
						ChooseGame(players, matches);					
						break;
					case 3:
						SelectGameOptions(players, matches);
						break;
					case 0:
						Display.GameInterface("Game Over!");
						Utils.WriteJSON(players, matches);
						break;		
				}
			} while (option != 0);
		}

		static void RegisterPlayer(List<Player> players)
		{
			string name, password, warning = "Jogador já Cadastrado!";
			bool isRegistered;
			Display.GameInterface("Cadastro de Novo Jogador");

			Console.WriteLine(Display.AlignMessage("Insira nome do novo jogador: "));
			name = Display.FormatConsoleReadLine();

			Console.WriteLine(Display.AlignMessage("Insira senha do novo jogador: "));
			password = Display.FormatConsoleReadLine(Constants.IS_ENCRYPTED);

			isRegistered = players.Exists(player => player.Nome == name);

			if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(name) && !isRegistered)
			{
				players.Add(new Player(name, password));
				warning = $"Jogador {name} Cadastrado com Sucesso!";
			}
			else if (string.IsNullOrEmpty(name))
				warning = "Entrada Inválida! Operação Não Realizada!";
			else
				warning += " Operação Não Realizada!";

			Display.ShowWarning(warning);

			Display.BackToMenu();
		}

		private static void ChooseGame(List<Player> players, List<Match> matches)
		{
			int option;

			do
			{
				option = GetGameTitle();
				if (option != 0)				
					GetLogs(players, matches, (GameTitle)option);
				
			} while (option != 0);
		}

		private static void GetLogs(List<Player> players, List<Match> matches, GameTitle game)
		{
			int option;
			List<string> menuOptions = new List<string>
			{Constants.MenuBackToOption, Constants.LogMenuFirstOption, Constants.LogMenuSecondOption};

			do
			{
				option = Display.ShowMenu(menuOptions, "Histórico");

				switch (option)
				{
					case 1:
						GetPlayersLogs(players, game);
						break;
					case 2:
						GetMatchesLogs(matches, game);						
						break;
					case 0:
						break;
				}
			} while (option != 0);
		}

		private static void GetMatchesLogs(List<Match> matches, GameTitle choosenGame)
		{
			Display.GameInterface("Histórico de Partidas");

			List<Match> choosenGameMatches = matches.FindAll(match => match.Game == choosenGame);

			foreach (Match match in choosenGameMatches)
				Display.ShowMatchesDetails(match);				

			Display.BackToMenu();
		}

		private static void GetPlayersLogs(List<Player> players, GameTitle game)
		{
			Display.GameInterface("Ranking");
			List<Player> ranking = players.OrderBy(player => player.MatchesInfo.Find(matches => matches.Game == game).Points ).ToList();
			ranking.Reverse();

			foreach (Player player in ranking)
				Display.ShowPlayerDetails(player, game);		
			
			Display.BackToMenu();
		}

		static Player GetPlayer(List<Player> players, int playerOrder)
		{
			string findPlayerAgain = "n", name, password;
			Player? player;

			do
			{
				Display.GameInterface("Selecionar Jogadores");

				Console.WriteLine(Display.AlignMessage($"Nome do Jogador {playerOrder}: "));
				name = Display.FormatConsoleReadLine();

				player = players.Find(player => player.Nome == name);

				if (player == null)
				{
					Display.ShowWarning("Jogador Não Encontrado!");
					Console.WriteLine(Display.AlignMessage("Selecionar Outro Jogador? S - sim / Qualquer outra tecla - não: "));
					findPlayerAgain = Display.FormatConsoleReadLine();
				}
				else
				{
					Console.WriteLine(Display.AlignMessage("Favor inserir a senha: "));
					password = Display.FormatConsoleReadLine(Constants.IS_ENCRYPTED);
					if (player.Password == password)					
						return player;
					else
					{
						Display.ShowWarning("Senha inválida");
						Console.WriteLine(Display.AlignMessage("Selecionar Outro Jogador? S - sim / Qualquer outra tecla - não: "));
						findPlayerAgain = Display.FormatConsoleReadLine();
					}					
				}
					

			} while (findPlayerAgain == "s" || findPlayerAgain == "S");

			return player;			
		}

		static bool SelectGameOptions(List<Player> players, List<Match> matches)
		{
			Player[] gamePlayers = SelectPlayers(players);

			if (gamePlayers.Contains(null))
				Display.ShowWarning("Jogador(es) Inválidos!");
			else
			{
				int option = GetGameTitle();

				switch (option)
				{
					case 1:
						break;
					case 2:
						InitializeTicTacToeGame(matches, gamePlayers);
						break;
					case 0:
						break;
				}
			}
			Display.BackToMenu();

			return true;
		}

		private static int GetGameTitle()
		{
			List<string> gameTitles = new List<string>
			{
				Constants.MenuBackToOption
			};

			gameTitles.AddRange(Enum.GetValues(typeof(GameTitle))
										  .Cast<GameTitle>()
										  .Select(title => title.ToString().Replace("_", " "))
										  .ToList());
			
			
			return Display.ShowMenu(gameTitles, "Escolha do Jogo");
		}

		private static void InitializeTicTacToeGame(List<Match> matches, Player[] gamePlayers)
		{
			string playAgain;

			CheckGameInfo(gamePlayers, GameTitle.JOGO_DA_VELHA);
			int playerOnePreMatchWins = gamePlayers[0].MatchesInfo.Find(match => match.Game == GameTitle.JOGO_DA_VELHA).Victories,
			    playerTwoPreMatchWins = gamePlayers[1].MatchesInfo.Find(match => match.Game == GameTitle.JOGO_DA_VELHA).Victories,
			    playerOnepreMatchDraws = gamePlayers[0].MatchesInfo.Find(match => match.Game == GameTitle.JOGO_DA_VELHA).Draws;

			do
			{
				Display.GameInterface("Jogar!");
				gamePlayers[0].PlayOrder = 1;
				gamePlayers[1].PlayOrder = 2;

				TicTacToeGame.PlayTicTacToeGame(gamePlayers[0], gamePlayers[1]);

				Console.WriteLine(Display.AlignMessage("Continuar Jogando? S - sim / Qualquer outra tecla - não: "));
				playAgain = Display.FormatConsoleReadLine();

				if (playAgain == "s" || playAgain == "S")
					Array.Reverse(gamePlayers);

			} while (playAgain == "s" || playAgain == "S");

			CalculateMatchResults(GameTitle.JOGO_DA_VELHA, gamePlayers, matches, playerOnePreMatchWins, playerTwoPreMatchWins, playerOnepreMatchDraws);
		}

		private static void CheckGameInfo(Player[] gamePlayers, GameTitle game)
		{
			foreach (Player player in gamePlayers)
			{
				if (!player.MatchesInfo.Exists(match => match.Game == game))				
					player.MatchesInfo.Add(new MatchEvaluation(game));				
			}
		}

		private static Player[] SelectPlayers(List<Player> players)
		{
			Display.GameInterface("Configurações Iniciais do Jogo");

			Player?[] gamePlayers = new Player[2];

			gamePlayers[0] = GetPlayer(players, 1);

			gamePlayers[1] = GetPlayer(players, 2);

			return gamePlayers;
		}

		private static void CalculateMatchResults(GameTitle game, Player[] gamePlayers, List<Match> matches, int playerOnePreMatchWins, int playerTwoPreMatchWins, int playerOnepreMatchDraws)
		{

			Match currentMatch = new Match(game, gamePlayers[0].Nome, gamePlayers[1].Nome);
			MatchEvaluation matchInfoP1 = gamePlayers[0].MatchesInfo.FirstOrDefault(match => match.Game == game),
							matchInfoP2 = gamePlayers[1].MatchesInfo.FirstOrDefault(match => match.Game == game);

			currentMatch.PlayerOneVictories = matchInfoP1.Victories - playerOnePreMatchWins;
			currentMatch.PlayerTwoVictories = matchInfoP2.Victories - playerTwoPreMatchWins;
			currentMatch.Draws = matchInfoP1.Draws - playerOnepreMatchDraws;
			
			matches.Add(currentMatch);

			matchInfoP1.Points += currentMatch.PlayerOneVictories * Constants.VictoryPoints +
								  currentMatch.PlayerTwoVictories * Constants.DefeatPoints +
								  currentMatch.Draws * Constants.DrawPoints;

			matchInfoP2.Points += currentMatch.PlayerTwoVictories * Constants.VictoryPoints +
								  currentMatch.PlayerOneVictories * Constants.DefeatPoints +
								  currentMatch.Draws * Constants.DrawPoints;
		}
	}
}
