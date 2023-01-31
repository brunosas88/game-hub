# Game Hub

O Game Hub emula uma central de jogos, onde jogadores podem se cadastrar e ter acesso aos jogos implementados na plataforma (jogo-da-velha e xadrez no momento), e analisar os resultados das partidas bem como os rankings com as pontuações dos jogadores de cada jogo.

Tabela de conteúdos
=================
<!--ts-->
   * [Sobre](#game-hub)
   * [Instalação](#instalação)
   * [Configuração](#configuração)
   * [Como usar](#como-usar)
      * [Xadrez](#xadrez)
      * [Jogo-da-Velha](#jogo-da-velha)
      * [Batalha Naval](#batalha-naval)
   * [Tecnologias](#tecnologias)
<!--te-->

# Instalação
  * Instalar versão 6 do .NET;
  * Instalar editor de código (Opcional);
  * Clonar ou baixar código zipado deste repositório;
  * Dentro da pasta Game-Hub iniciar o terminal e digitar o comando ```dotnet run```;
  
# Configuração
  * Para correta visualização da interface do programa é necessário que o terminal seja configurado para **fonte Consolas** de **tamanho 20** e selecionar a opção **Fonte em Negrito**;
  * O tamanho da janela do console é padronizado e não deve ser alterado;
    * OBS.: o comando ``` Console.SetWindowSize(Constants.WindowWidthSize, Constants.WindowHeightSize); ``` localizado na linha 24 da classe Game.cs funciona somente no Windows, o que significa que para rodar em outro sistema operacional ela deve ser comentada. Lembrando também que não utilizar o sistema Windows acarreta em alguns caracteres não serem reconhecidos e exibidos de forma incorreta além de uma interface desalinhada.
  
# Como Usar
  * Ao iniciar o programa será oferecida as opções de cadastro de jogador, verificar histórico de partidas, iniciar um jogo e sair do programa;
    * Cadastro do Jogador: Cada jogador precisa se cadastrar com um nome de usuário e senha para ter acesso aos jogos;
    * Histórico: O usuário pode verificar o histórico de partidas jogadas de cada tipo de jogo e o ranking respectivo;
      * O ranking é contabilizado da seguinte maneira: 1 ponto para empate, 2 para vitória e -1 para derrota;
    * Iniciar Jogo: O usuário precisa inserir um nome e senha já cadastrados para ter acesso aos jogos, iniciando pela definição de jogador 1
    depois o jogador 2. Após isso decide-se o jogo a ser jogado, no caso, jogo-da-velha ou xadrez;
  * As partidas e jogadores cadastrados são salvos automaticamente ao final das partidas e ao final do cadastro respectivamente;
  * É permitido ao jogador se render durante seu turno dando a vitória ao jogador oposto;
  * É permitido ao jogador tentar declarar empate, mas somente é concedido caso o jogador oposto também aceite ao ser perguntado pelo programa;
  
## Xadrez
  * As peças de xadrez seguem o padrão brasileiro de notação: R = rei, D = dama, C = cavalo, T = torre e B = bispo com a adição da letra P para representar o peão;
  * As posições são inseridas informando coluna seguido da linha, ex.: a1, b2, d5, h8;
    * Primeiro se informa a posição da peça a ser movida e depois a posição para onde ela deve ir (indicada em destaque) respeitando os tipos de movimentações de cada peça; 
  * Ainda não estão implementadas as jogadas especiais *en passant* e roque;
  * Ainda não está implementada a opção de gerar o pgn da partida;
  * Condição de vitória é capturar o Rei do jogador adversário;
## Jogo-da-Velha
  * Jogadores são representados pelas letras X e O;
  * O jogador deve inserir uma posição de 1 a 9 que representa os espaços do tabuleiro, não é permitido inserir em um espaço já inserido e nem um valor fora
desse intervalo é aceito.
    *   ```
        ¹ | ² | ³
        ⁴ | ⁵ | ⁶
        ⁷ | ⁸ | ⁹
        ```
  * Condição de vitória é conseguir posicionar uma sequência de três simbolos iguais (X ou O);
  
## Batalha Naval
  * Cada jogador possui 5 navios distribuidos aleatoriamente em seu tabuleiro de 10x10, sendo estes definidos por um nome e pela quantidade de posições que ocupam respectivamente: Carrier (5), Battleship (4), Cruiser (3), Submarine (3), and Destroyer (2);
  * As posições são inseridas informando coluna seguido da linha, ex.: a1, b2, d5, h8;
  * O jogador, no seu turno, deve informar uma posição para atacar no tabuleiro inimigo (uma posição que pode ser atacada é simbolizada por um til "~"): caso um navio seja atingido um círculo preenchido (●) de cor vermelha é mostrado no tabuleiro, caso a posição não contenha parte de um navio é mostrado um círculo branco sem preenchimento (○);
  * Um navio só é dado como abatido se todas as posições que o contêm forem atacadas;
  * A condição de vitória é abater todos os navios do adversário;

# Tecnologias
  * Desenvolvido em C#, utilizando conceitos de classes, interface, herança, serialização(persistência de dados) e estruturação de projeto em MVC;
