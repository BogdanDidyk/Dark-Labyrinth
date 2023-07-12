namespace Prize_Collector_Console_Game
{
    public enum PrizeType : uint { Silver = 3, Gold = 8, Diamond = 17 }

    class Prize : Entity
    {
        public PrizeType PrizeType { get; set; }

        public Prize(char symbol = '*', ConsoleColor color = ConsoleColor.Gray, PrizeType prizeType = PrizeType.Silver) : base(symbol, color)
        {
            PrizeType = prizeType;
        }
    }
}