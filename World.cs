using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CitySimulation
{
    class World
    {
        private int id;
        private string name;
        protected float currentTime = 1;
        protected int day = 1;

        public int Id
        {
            get {return id;}
            set {id = value;}
        }
        public string Name
        {
            get {return name;}
            set {name = value;}
        }
        public float CurrentTime
        {
            get {return currentTime;}
            set {currentTime = value;}
        }
        public int Day 
        {
            get {return day;}
            set {day = value;}
        }
        
        public List<RealisticWorld> realisticWorlds = new List<RealisticWorld>();
        public List<WeirdWorld> weirdWorlds = new List<WeirdWorld>();

        [JsonIgnore]
        public RealisticWorld currentPlayedRealisticWorld;
        [JsonIgnore]
        public WeirdWorld currentPlayedWeirdWorld;

        [JsonIgnore]
        public Human[] owners = new Human[] { };

        [JsonIgnore]
        public Food[] cookedFoods = new Food[] {};

        [JsonIgnore]
        public Food[] foods = new Food[] {};
        [JsonIgnore]
        public RobotFood[] robotFoods = new RobotFood[] {};
        [JsonIgnore]
        public AlienFood[] alienFoods = new AlienFood[] {};
        
        [JsonIgnore]
        public PetFood[] petFoods = new PetFood[] {};
        
        [JsonIgnore]
        public Drink freeCoffee = new Drink("Coffee", 2, 5, 1, 0, false, 0);
        [JsonIgnore]
        public Drink[] drinks = new Drink[] {};
        [JsonIgnore]
        public RobotOil[] robotOils = new RobotOil[] {};
        
        [JsonIgnore]
        public Exercise[] exercises = new Exercise[] {};
        
        [JsonIgnore]
        public HumanWork[] works = new HumanWork[] {};
        [JsonIgnore]
        public RobotWork[] robotWorks = new RobotWork[] {};
        
        [JsonIgnore]
        public Entertainment[] entertainments = new Entertainment[] {};
        
        [JsonIgnore]
        public string[] nameList = new string[] {};
        
        [JsonIgnore]
        public string[] genderList = new string[] {};

        public World(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        private void CreateNewRealisticWorld()
        {
            currentPlayedRealisticWorld = new RealisticWorld(Game.GameInstance.NumerateReal(), Game.GameInstance.NameWorld());
            realisticWorlds.Add(currentPlayedRealisticWorld);
            currentPlayedRealisticWorld.Introduction();
        }

        private void CreateNewWeirdWorld()
        {
            currentPlayedWeirdWorld = new WeirdWorld(Game.GameInstance.NumerateWeird(), Game.GameInstance.NameWorld());
            weirdWorlds.Add(currentPlayedWeirdWorld);
            currentPlayedWeirdWorld.Introduction();
        }

        public void ChooseWorld()
        {
            Console.WriteLine("Press [1] for a realistic World\nPress [2] for a weird world");
            int choice = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            if(choice == 1)
            {
                CreateNewRealisticWorld();
            }
            if(choice == 2)
            {
                CreateNewWeirdWorld();
            }
        }

        public void ChooseWorldContinue()
        {
            Console.WriteLine("Press [1] to choose from your realistic worlds\nPress [2] to add a realistic world\nPress [3] to choose from your weird worlds\nPress [4] to add a weird world");
            int choice = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            switch(choice)
            {
                case 1:
                    if(realisticWorlds.Count == 0)
                    {
                        Console.WriteLine("You have not created a realistic world yet. Creating one now...");
                        CreateNewRealisticWorld();
                    }
                    else
                    {
                        currentPlayedRealisticWorld = realisticWorlds[Game.GameInstance.ChooseFromRealisticWorldList()];
                        currentPlayedRealisticWorld.Continue();
                    }
                    break;
                case 2: 
                    CreateNewRealisticWorld();
                    break;
                case 3:
                    if(weirdWorlds.Count == 0)
                    {
                        Console.WriteLine("You have not created a weird world yet. Creating one now...");
                        CreateNewWeirdWorld();
                    }
                    else
                    {
                        currentPlayedWeirdWorld = weirdWorlds[Game.GameInstance.ChooseFromWeirdWorldList()];
                        currentPlayedWeirdWorld.Continue();
                    }
                    break;
                case 4:
                    CreateNewWeirdWorld();
                    break;
            }
        }
    }
}