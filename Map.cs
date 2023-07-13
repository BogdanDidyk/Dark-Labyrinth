namespace Prize_Collector_Console_Game
{
    class Map
    {
        private Dictionary<Position, Wall> walls;
        private Dictionary<Position, Prize> prizes = new Dictionary<Position, Prize>();
        private readonly Random rnd;
		
		public Map(Dictionary<Position, Wall> wallPositions)
        {
            rnd = new Random();
            walls = wallPositions;
        }
    }
}