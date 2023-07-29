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
	}
}