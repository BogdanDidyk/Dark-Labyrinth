namespace Dark_Labyrinth
{
	class DarkLabyrinth
    {
		private Random rnd = new Random();

        public Map Map { get; private set; }
        public uint VisibilityRadius { get; private set; }
        public ConsoleColor DimmingColor { get; private set; }
        public ConsoleColor LightingColor { get; private set; }
        public uint Scores { get; private set; }
		
	    public DarkLabyrinth(Map map, ConsoleColor dimmingColor = ConsoleColor.Black, ConsoleColor lightingColor = ConsoleColor.DarkGray, uint visibilityRadius = 1, uint prizesCount = 1)
        {
            Map = map;
            DimmingColor = dimmingColor;
            LightingColor = lightingColor;
            VisibilityRadius = visibilityRadius;
			
			CreateHero();
            CreatePrizes(prizesCount);
        }
		
		private void CreateHero()
        {
            Map.Hero = new Hero('↑', ConsoleColor.White);
            Map.HeroPosition = Map.GetRandomFreePosition();
        }
		
		private void CreatePrizes(uint count = 3)
        {
            if (count <= 0) count = 1;

            for (int i = 0; i < count; i++)
            {
                if (Map.FreePositionsCount > 0)
                {
                    PrizeType prizeType = GetRandomPrizeType();
                    ConsoleColor color = GetPrizeColor(prizeType);
                    Position position = Map.GetRandomFreePosition();
                    Prize prize = new Prize('●', color, prizeType);

                    Map.AddPrizeAtPosition(position, prize);
                }
            }
        }
		
		private ConsoleColor GetPrizeColor(PrizeType prizeType)
        {
            ConsoleColor color = ConsoleColor.Gray;

            switch (prizeType)
            {
                case PrizeType.Silver:
                    color = ConsoleColor.Gray;
                    break;
                case PrizeType.Gold:
                    color = ConsoleColor.DarkYellow;
                    break;
                case PrizeType.Diamond:
                    color = ConsoleColor.Blue;
                    break;
                default:
                    color = ConsoleColor.White;
                    break;
            }

            return color;
        }
		
		private int getRandomIndex(double[] probabilities)
        {
            double sum = probabilities.Sum();
            int len = probabilities.Length;
            double randomValue = rnd.NextDouble();

            double probability = 0.0;
            int ind = 0;

            while (ind < len && randomValue >= (probability += probabilities[ind] / sum)) ind++;

            return ind;
        }
		
		private PrizeType GetRandomPrizeType()
        {
            // Probability for Silver, Gold and Diamond prizes
            double[] probabilities = new double[] { 0.5, 0.375, 0.125 };
            string[] prizeNames = Enum.GetNames(typeof(PrizeType));

            int randomIndex = getRandomIndex(probabilities);
            string randomPrize = prizeNames[randomIndex];

            return (PrizeType)Enum.Parse(typeof(PrizeType), randomPrize);
        }
		
		private void ControlHero(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow)
            {
                if (!Map.IsTopDeadEndAt(Map.HeroPosition))
                {
                    Map.HeroPosition = new Position(Map.HeroPosition.Left, Map.HeroPosition.Top - 1);
                }
                Map.Hero.Symbol = '↑';
            }

            if (key == ConsoleKey.RightArrow)
            {
                if (!Map.IsRightDeadEndAt(Map.HeroPosition))
                {
                    Map.HeroPosition = new Position(Map.HeroPosition.Left + 1, Map.HeroPosition.Top);
                }
                Map.Hero.Symbol = '→';
            }

            if (key == ConsoleKey.DownArrow)
            {
                if (!Map.IsBottomDeadEndAt(Map.HeroPosition))
                {
                    Map.HeroPosition = new Position(Map.HeroPosition.Left, Map.HeroPosition.Top + 1);
                }
                Map.Hero.Symbol = '↓';
            }

            if (key == ConsoleKey.LeftArrow)
            {
                if (!Map.IsLeftDeadEndAt(Map.HeroPosition))
                {
                    Map.HeroPosition = new Position(Map.HeroPosition.Left - 1, Map.HeroPosition.Top);
                }
                Map.Hero.Symbol = '←';
            }
        }
		
		private void PaintWallAtPosition(Position position, ConsoleColor color = ConsoleColor.DarkGray)
        {
            if (Map.HasWallAtPosition(position))
            {
                Wall wall = Map.GetWallFromPosition(position);
                wall.Color = color;
                wall.PrintAtPosition(position);
            }
        }
		
		private void PaintSurroundedWalls(Position position, ConsoleColor color = ConsoleColor.DarkGray)
        {
            for (int left = (int)-VisibilityRadius; left <= VisibilityRadius; left++)
            {
                for (int top = (int)-VisibilityRadius; top <= VisibilityRadius; top++)
                {
                    Position surroundedPosition = new Position(position.Left + (uint)left, position.Top + (uint)top);
                    PaintWallAtPosition(surroundedPosition, color);
                }
            }
        }
		
		private void PrintColoredSymbol(char symbol, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.Write(symbol);
            Console.ResetColor();
        }
		
		private void ShowRules()
        {
            Console.WriteLine("Press the arrow keys on the keyboard to control the hero.");
            Console.WriteLine("Control the hero to get to the Prize.\n");

            PrintColoredSymbol(Map.Hero.Symbol, Map.Hero.Color);
            Console.WriteLine(" – Hero symbol");

            Array prizeNames = Enum.GetValues(typeof(PrizeType));
            foreach (object name in prizeNames)
            {
                PrizeType prizeType = (PrizeType)Enum.Parse(typeof(PrizeType), name.ToString(), true);
                ConsoleColor color = GetPrizeColor(prizeType);

                PrintColoredSymbol('●', color);
                Console.WriteLine($" – {name} symbol, gives {(uint)(PrizeType)name} scores");
            }

            Console.WriteLine("\n\nPress any key to begin...");
            Console.ReadKey(true);
            Console.Clear();
        }
		
		private void ShowScores()
        {
            Console.Title = $"Scores: {Scores}";
        }
		
		private void UpdateScores(PrizeType prizeType)
        {
            Scores += (uint)prizeType;
        }
		
		private void ChangeWindowSize()
        {
            Console.SetWindowSize((int)Map.Width, (int)Map.Height);
        }
		
		private void StartInit()
        {
            ShowRules();
            ChangeWindowSize();
            ShowScores();
            Map.PrintWalls();
            Map.PrintPrizes();
        }
		
		private void ChangePrize(Prize prize)
        {
            Position newPrizePosition = Map.GetRandomFreePosition();
            PrizeType prizeType = GetRandomPrizeType();

            prize.PrizeType = prizeType;
            prize.Color = GetPrizeColor(prizeType);

            Map.RemovePrizeAtPosition(Map.HeroPosition);
            Map.AddPrizeAtPosition(newPrizePosition, prize);

            prize.PrintAtPosition(newPrizePosition);
        }
		
		public void Begin()
        {
            StartInit();

            ConsoleKey key;
            Position prevHeroPosition = new Position();
            Entity spaceSymbol;

            do
            {
                Map.PrintHero();
                if (!Map.HasHeroAtPosition(prevHeroPosition))
                {
                    PaintSurroundedWalls(Map.HeroPosition, LightingColor);
                }

                prevHeroPosition = Map.HeroPosition;
                spaceSymbol = new Entity(' ');

                key = Console.ReadKey(true).Key;
                ControlHero(key);

                if (!Map.HasHeroAtPosition(prevHeroPosition))
                {
                    spaceSymbol.PrintAtPosition(prevHeroPosition);
                    PaintSurroundedWalls(prevHeroPosition, DimmingColor);
                }

                if (Map.HasPrizeAtPosition(Map.HeroPosition))
                {
                    Prize prize = Map.GetPrizeFromPosition(Map.HeroPosition);

                    UpdateScores(prize.PrizeType);
                    ChangePrize(prize);
                    ShowScores();
                }
            }
            while (key != ConsoleKey.Escape);
        }
	}
}