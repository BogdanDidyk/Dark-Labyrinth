namespace Prize_Collector_Console_Game
{
	class ShadowLabyrinth
    {
		private Random rnd = new Random();

        public Map Map { get; private set; }
        public uint VisibilityRadius { get; private set; }
        public ConsoleColor DimmingColor { get; private set; }
        public ConsoleColor LightingColor { get; private set; }
        public uint Scores { get; private set; }
		
	    public ShadowLabyrinth(Map map, ConsoleColor dimmingColor = ConsoleColor.Black, ConsoleColor lightingColor = ConsoleColor.DarkGray, uint visibilityRadius = 1, uint prizesCount = 1)
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
	}
}