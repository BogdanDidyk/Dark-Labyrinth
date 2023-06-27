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
	
	public void MoveTop() => Top--;
	public void MoveRight() => Left++;
	public void MoveBottom() => Top++;
	public void MoveLeft() => Left--;
}