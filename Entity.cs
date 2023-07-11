namespace Prize_Collector_Console_Game
{
    class Entity
    {
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; }

        public Entity(char symbol = '*', ConsoleColor color = ConsoleColor.Gray)
        {
            Symbol = symbol;
            Color = color;
        }

        public void PrintAtPosition(Position position)
        {
            Console.SetCursorPosition((int)position.Left, (int)position.Top);
            Console.ForegroundColor = Color;
            Console.Write(Symbol);
            Console.ResetColor();
        }
    }
}