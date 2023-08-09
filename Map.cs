namespace Prize_Collector_Console_Game
{
    class Map
    {
        private Dictionary<Position, Wall> walls;
        private Dictionary<Position, Prize> prizes = new Dictionary<Position, Prize>();
        private readonly Random rnd;
		
		public uint Width { get; private set; }
        public uint Height { get; private set; }
        public uint WallsCount { get => (uint)walls.Count; }
        public uint PrizesCount { get => (uint)prizes.Count; }
        public uint HeroesCount { get => 1u; }
        public uint FreePositionsCount { get => Width * Height - WallsCount - PrizesCount - HeroesCount; }
		
		public Hero Hero { get; set; }
        public Position HeroPosition { get; set; }
		
		public Map(Dictionary<Position, Wall> wallPositions)
        {
            rnd = new Random();
            walls = wallPositions;
			
			var keys = wallPositions.Keys;
            Width = keys.Max(wall => wall.Left) + 1;
            Height = keys.Max(wall => wall.Top) + 1;
        }
		
		public bool HasWallAtPosition(Position wallPosition) => walls.ContainsKey(wallPosition);
        public Wall GetWallFromPosition(Position wallPosition) => walls.GetValueOrDefault(wallPosition, null);

        public bool HasPrizeAtPosition(Position prizePosition) => prizes.ContainsKey(prizePosition);
        public Prize GetPrizeFromPosition(Position prizePosition) => prizes.GetValueOrDefault(prizePosition, null);

        public bool HasHeroAtPosition(Position heroPosition) => HeroPosition.Left == heroPosition.Left && HeroPosition.Top == heroPosition.Top;

        public void AddPrizeAtPosition(Position prizePosition, Prize prize)
        {
            if (!HasPrizeAtPosition(prizePosition))
            {
                prizes.Add(prizePosition, prize);
            }
        }

        public void RemovePrizeAtPosition(Position prizePosition)
        {
            if (HasPrizeAtPosition(prizePosition))
            {
                prizes.Remove(prizePosition);
            }
        }
		
		public bool IsTopEdgeAt(Position pos) => pos.Top == 0;
        public bool IsRightEdgeAt(Position pos) => pos.Left == Width - 1;
        public bool IsBottomEdgeAt(Position pos) => pos.Top == Height - 1;
        public bool IsLeftEdgeAt(Position pos) => pos.Left == 0;

        public bool IsTopDeadEndAt(Position pos) => IsTopEdgeAt(pos) || HasWallAtPosition(new Position(pos.Left, pos.Top - 1));
        public bool IsRightDeadEndAt(Position pos) => IsRightEdgeAt(pos) || HasWallAtPosition(new Position(pos.Left + 1, pos.Top));
        public bool IsBottomDeadEndAt(Position pos) => IsBottomEdgeAt(pos) || HasWallAtPosition(new Position(pos.Left, pos.Top + 1));
        public bool IsLeftDeadEndAt(Position pos) => IsLeftEdgeAt(pos) || HasWallAtPosition(new Position(pos.Left - 1, pos.Top));

        public bool IsFreePositionAt(Position pos) => !(HasWallAtPosition(pos) || HasPrizeAtPosition(pos) || HasHeroAtPosition(pos));
		
		public void PrintWalls()
        {
            foreach (var keyValuePair in walls)
            {
                Position position = keyValuePair.Key;
                Wall wall = keyValuePair.Value;
                wall.PrintAtPosition(position);
            }
        }
		
		public void PrintPrizes()
        {
            foreach (var keyValuePair in prizes)
            {
                Position position = keyValuePair.Key;
                Prize prize = keyValuePair.Value;
                prize.PrintAtPosition(position);
            }
        }
		
		public void PrintHero()
        {
            Hero.PrintAtPosition(HeroPosition);
        }
		
		public Position GetRandomFreePosition()
        {
            uint left, top;
            Position pos;

            do
            {
                left = (uint)rnd.Next((int)Width);
                top = (uint)rnd.Next((int)Height);
                pos = new Position(left, top);
            }
            while (!IsFreePositionAt(pos));

            return pos;
        }
		
		public static Map LoadFromTxtFile(string pathToFile, char wallSymbolInFile = '*', char wallSymbolToSet = '*', ConsoleColor wallColorToSet = ConsoleColor.Gray)
        {
            string[] lines = File.ReadAllLines(pathToFile, System.Text.Encoding.Default);
            Dictionary<Position, Wall> wallPositions = new Dictionary<Position, Wall>();

            for (uint top = 0; top < lines.Length; top++)
            {
                for (uint left = 0; left < lines[top].Length; left++)
                {
                    char symbol = lines[top][(int)left];
                    if (symbol == wallSymbolInFile)
                    {
                        Position position = new Position(left, top);
                        wallPositions.Add(position, new Wall(wallSymbolToSet, wallColorToSet));
                    }
                }
            }

            return new Map(wallPositions);
        }
    }
}