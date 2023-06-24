namespace Prize_Collector_Console_Game
{
	class Position
    {
        public uint Left { get; protected set; }
        public uint Top { get; protected set; }

        public Position(uint left = 0, uint top = 0)
        {
            Left = left;
            Top = top;
        }
    }
}