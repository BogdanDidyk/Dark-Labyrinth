namespace Prize_Collector_Console_Game
{
    class Entity : Position
    {
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; }

        public Entity(uint left = 0, uint top = 0, char symbol = '*', ConsoleColor color = ConsoleColor.Gray) : base(left, top)
        {
            Symbol = symbol;
            Color = color;
        }

        public Entity(Position pos, char symbol = '*', ConsoleColor color = ConsoleColor.Gray) : this(pos.Left, pos.Top, symbol, color) { }
		
		public void Print()
        {
            Console.SetCursorPosition((int)Left, (int)Top);
            Console.ForegroundColor = Color;
            Console.Write(Symbol);
            Console.ResetColor();
        }
    }
}