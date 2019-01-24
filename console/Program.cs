using System;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Uno in CSharp");
            Console.WriteLine("Starting the Game");

            int humanPlayers = 0;
            Console.WriteLine("How many players want to play?");
            if (int.TryParse(Console.ReadLine(), out int parseResult))
                humanPlayers = parseResult;
            else
                Console.WriteLine("Don't fuck with me, press a fucking number, game will be start with only bots");

            GameManager gameManager = new GameManager(humanPlayers);
            gameManager.PlayGame();
        }
    }
}
