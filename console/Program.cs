using System;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Uno in CSharp");
            Console.WriteLine("Starting the Game");

            GameManager gameManager = new GameManager(4);
            gameManager.PlayGame();
            
        }
    }
}
