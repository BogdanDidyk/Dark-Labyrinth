using System;
using System.Text;

namespace Prize_Collector_Console_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.Unicode;

            
            try
            {
                ConsoleColor dimmingColor = ConsoleColor.Black;
                ConsoleColor lightingColor = ConsoleColor.DarkGray;

                Map map = Map.LoadFromTxtFile("map.txt", '*', '█', dimmingColor);
                ShadowLabyrinth game = new ShadowLabyrinth(map, dimmingColor, lightingColor, 2, 10);
                game.Begin();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong... Try to restart the game.");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            Console.Read();
        }
    }
}

