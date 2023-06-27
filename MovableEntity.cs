namespace Prize_Collector_Console_Game
{
    class MovableEntity : Position
    {
        public MovableEntity(Position position, char symbol = '*', ConsoleColor color = ConsoleColor.Gray) : base(position, symbol, color) { }
    }
	
	public void ChangePositionTo(Position position)
	{
		Left = position.Left;
		Top = position.Top;
	}
}