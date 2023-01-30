using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Hub.Controller;
using Game_Hub.Model;
using Game_Hub.Model.BattleShip;
using Game_Hub.Model.Enums;
using Game_Hub.Model.TicTacToe;
using Game_Hub.Repository;
using Game_Hub.Utils;
using Game_Hub.View;

namespace Game_Hub
{
    class Game
	{
		private static PlayerRepository playerRepository = new PlayerRepository();
		private static MatchRepository matchRepository = new MatchRepository();
		static void Main(string[] args)
		{		
			Console.CursorVisible = false;
			Console.SetWindowSize(Constants.WINDOW_WIDTH_SIZE, Constants.WINDOW_HEIGHT_SIZE);
			Console.OutputEncoding = Encoding.Unicode;

			//Display.ShowLogo();

			Console.BackgroundColor = Constants.MAIN_BACKGROUND_COLOR;
			Console.ForegroundColor = Constants.MAIN_FOREGROUND_COLOR;
			Console.CursorVisible = true;

			List<string> mainMenuOptions = new List<string>
			{Constants.MAIN_MENU_END_GAME_OPTION, Constants.MAIN_MENU_FIRST_OPTION, Constants.MAIN_MENU_SECOND_OPTION,
			Constants.MAIN_MENU_THIRD_OPTION};

			int option;

			do
			{				
				option = Display.ShowMenu(mainMenuOptions, "Menu Inicial");

				switch (option)
				{
					case 1:
						RegisterPlayer();											
						break;
					case 2:
						ChooseLogGame();					
						break;
					case 3:
						SelectGameOptions();						
						break;
					case 0:
						Display.GameInterface("Game Over!");						
						break;		
				}
			} while (option != 0);
		}

		static void RegisterPlayer()
		{
			string name, password, warning = "Jogador já Cadastrado!";
			bool isRegistered;
			List<Player> players = playerRepository.Read();

			Display.GameInterface("Cadastro de Novo Jogador");

			Console.WriteLine(Display.AlignMessage($"Insira nome do novo jogador (máx. de {Constants.MAX_CHARACTER_ALLOWED} caracteres):"));
			name = Display.FormatConsoleReadLine();

			Console.WriteLine(Display.AlignMessage($"Insira senha do novo jogador (máx. de {Constants.MAX_CHARACTER_ALLOWED} caracteres):"));
			password = Display.FormatConsoleReadLine(Constants.IS_ENCRYPTED);

			isRegistered = players.Exists(player => player.Name == name);

			if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password) && !isRegistered)
			{
				playerRepository.Save(new Player(name, password));
				
				warning = $"Jogador {name} Cadastrado com Sucesso!";
			}
			else if (string.IsNullOrEmpty(name))
				warning = "Entrada Inválida! Operação Não Realizada!";
			else
				warning += " Operação Não Realizada!";

			Display.ShowWarning(warning);

			Display.BackToMenu();

		}

		private static void ChooseLogGame()
		{
			int option;

			do
			{
				option = GetGameTitle();
				if (option != 0)				
					GetLogs((GameTitle)option);
				
			} while (option != 0);
		}

		private static void GetLogs(GameTitle game)
		{
			int option;
			List<string> menuOptions = new List<string>
			{Constants.MENU_BACK_TO_OPTION, Constants.LOG_MENU_FIRST_OPTION, Constants.LOG_MENU_SECOND_OPTION};

			string gameName = Util.CaptalizeString(game.ToString().Replace("_", " "));

			do
			{
				option = Display.ShowMenu(menuOptions, $"Histórico de {gameName}");

				switch (option)
				{
					case 1:
						GetPlayersLogs(game);
						break;
					case 2:
						GetMatchesLogs(game);						
						break;
					case 0:
						break;
				}
			} while (option != 0);
		}

		private static void GetMatchesLogs(GameTitle game)
		{
			string gameName = Util.CaptalizeString(game.ToString().Replace("_", " "));
			Display.GameInterface($"Histórico de Partidas de {gameName}");

			List<Match> matches = matchRepository.Read();

			List<Match> choosenGameMatches = matches.FindAll(match => match.Game == game);

			foreach (Match match in choosenGameMatches)
				Display.ShowMatchesDetails(match);				

			Display.BackToMenu();
		}

		private static void GetPlayersLogs(GameTitle game)
		{
			string gameName = Util.CaptalizeString(game.ToString().Replace("_", " "));
			Display.GameInterface($"Ranking de {gameName}");

			List<Player> players = playerRepository.Read();
			List<Player> ranking = players.OrderBy(player => player.MatchesInfo.Find(matches => matches.Game == game).Points).ToList();
			ranking.Reverse();

			foreach (Player player in ranking)
				Display.ShowPlayerDetails(player, game);		
			
			Display.BackToMenu();
		}

		static Player GetPlayer(int playerOrder)
		{
			string findPlayerAgain = "n", name, password;
			List<Player> players = playerRepository.Read();
			Player? player;

			do
			{
				Display.GameInterface("Selecionar Jogadores");

				Console.WriteLine(Display.AlignMessage($"Nome do Jogador {playerOrder}: "));
				name = Display.FormatConsoleReadLine();

				player = players.Find(player => player.Name == name);

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
			} while (findPlayerAgain.ToLower() == "s" );

			return player;			
		}

		static bool SelectGameOptions()
		{
			Player[] gamePlayers = SelectPlayers();

			if (gamePlayers.Contains(null))
				Display.ShowWarning("Jogador(es) Inválidos!");
			else
			{
				int option = GetGameTitle();

				InitializeGame(gamePlayers, (GameTitle)option);
			}
			Display.BackToMenu();

			return true;
		}

		private static int GetGameTitle()
		{
			List<string> gameTitles = new List<string>
			{
				Constants.MENU_BACK_TO_OPTION
			};

			gameTitles.AddRange(Enum.GetValues(typeof(GameTitle))
										  .Cast<GameTitle>()
										  .Select(title => Util.CaptalizeString(title.ToString().Replace("_", " ")))
										  .ToList());
			
			
			return Display.ShowMenu(gameTitles, "Escolha do Jogo");
		}

		private static void InitializeGame(Player[] gamePlayers, GameTitle game)
		{
			string playAgain;
		
			int playerOnePreMatchWins = gamePlayers[0].MatchesInfo.Find(match => match.Game == game).Victories,
			    playerTwoPreMatchWins = gamePlayers[1].MatchesInfo.Find(match => match.Game == game).Victories,
			    playerOnepreMatchDraws = gamePlayers[0].MatchesInfo.Find(match => match.Game == game).Draws;

			do
			{
				Display.GameInterface("Jogar!");
				gamePlayers[0].PlayOrder = 1;
				gamePlayers[1].PlayOrder = 2;

				switch (game)
				{
					case GameTitle.XADREZ:
						ChessGame.PlayChessGame(gamePlayers[0], gamePlayers[1]);
						break;
					case GameTitle.JOGO_DA_VELHA:
						TicTacToeGame.PlayTicTacToeGame(gamePlayers[0], gamePlayers[1]);
						break;
					case GameTitle.BATALHA_NAVAL:
						BattleShipGame.PlayBattleShipGame(gamePlayers[0], gamePlayers[1]);
						break;
				}				

				Console.WriteLine(Display.AlignMessage("Continuar Jogando? S - sim / Qualquer outra tecla - não: "));
				playAgain = Display.FormatConsoleReadLine();

				if (playAgain.ToLower() == "s")
					Array.Reverse(gamePlayers);

			} while (playAgain.ToLower() == "s");

			CalculateMatchResults(game, gamePlayers, playerOnePreMatchWins, playerTwoPreMatchWins, playerOnepreMatchDraws);
		}

		private static Player[] SelectPlayers()
		{
			Display.GameInterface("Configurações Iniciais do Jogo");

			Player?[] gamePlayers = new Player[2];

			gamePlayers[0] = GetPlayer(1);

			gamePlayers[1] = GetPlayer(2);

			return gamePlayers;
		}

		private static void CalculateMatchResults(GameTitle game, Player[] gamePlayers, int playerOnePreMatchWins, int playerTwoPreMatchWins, int playerOnepreMatchDraws)
		{
			Match currentMatch = new Match(game, gamePlayers[0].Name, gamePlayers[1].Name);
			MatchEvaluation matchInfoP1 = gamePlayers[0].MatchesInfo.FirstOrDefault(match => match.Game == game),
							matchInfoP2 = gamePlayers[1].MatchesInfo.FirstOrDefault(match => match.Game == game);

			currentMatch.PlayerOneVictories = matchInfoP1.Victories - playerOnePreMatchWins;
			currentMatch.PlayerTwoVictories = matchInfoP2.Victories - playerTwoPreMatchWins;
			currentMatch.Draws = matchInfoP1.Draws - playerOnepreMatchDraws;		
			
			matchInfoP1.Points += currentMatch.PlayerOneVictories * Constants.VICTORY_POINTS +
								  currentMatch.PlayerTwoVictories * Constants.DEFEAT_POINTS +
								  currentMatch.Draws * Constants.DRAW_POINTS;

			matchInfoP2.Points += currentMatch.PlayerTwoVictories * Constants.VICTORY_POINTS +
								  currentMatch.PlayerOneVictories * Constants.DEFEAT_POINTS +
								  currentMatch.Draws * Constants.DRAW_POINTS;

			playerRepository.Update(gamePlayers[0]);
			playerRepository.Update(gamePlayers[1]);
			matchRepository.Save(currentMatch);
		}
	}
}
