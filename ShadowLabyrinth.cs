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
	}
}