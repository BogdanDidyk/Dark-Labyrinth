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
    }
}