using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CitySimulation
{
    class Universe
    {
        public List<RealisticWorld> realisticWorlds = new List<RealisticWorld>();
        public List<WeirdWorld> weirdWorlds = new List<WeirdWorld>();
        private string name;
        protected double currentTime = 1;
        protected int day = 1;
        protected int id;

        public string Name
        {
            get {return name;}
            set {name = value;}
        }
        public double CurrentTime
        {
            get {return currentTime;}
            set {currentTime = value;}
        }
        public int Day 
        {
            get {return day;}
            set {day = value;}
        }
        public int Id
        {
            get {return id;}
            set {id = value;}
        }

        private Game game = Game.Instance();

        [JsonIgnore]
        public RealisticWorld currentPlayedRealisticWorld;
        [JsonIgnore]
        public WeirdWorld currentPlayedWeirdWorld;
        protected Random random = new Random();

        public Universe(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public void ChooseWorld()
        {
            Console.WriteLine("Press [1] for a realistic World\nPress [2] for a weird world");
            int choice = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            if(choice == 1)
            {
                currentPlayedRealisticWorld = new RealisticWorld(game.NumerateReal(), game.NameWorld());
                realisticWorlds.Add(currentPlayedRealisticWorld);
                currentPlayedRealisticWorld.Introduction();
            }
            if(choice == 2)
            {
                currentPlayedWeirdWorld = new WeirdWorld(game.NumerateWeird(), game.NameWorld());
                weirdWorlds.Add(currentPlayedWeirdWorld);
                currentPlayedWeirdWorld.Introduction();
            }
        }

        public void ChooseWorldContinue()
        {
            Console.WriteLine("Press [1] to choose from your realistic world/s\nPress [2] to add a realistic world\nPress [3] to choose from your weird world/s\nPress [4] to add a weird world");
            int choice = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            switch(choice)
            {
                case 1:
                    currentPlayedRealisticWorld = realisticWorlds[Game.GameInstance.ChooseFromRealisticWorldList()];
                    currentPlayedRealisticWorld.Continue();
                    break;
                case 2: 
                    currentPlayedRealisticWorld = new RealisticWorld(game.NumerateReal(), game.NameWorld());
                    realisticWorlds.Add(currentPlayedRealisticWorld);
                    Console.WriteLine("\nRealistic world added!");
                    currentPlayedRealisticWorld.Introduction();
                    break;
                case 3:
                    currentPlayedWeirdWorld = weirdWorlds[Game.GameInstance.ChooseFromList<WeirdWorld>("weirdWorlds", Game.GameInstance.Deserialize("weirdWorlds", weirdWorlds))];
                    currentPlayedWeirdWorld.Continue();
                    break;
                case 4:
                    currentPlayedWeirdWorld = new WeirdWorld(game.NumerateWeird(), game.NameWorld());
                    weirdWorlds.Add(currentPlayedWeirdWorld);
                    Console.WriteLine("\nWeird world added!");
                    currentPlayedWeirdWorld.Introduction();
                    break;
            }
        }
    }
}
