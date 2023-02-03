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

        private string[,] _grid;
        private bool[,] _ships;

        private int _turns = 0;
        private int _found = 0;
        private long _start;

        public static Random _random = new Random();

        public Game()
        {

            runStage(null);
        }

        public void runStage(String input)
        {
            if (_grid == null) makeGrid();
            print();

            Console.WriteLine("");
            if (input != null) Console.WriteLine(input);
            Console.WriteLine("===================================");
            Console.WriteLine("Turns taken: " + _turns);
            Console.WriteLine("Found: " + _found);
            Console.WriteLine("Time Taken: " + Utils.format(Environment.TickCount - _start));
            Console.WriteLine("===================================");
            Console.WriteLine("Please enter your guess in \"X Z\" format: for example 0 0");
            processInput(Console.ReadLine());
        }

        private void processInput(String s)
        {
            string[] args = s.Split(" ");
            if (args.Length == 1)
            {
                runStage("Invalid input.");
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
                runStage("Invalid input (" + e.Message + ")");
                return;
            }

            if (x >= SIZE || y >= SIZE || x < 0 || y < 0)
            {
                runStage("Out of bounds!");
                return;
            }

            if (!_grid[x, y].Equals(UNKNOWN))
            {
                runStage("You've already chose " + x + "," + y);
                return;
            }

            _turns++;

            if (_ships[x, y])
            {
                Console.ForegroundColor = ConsoleColor.Green;

                _found++;
                _grid[x, y] = BOOM;

                if (_found == SHIPS)
                {
                    Console.WriteLine("You found all the ships! W");
                    Console.WriteLine("Time: " + Utils.format(Environment.TickCount - _start));
                    return;
                }
                runStage("Boom! W");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;

            _grid[x, y] = WRONG;
            runStage("Common Battleship L");
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
            _start = Environment.TickCount;
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
           Console.Clear();

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
    }

    class Utils
    {
        public static string format(long milliseconds)
        {
            int time = (int) milliseconds / 1000;
            return string.Format("00:{0:D2}:{1:D2}", time / 60, time % 60);
        }
    }
}
