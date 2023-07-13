namespace Prize_Collector_Console_Game
{
    struct Position
    {
        public uint Left { get; private set; }
        public uint Top { get; private set; }

        public Position(uint left = 0, uint top = 0)
        {
            Left = left;
            Top = top;
        }
    }
}