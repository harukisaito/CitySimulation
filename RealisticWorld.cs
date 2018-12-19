using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CitySimulation
{
    class RealisticWorld : Universe
    {
        [JsonIgnore]
        public Human[] owners = new Human[]
        {  
        };
        [JsonIgnore]
        public Food[] cookedFoods = new Food[]
        {
        };
        [JsonIgnore]
        public Food[] foods = new Food[]
        {
        };
        [JsonIgnore]
        public PetFood[] petFoods = new PetFood[]
        {
        };
        [JsonIgnore]
        public Drink freeCoffee = new Drink("Coffee", 2, 5, 1, 0, false, 0);
        [JsonIgnore]
        public Drink[] drinks = new Drink[]
        {
        };
        [JsonIgnore]
        public Exercise[] exercises = new Exercise[]
        {
        };
        [JsonIgnore]
        public Work[] works = new Work[]
        {
        };
        [JsonIgnore]
        public Entertainment[] entertainments = new Entertainment[]
        {
        };
        [JsonIgnore]
        public string[] nameList = new string[]
        {
        };
        [JsonIgnore]
        public string[] genderList = new string[]
        {
        };
        private static string gender;
        public List<Human> humans = new List<Human>();
        public List<Task> tasks = new List<Task>();
        [JsonIgnore]
        public Human currentPlayedHuman;
        private static Game game = Game.Instance();
        public RealisticWorld(int id, string name) : base(id, name)
        {
        }

        public void CompleteTask(int id)
        {
            tasks = Game.GameInstance.Deserialize("tasks", tasks);
            if(tasks[id].Completed == false && CheckConditions(id) == true || tasks[tasks[id].Id].Completed == false && tasks[id].Conditions.Length == 0)
            {
                Console.WriteLine(tasks[id].Name + tasks[id].Content);
                tasks[id].Completed = true;
            }
            Console.WriteLine(tasks[id].Id);
            Console.WriteLine(tasks[tasks[id].Id].Completed);
            Console.WriteLine(CheckConditions(id));
            Console.WriteLine("LENGTH= " + tasks[id].Conditions.Length);
            //Game.GameInstance.Serialize<Task>("tasks", tasks);
        }

        public bool CheckConditions(int id)
        {
            for(int i = 0; i < tasks[id].Conditions.Length; i++)
            {
                if(tasks[tasks[id].Conditions[i]].Completed == true)
                {
                    return true;
                }
            }
            return false;
        }

        public void ShowStatistics()
        {
            foreach(Human h in humans)
            {
                Console.WriteLine(h.ToString());
            }
        }

        public void Introduction()
        {
            Game.GameInstance.ResetTask();
            Console.WriteLine("\nStart the Simulation inside a Simulation inside a simulation...");
            Console.WriteLine("Let's create your human!\n\nPress [1] to create your own human\nPress [2] for a random human");
            int choiceHuman = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            ChooseHuman(choiceHuman);
        }

        public void Continue()
        {
            Console.WriteLine("\nStart the Simulation inside a Simulation inside a simulation...");
            Console.WriteLine("\nAdd a Human to your world?\n\nPress [1] to create your own human\nPress [2] for a random human\nPress [3] to keep playing with your existing humans");
            int choiceHuman = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            ChooseHuman(choiceHuman);
        }

        private void ChooseHuman(int choiceHuman)
        {
            switch(choiceHuman)
            {
                case 1:
                    GiveNameGender();
                    break;
                case 2:
                    bool empty = Game.GameInstance.CheckJson("universes");
                    Console.WriteLine(empty);
                    if(empty == false)
                    {
                        Game.GameInstance.Deserialize("universes", Game.GameInstance.universes);
                        CompleteTask(3);
                    }
                    nameList = Game.GameInstance.Deserialize("nameList", nameList);
                    genderList = Game.GameInstance.Deserialize("genderList", genderList);
                    currentPlayedHuman = new Human(nameList[random.Next(0, nameList.Length)], random.Next(10,30), random.Next(1,100), random.Next(50,200), random.Next(50,100), 10, genderList[random.Next(0, genderList.Length)],  10, 10, new KarmaKonto(0), 1, 10);
                    humans.Add(currentPlayedHuman);
                    foreach(Human h in Game.GameInstance.GetRealisticWorld().humans)
                    {
                        Console.WriteLine(h.Name);
                        h.HealthStatus();
                    }
                    CompleteTask(0);
                    currentPlayedHuman.AdoptPet();
                    break;
                case 3:
                    currentPlayedHuman = humans[Game.GameInstance.ChooseFromHumanList()];
                    ChooseMethod();
                    break;
            }
        }

        private void GiveNameGender()
        {
            Console.WriteLine("Give your human a name:");
            string name = Console.ReadLine();
            Console.WriteLine("Choose your gender\n\nPress [1] for male\nPress [2] for female");
            int choiceGender = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            if(choiceGender == 1)
            {
                gender = "male";
            }
            if( choiceGender == 2)
            {
                gender = "female";
            }
            CreateHuman(name, gender);
        }

        private void CreateHuman(string name, string gender) 
        {
            bool empty = Game.GameInstance.CheckJson("humans");
            if(empty == false)
            {
                Game.GameInstance.Deserialize("universes", Game.GameInstance.universes);
                CompleteTask(3);
            }
            currentPlayedHuman = new Human(name, random.Next(10,30), random.Next(1,100), random.Next(50,200), random.Next(50,100), 10, gender, 10, 10, new KarmaKonto(0), 1, 10);
            humans.Add(currentPlayedHuman);
            CompleteTask(0);
            currentPlayedHuman.AdoptPet();
        }

        private void CheckSleep()
        {
            if(currentTime >= 16 && currentTime <= 24)
            {
                Console.WriteLine("Let " + currentPlayedHuman.Name + " sleep for now? 8 hours is the recommended time of sleep\n\nPress [1] for Yes\nPress [2] for No");
                int sleepAnswer = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
                CheckTime();
                if(sleepAnswer == 1)
                {
                    currentPlayedHuman.Sleep();
                }
            }
        }

        private void CheckTime()
        {
            if(currentTime >= 24)
            {
                currentTime %= 24;
                day++;
            }
            double temp = currentTime;
            int hours = (int)currentTime;
            double minutesdouble = currentTime %= 1;
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
            Console.WriteLine("Day: " + day);
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
            Game.GameInstance.Serialize("universes");
            CheckTime();
            currentPlayedHuman.GetSick();
            CheckSleep();
            currentPlayedHuman.GetHungry();
            currentPlayedHuman.DebtCounter(currentPlayedHuman.LoanAmount);
            currentPlayedHuman.AllStatus();
            currentPlayedHuman.PetStatus();
            Console.WriteLine("\nWhat do you want to do next?\nPress [1] to eat something\nPress [2] to Drink something\nPress [3] to rest\nPress [4] to go work\nPress [5] to exercise\nPress [6] to do something entertaining\nPress [7] to either feed your pet or play with it\nPress [8] adpot another pet\nPress [9] to improve your skills for a certain job\nPress [10] to sleep\nPress [11] to get a loan or pay the debt\nPress [12] to show the statistics of all humans\n\nPress [13] to quit");
            int choiceMethod = int.Parse(Regex.Match(Console.ReadLine(), @"\d+").Value);
            switch(choiceMethod)
            {
                case 1: 
                    currentPlayedHuman.SelectAndEat();
                    break;
                case 2: 
                    currentPlayedHuman.SelectAndDrink();
                    break;
                case 3: 
                    currentPlayedHuman.Rest();
                    break;
                case 4: 
                    int luck = random.Next(0,100);
                    if(luck > 80)
                    {
                        currentPlayedHuman.RescuePet(new Cat("", 10, 10, 10, 10, 10, 10, Game.GameInstance.Deserialize("genderList", genderList)[random.Next(0, genderList.Length)], 10, 10, currentPlayedHuman, 10)); 
                    }
                    currentPlayedHuman.InputForWork();
                    break;
                case 5: 
                    currentPlayedHuman.InputForExercise();
                    break;
                case 6: 
                    currentPlayedHuman.DecideForEntertainment();
                    break;
                case 7: 
                    currentPlayedHuman.ActivityWithPet();
                    break;
                case 8: 
                    currentPlayedHuman.AdoptPet();
                    break;
                case 9: 
                    currentPlayedHuman.DecideForCompetence();
                    break;
                case 10: 
                    currentPlayedHuman.Sleep();
                    break;
                case 11: 
                    currentPlayedHuman.GetALoan();
                    break;
                case 12: 
                    ShowStatistics();
                    ChooseMethod();
                    break;
                case 13: 
                    game.Quit();
                    break;
            }
        }
    }
}
