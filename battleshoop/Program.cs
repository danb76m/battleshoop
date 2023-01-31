using System;

namespace battleshoop
{
    class Program
    {
        static void Main(string[] args)
        {
            new Game();
        }
    }

    class Game
    {
        public static int SIZE = 9;
        public static int SHIPS = 4;

        public static string UNKNOWN = "/",
            WRONG = "X",
            BOOM = "A";

        private int _player;
        private string[,] _grid;
        private bool[,] _ships;

        public static Random _random = new Random();

        public Game()
        {

            runStage();
        }

        public void runStage()
        {
            if (_grid == null) makeGrid();
            print();
            Console.WriteLine("Please enter your guess in \"X Z\" format: for example 0 0");
            processInput(Console.ReadLine());
        }

        private void processInput(String s)
        {
            string[] args = s.Split(" ");
            if (args.Length == 1)
            {
                Console.WriteLine("Invalid input.");
                runStage();
                return;
            }

            int x, y;

            try
            {
                x = int.Parse(args[0]);
                y = int.Parse(args[1]);
            }
            catch(FormatException e)
            {
                Console.WriteLine("Invalid input (" + e.Message + ")");
                runStage();
                return;
            }

            if (x >= SIZE || y >= SIZE || x < 0 || y < 0)
            {
                Console.WriteLine("Out of bounds!");
                runStage();
                return;
            }

            if (!_grid[x, y].Equals(UNKNOWN))
            {
                Console.WriteLine("You've already chose " + x + "," + y);
                runStage();
                return;
            }

            if (_ships[x, y])
            {
                _grid[x, y] = BOOM;
                Console.WriteLine("Boom! W");
                runStage();
                return;
            }

            _grid[x, y] = WRONG;
            Console.WriteLine("Common Battleship L");
            runStage();
            return;
        }

        private void makeGrid()
        {
            _grid = new string[SIZE, SIZE];
            _ships = new bool[SIZE, SIZE];

            for (int x = 0; x < SIZE; x++)
            {
                for (int z = 0; z < SIZE; z++)
                {
                    _grid[x, z] = UNKNOWN;

                }
            }

            for (int x = 0; x < SHIPS; x++) make();
        }

        private void make()
        {
            int x = _random.Next(SIZE);
            int y = _random.Next(SIZE);

            if (_ships[x, y]) make();
            _ships[x, y] = true;
        }

        private void print()
        {
           // Console.Clear();

            printTopColumn();
            for (int y = 0; y < SIZE; y++)
            {
                printColumn(y);         }
        }

        private void printTopColumn()
        {
            Console.Write("  ");
            for (int x = 0; x < SIZE; x++)
            {
                Console.Write(x + " ");
            }
            Console.Write("\n");
        }

        private void printColumn(int y)
        {
            Console.Write(y + " ");
            for (int x = 0; x < SIZE; x++)
            {
                Console.Write(_grid[x, y] + " ");
            }
            Console.Write("\n");
        }
        public int getTurn()
        {
            if (_player++ == 2) _player = 0;
            return _player;
        }
    }
}
