namespace Prize_Collector_Console_Game
{
    class Entity : Position
    {
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; }

        public Entity(Position position, char symbol = '*', ConsoleColor color = ConsoleColor.Gray) : base(position.Left, position.Top)
        {
            Symbol = symbol;
            Color = color;
        }
		
		public void Print()
        {
            Console.SetCursorPosition((int)Left, (int)Top);
            Console.ForegroundColor = Color;
            Console.Write(Symbol);
            Console.ResetColor();
        }
		
        public bool IsLocatedAtPosition(Position position) => Left == position.Left && Top == position.Top;
    }
}