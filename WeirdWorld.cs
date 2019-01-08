using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CitySimulation
{
    class WeirdWorld : World
    {
        [JsonIgnore]
        public Robot currentPlayedRobot;
        public List<Robot> robots = new List<Robot>();

        public WeirdWorld(int id, string name) : base(id, name)
        {
        }

        private void DisplayRobotAscii()
        {
            Console.WriteLine(
            "         _\n" +
            "        [ ]\n" +
            "       (° °)\n" +
            "        |>|\n" +
            "     __/===\\__\n" +
            "    //| o=o |\\\n" +
            "  <]  | o=o |  [>\n" + 
            "      \\====/\n" +
            "     / / |  \\\n" + 
            "    <_________>\n");
            Console.WriteLine("\nThat's one ugly ass robot bro!");
        }

        private void CreateRobot()
        {
            Console.WriteLine("\nThe factory producing robots just kicked one out for its lacking capacbilites! From now on it's your robot.");
            currentPlayedRobot = new Robot(Game.GameInstance.AssignId(), 10, 10, 10, 10, 10, 10, 1);
            robots.Add(currentPlayedRobot);
            Console.WriteLine("The Name of your Robot is: " + Game.GameInstance.GetRobot().Id);
            DisplayRobotAscii();
            currentPlayedRobot.AdoptingAlien();
        }
        public void Introduction()
        {
            Console.WriteLine("\nStart the Simulation inside a Simulation inside a simulation...");
            CreateRobot();
        }

        public void Continue()
        {
            Console.WriteLine("\nStart the Simulation inside a Simulation inside a simulation...");
            Console.WriteLine("\nPress [1] to pick another robot up\nPress [2] to play with your exisiting robots");
            int choice = int.Parse(Regex.Match(Console.ReadLine(),@"\d").Value);
            if(choice == 1)
            {
                CreateRobot();
            }
            else if(choice == 2)
            {
                if(robots.Count == 1)
                {
                    currentPlayedRobot = robots[0];
                }
                else
                {
                    currentPlayedRobot = robots[Game.GameInstance.ChooseFromRobotList()];
                }
                Game.GameInstance.GetWeirdWorld().ChooseMethod();
            }
        }

        private void CheckTime()
        {
            if(currentTime >= 24)
            {
                CurrentTime %= 24;
                Day++;
            }
            float temp = currentTime;
            int hours = (int)currentTime;
            float minutesdouble = currentTime %= 1;
            minutesdouble *= 60;
            int minutes = (int)(minutesdouble + 0.5);
            if(hours <= 12)
            {
                DisplayTime(hours, minutes);
            }
            else
            {
                hours -= 12;
                DisplayTime(hours, minutes);
                hours += 12;
            }
            currentTime = temp;
            Console.WriteLine("Day: " + Day);
        }

        private void DisplayTime(int hours, int minutes)
        {
            if(minutes < 10)
            {
                Console.WriteLine("\nTime: " + hours + ":0" + minutes + " AM");
            }
            else
            {
                Console.WriteLine("\nTime: " + hours + ":" + minutes + " PM");
            }
        }

        public void ChooseMethod()
        {
            CheckTime();
            Game.GameInstance.GetRobot().AllStatus();
            Game.GameInstance.GetRobot().AlienStatus();
            Game.GameInstance.GetRobot().RegeneratingEnergy();
            Console.WriteLine("\nWhat do you want to do next?\nPress [1] to consume something\nPress [2] to fill your tank\nPress [3] to rest\nPress [4] to go work\nPress [5] to clean your robot\nPress [6] to do something entertaining\nPress [7] to feed one or all your aliens\nPress [8] to look for another alien to adopt\nPress [9] to improve your skills\n\nPress [10] to quit");
            switch(int.Parse(Regex.Match(Console.ReadLine(), @"\d+").Value))
            {
                case 1: 
                    Game.GameInstance.GetRobot().SelectAndEatFood();
                    break;
                case 2: 
                    Game.GameInstance.GetRobot().SelectAndDrinkOil();
                    break;
                case 3: 
                    Game.GameInstance.GetRobot().Rest();
                    break;
                case 4: 
                    Game.GameInstance.GetRobot().InputForWork();
                    break;
                case 5: 
                    Game.GameInstance.GetRobot().Clean();
                    break;
                case 6: 
                    Game.GameInstance.GetRobot().Entertainment();
                    break;
                case 7:
                    Game.GameInstance.GetRobot().FeedingAlien();
                    break;
                case 8: 
                    Random random = new Random();
                    if(random.Next(0,100) <= 30)
                    {
                        Game.GameInstance.GetRobot().AdoptingAlien();
                    }
                    else
                    {
                        Console.WriteLine("No luck this time!");
                        ChooseMethod();
                    }
                    break;
                case 9: 
                    Game.GameInstance.GetRobot().DecideForCompetence();
                    break;
                case 10: 
                    Game.GameInstance.Quit();
                    break;
            }
        }
    }
}
