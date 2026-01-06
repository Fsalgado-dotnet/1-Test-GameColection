using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCollection
{
    class Program
    {
        static int playerXWins = 0;
        static int playerOWins = 0;
        static char[,] board = new char[16, 16];
        static Player human = new Player('X');
        static Player computer = new Player('O', isComputer: true);
        static Random random = new Random();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Game Collection!");
                Console.WriteLine("1. Tic-Tac-Toe");
                Console.WriteLine("2. Naval Battle (Battleship)");
                Console.WriteLine("3. Adventure Game");
                Console.WriteLine("4. Four in a Row (vs Computer)");
                Console.WriteLine("0. Exit");
                Console.Write("Choose a game: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        for (int i = 0; i < 3; i++) PlayTicTacToe();
                        break;
                    case "2":
                        PlayNavalBattle();
                        break;
                    case "3":
                        PlayAdventureGame();
                        break;
                    case "4":
                        PlayFourInARow();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // ========================= TIC-TAC-TOE =========================
        static void PlayTicTacToe()
        {
            char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char currentPlayer = 'X';
            int turns = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Tic-Tac-Toe");
                Console.WriteLine($"{board[0]} | {board[1]} | {board[2]}");
                Console.WriteLine("--+---+--");
                Console.WriteLine($"{board[3]} | {board[4]} | {board[5]}");
                Console.WriteLine("--+---+--");
                Console.WriteLine($"{board[6]} | {board[7]} | {board[8]}");
                Console.Write($"Player {currentPlayer}, choose your position (1-9): ");

                if (int.TryParse(Console.ReadLine(), out int pos) && pos >= 1 && pos <= 9 && board[pos - 1] != 'X' && board[pos - 1] != 'O')
                {
                    board[pos - 1] = currentPlayer;
                    turns++;

                    if (CheckWin(board, currentPlayer))
                    {
                        Console.WriteLine($"Player {currentPlayer} wins!");
                        if (currentPlayer == 'X') playerXWins++; else playerOWins++;
                        break;
                    }

                    if (turns == 9)
                    {
                        Console.WriteLine("It's a draw!");
                        break;
                    }

                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }

            Console.WriteLine($"Score - X: {playerXWins}, O: {playerOWins}");
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }

        static bool CheckWin(char[] b, char p)
        {
            return (b[0] == p && b[1] == p && b[2] == p) || (b[3] == p && b[4] == p && b[5] == p) || (b[6] == p && b[7] == p && b[8] == p) ||
                   (b[0] == p && b[3] == p && b[6] == p) || (b[1] == p && b[4] == p && b[7] == p) || (b[2] == p && b[5] == p && b[8] == p) ||
                   (b[0] == p && b[4] == p && b[8] == p) || (b[2] == p && b[4] == p && b[6] == p);
        }

        // ========================= NAVAL BATTLE =========================
        static void PlayNavalBattle()
        {
            char[,] board = new char[5, 5];
            Random rand = new Random();
            int shipRow = rand.Next(5), shipCol = rand.Next(5), attempts = 0;
            bool hit = false;

            for (int i = 0; i < 5; i++) for (int j = 0; j < 5; j++) board[i, j] = '~';

            while (attempts < 5 && !hit)
            {
                Console.Clear();
                Console.WriteLine("Naval Battle (Battleship)");

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++) Console.Write(board[i, j] + " ");
                    Console.WriteLine();
                }

                Console.Write("Enter guess (row col): ");
                string[] input = Console.ReadLine().Split();
                if (input.Length == 2 && int.TryParse(input[0], out int r) && int.TryParse(input[1], out int c) && r >= 1 && r <= 5 && c >= 1 && c <= 5)
                {
                    r--; c--;
                    if (board[r, c] == 'X' || board[r, c] == 'O') Console.WriteLine("Already guessed!");
                    else
                    {
                        attempts++;
                        board[r, c] = (r == shipRow && c == shipCol) ? 'X' : 'O';
                        if (board[r, c] == 'X') { Console.WriteLine("Hit!"); hit = true; }
                        else Console.WriteLine("Miss!");
                    }
                }
                else Console.WriteLine("Invalid input.");
                Console.ReadKey();
            }

            if (!hit) Console.WriteLine($"Out of attempts! Ship was at ({shipRow + 1}, {shipCol + 1})");
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }

        // ========================= ADVENTURE GAME =========================
        static void PlayAdventureGame()
        {
            Console.Clear();
            Console.WriteLine("You're in a dark forest. Go left or right?");
            Console.WriteLine("1. Go Left");
            Console.WriteLine("2. Go Right");
            string choice = Console.ReadLine();

            if (choice == "1") GoLeft();
            else if (choice == "2") GoRight();
            else Console.WriteLine("You stand still...");
        }

        static void GoLeft()
        {
            Console.Clear();
            Console.WriteLine("You meet a friendly elf with a sword.");
            Console.WriteLine("1. Thank him");
            Console.WriteLine("2. Ask for more help");
            string choice = Console.ReadLine();

            if (choice == "1") Console.WriteLine("You gain the sword and move on!");
            else if (choice == "2") { Console.WriteLine("He reveals treasure nearby."); SearchForTreasure(); }
            else Console.WriteLine("The elf disappears.");
        }

        static void SearchForTreasure()
        {
            Console.WriteLine("You find a cave.");
            Console.WriteLine("1. Enter");
            Console.WriteLine("2. Leave");
            string choice = Console.ReadLine();

            if (choice == "1") Console.WriteLine("Gold chest found!");
            else if (choice == "2") Console.WriteLine("You keep exploring.");
            else Console.WriteLine("You hesitate at the entrance.");
        }

        static void GoRight()
        {
            Console.Clear();
            Console.WriteLine("You see a dragon!");
            Console.WriteLine("1. Fight");
            Console.WriteLine("2. Run");
            string choice = Console.ReadLine();

            if (choice == "1") Console.WriteLine("You scare the dragon away!");
            else if (choice == "2") ExploreCave();
            else Console.WriteLine("The dragon flies off.");
        }

        static void ExploreCave()
        {
            Console.WriteLine("You find two paths in a cave.");
            Console.WriteLine("1. Left path");
            Console.WriteLine("2. Right path");
            string choice = Console.ReadLine();

            if (choice == "1") Console.WriteLine("You see a sleeping bear.");
            else if (choice == "2") Console.WriteLine("You find a magical amulet!");
            else Console.WriteLine("You freeze in place.");
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }

        // ========================= FOUR IN A ROW =========================
        static void PlayFourInARow()
        {
            InitializeBoard();
            bool gameOver = false;
            Player current = human;

            while (!gameOver)
            {
                DisplayBoard();
                int col = current.IsComputer ? GetSmartComputerMove() : GetColumnInput();
                if (MakeMove(current, col, out int row))
                {
                    if (CheckVictory(current, row, col))
                    {
                        DisplayBoard();
                        Console.WriteLine($"{current.Symbol} wins!");
                        gameOver = true;
                    }
                    else if (IsBoardFull())
                    {
                        DisplayBoard();
                        Console.WriteLine("It's a draw!");
                        gameOver = true;
                    }
                    else current = (current == human) ? computer : human;
                }
                else if (!current.IsComputer)
                    Console.WriteLine("Invalid move. Try again.");
            }

            Console.Write("Play again? (y/n): ");
            if (Console.ReadLine().Trim().ToLower() == "y")
                PlayFourInARow();
        }

        static void InitializeBoard()
        {
            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 16; j++)
                    board[i, j] = '.';
        }

        static void DisplayBoard()
        {
            Console.Clear();
            Console.WriteLine("   " + string.Join(" ", Enumerable.Range(1, 16).Select(x => x.ToString().PadLeft(2))));
            for (int i = 0; i < 16; i++)
            {
                Console.Write((i + 1).ToString().PadLeft(2) + " ");
                for (int j = 0; j < 16; j++) Console.Write(board[i, j] + " ");
                Console.WriteLine();
            }
        }

        static int GetColumnInput()
        {
            int col;
            while (!int.TryParse(Console.ReadLine(), out col) || col < 1 || col > 16 || !IsColumnPlayable(col - 1))
                Console.Write("Choose a valid column (1-16): ");
            return col - 1;
        }

        static bool IsColumnPlayable(int col) => board[0, col] == '.';

        static bool MakeMove(Player p, int col, out int row)
        {
            for (int i = 15; i >= 0; i--)
            {
                if (board[i, col] == '.')
                {
                    board[i, col] = p.Symbol;
                    row = i;
                    return true;
                }
            }
            row = -1;
            return false;
        }

        static bool CheckVictory(Player p, int row, int col)
        {
            return Count(p.Symbol, row, col, 1, 0) + Count(p.Symbol, row, col, -1, 0) >= 3 ||
                   Count(p.Symbol, row, col, 0, 1) + Count(p.Symbol, row, col, 0, -1) >= 3 ||
                   Count(p.Symbol, row, col, 1, 1) + Count(p.Symbol, row, col, -1, -1) >= 3 ||
                   Count(p.Symbol, row, col, 1, -1) + Count(p.Symbol, row, col, -1, 1) >= 3;
        }

        static int Count(char s, int r, int c, int dr, int dc)
        {
            int cnt = 0;
            r += dr; c += dc;
            while (r >= 0 && r < 16 && c >= 0 && c < 16 && board[r, c] == s) { cnt++; r += dr; c += dc; }
            return cnt;
        }

        static bool IsBoardFull() => Enumerable.Range(0, 16).All(c => board[0, c] != '.');

        static int GetSmartComputerMove()
        {
            for (int c = 0; c < 16; c++) if (TrySimulateMove(computer, c, out int r) && CheckVictory(computer, r, c)) return c;
            for (int c = 0; c < 16; c++) if (TrySimulateMove(human, c, out int r) && CheckVictory(human, r, c)) return c;
            foreach (int c in GetColumnsNearComputerPieces()) if (IsColumnPlayable(c)) return c;
            int col; do { col = random.Next(16); } while (!IsColumnPlayable(col)); return col;
        }

        static bool TrySimulateMove(Player p, int col, out int row)
        {
            row = -1;
            if (!IsColumnPlayable(col)) return false;
            for (int i = 15; i >= 0; i--)
            {
                if (board[i, col] == '.')
                {
                    board[i, col] = p.Symbol;
                    bool win = CheckVictory(p, i, col);
                    board[i, col] = '.';
                    row = i;
                    return win;
                }
            }
            return false;
        }

        static List<int> GetColumnsNearComputerPieces()
        {
            var result = new HashSet<int>();
            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 16; j++)
                    if (board[i, j] == computer.Symbol)
                        foreach (int offset in new[] { -1, 1 })
                            if (j + offset >= 0 && j + offset < 16 && IsColumnPlayable(j + offset))
                                result.Add(j + offset);
            return result.OrderBy(x => random.Next()).ToList();
        }

        public class Player
        {
            public char Symbol { get; }
            public bool IsComputer { get; }
            public Player(char symbol, bool isComputer = false) { Symbol = symbol; IsComputer = isComputer; }
        }
    }
}





/*
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Game game = new Game();
        game.Start();
    }
}

class Game
{
    private Player human;
    private Player computer;
    private bool gameOver;

    public Game()
    {
        human = new Player("Human");
        computer = new Player("Computer");
        gameOver = false;
    }

    public void Start()
    {
        Console.WriteLine("Welcome to 'You Sunk My Battleship'!");

        PlaceShips(human, "Your");
        PlaceShips(computer, "Computer's");

        while (!gameOver)
        {
            // Human turn
            Console.Clear();
            DisplayBoards();
            PlayerTurn(human);
            if (computer.FleetIsSunk())
            {
                Console.WriteLine("You have sunk all of the computer's ships. You win!");
                break;
            }

            // Computer turn
            PlayerTurn(computer);
            if (human.FleetIsSunk())
            {
                Console.WriteLine("The computer has sunk all of your ships. Game over!");
                break;
            }
        }
    }

    private void PlaceShips(Player player, string playerName)
    {
        Console.WriteLine($"{playerName} turn to place ships.");
        player.PlaceShip(5, "Carrier");
        player.PlaceShip(4, "Battleship");
        player.PlaceShip(3, "Destroyer");
        player.PlaceShip(3, "Submarine");
        player.PlaceShip(2, "Patrol Boat");
    }

    private void DisplayBoards()
    {
        Console.WriteLine("Human's Board:");
        human.DisplayBoard();
        Console.WriteLine("\nComputer's Board:");
        computer.DisplayBoard(false);
    }

    private void PlayerTurn(Player player)
    {
        if (player.Name == "Human")
        {
            Console.WriteLine("\nYour turn!");
            Console.WriteLine("Enter a target (e.g., A5): ");
            string target = Console.ReadLine().ToUpper();
            while (!player.TakeTurn(target, computer))
            {
                Console.WriteLine("Invalid target. Try again: ");
                target = Console.ReadLine().ToUpper();
            }
        }
        else
        {
            Console.WriteLine("\nComputer's turn!");
            string target = computer.TakeRandomTurn();
            Console.WriteLine($"Computer targets: {target}");
            computer.TakeTurn(target, human);
        }
    }
}

class Player
{
    public string Name { get; private set; }
    private char[,] board;
    private char[,] visibleBoard;
    private List<Ship> ships;

    public Player(string name)
    {
        Name = name;
        board = new char[10, 10];
        visibleBoard = new char[10, 10];
        ships = new List<Ship>();
        InitializeBoard(board);
        InitializeBoard(visibleBoard);
    }

    public void DisplayBoard(bool showShips = true)
    {
        Console.WriteLine("  A B C D E F G H I J");
        for (int row = 0; row < 10; row++)
        {
            Console.Write((row + 1) + " ");
            for (int col = 0; col < 10; col++)
            {
                char displayChar = visibleBoard[row, col];
                if (showShips && board[row, col] != '\0')
                {
                    displayChar = board[row, col]; // show the ships if needed
                }
                Console.Write(displayChar + " ");
            }
            Console.WriteLine();
        }
    }

    public void PlaceShip(int size, string shipType)
    {
        Console.WriteLine($"Place your {shipType} (Size: {size})");
        Console.WriteLine("Enter starting position (e.g., A1): ");
        string startPosition = Console.ReadLine().ToUpper();
        Console.WriteLine("Enter orientation (H for horizontal, V for vertical): ");
        string orientation = Console.ReadLine().ToUpper();

        while (!PlaceShipOnBoard(startPosition, orientation, size))
        {
            Console.WriteLine("Invalid placement. Try again.");
            Console.WriteLine("Enter starting position (e.g., A1): ");
            startPosition = Console.ReadLine().ToUpper();
            Console.WriteLine("Enter orientation (H for horizontal, V for vertical): ");
            orientation = Console.ReadLine().ToUpper();
        }

        Ship ship = new Ship(size, shipType, startPosition, orientation);
        ships.Add(ship);
    }

    public bool TakeTurn(string target, Player opponent)
    {
        int row = target[1] - '1';
        int col = target[0] - 'A';

        if (row < 0 || row >= 10 || col < 0 || col >= 10)
        {
            return false;
        }

        if (opponent.board[row, col] != '\0')
        {
            opponent.board[row, col] = 'X'; // Hit!
            visibleBoard[row, col] = 'X';
            Console.WriteLine("Hit!");
            return true;
        }
        else
        {
            visibleBoard[row, col] = 'O'; // Miss!
            Console.WriteLine("Miss!");
            return true;
        }
    }

    public string TakeRandomTurn()
    {
        Random rand = new Random();
        int row = rand.Next(0, 10);
        int col = rand.Next(0, 10);

        while (visibleBoard[row, col] == 'X' || visibleBoard[row, col] == 'O')
        {
            row = rand.Next(0, 10);
            col = rand.Next(0, 10);
        }

        return $"{(char)('A' + col)}{row + 1}";
    }

    public bool FleetIsSunk()
    {
        return ships.All(ship => ship.IsSunk());
    }

    private bool PlaceShipOnBoard(string startPosition, string orientation, int size)
    {
        int row = startPosition[1] - '1';
        int col = startPosition[0] - 'A';

        if (row < 0 || row >= 10 || col < 0 || col >= 10)
        {
            return false;
        }

        if (orientation == "H")
        {
            if (col + size > 10) return false;

            for (int i = 0; i < size; i++)
            {
                if (board[row, col + i] != '\0') return false;
            }

            for (int i = 0; i < size; i++)
            {
                board[row, col + i] = 'X'; // Mark the ship on the board
            }
        }
        else if (orientation == "V")
        {
            if (row + size > 10) return false;

            for (int i = 0; i < size; i++)
            {
                if (board[row + i, col] != '\0') return false;
            }

            for (int i = 0; i < size; i++)
            {
                board[row + i, col] = 'X'; // Mark the ship on the board
            }
        }

        return true;
    }

    private void InitializeBoard(char[,] board)
    {
        for (int row = 0; row < 10; row++)
        {
            for (int col = 0; col < 10; col++)
            {
                board[row, col] = '~'; // Water
            }
        }
    }
}

class Ship
{
    public int Size { get; }
    public string Type { get; }
    public List<string> Coordinates { get; }

    public Ship(int size, string type, string startPosition, string orientation)
    {
        Size = size;
        Type = type;
        Coordinates = new List<string>();
        int row = startPosition[1] - '1';
        int col = startPosition[0] - 'A';

        if (orientation == "H")
        {
            for (int i = 0; i < size; i++)
            {
                Coordinates.Add($"{(char)('A' + col + i)}{row + 1}");
            }
        }
        else if (orientation == "V")
        {
            for (int i = 0; i < size; i++)
            {
                Coordinates.Add($"{(char)('A' + col)}{row + i + 1}");
            }
        }
    }

    public bool IsSunk()
    {
        return Coordinates.All(coord => coord == "X");
    }
}
