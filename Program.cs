using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CitySimulation
{
    class Program
    {
        public static void Main(string[] args)
        {
            Game.Instance();
            Console.WriteLine("\nPress [1] to start a new game\nPress [2] to continue your game\nPress [3] to quit the game\nPress [4] to clear the JSON");
            int choice = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            switch (choice)
            {
                case 1:
                    Game.GameInstance.NewGame();
                    break;
                case 2:
                    Game.GameInstance.LoadGame();
                    break;
                case 3:
                    Console.WriteLine("Byebye, please come again...");
                    Environment.Exit(0);
                    break;
                case 4:
                    Game.GameInstance.ClearList();
                    break;
                default:
                    break;
            }
        }
    }
}