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
        private string universeName;
        private int id;
        
        public string UniverseName
        {
            get {return universeName;}
            set {universeName = value;}
        }

        public int Id
        {
            get {return id;}
            set {id = value;}
        }

        [JsonIgnore]
        public RealisticWorld currentPlayedRealisticWorld;
        [JsonIgnore]
        public WeirdWorld currentPlayedWeirdWorld;
        protected Random random = new Random();

        public Universe(int id, string name)
        {
            this.id = id;
            this.universeName = name;
        }

        public void ChooseWorld()
        {
            Console.WriteLine("Press [1] for a realistic World\nPress [2] for a weird world");
            int choice = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            if(choice == 1)
            {
                currentPlayedRealisticWorld = new RealisticWorld(Game.GameInstance.NumerateReal(), Game.GameInstance.NameWorld());
                realisticWorlds.Add(currentPlayedRealisticWorld);
                currentPlayedRealisticWorld.Introduction();
            }
            if(choice == 2)
            {
                currentPlayedWeirdWorld = new WeirdWorld(Game.GameInstance.NumerateWeird(), Game.GameInstance.NameWorld());
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
                    currentPlayedRealisticWorld = new RealisticWorld(Game.GameInstance.NumerateReal(), Game.GameInstance.NameWorld());
                    realisticWorlds.Add(currentPlayedRealisticWorld);
                    Console.WriteLine("\nRealistic world added!");
                    currentPlayedRealisticWorld.Introduction();
                    break;
                case 3:
                    // currentPlayedWeirdWorld = weirdWorlds[Game.GameInstance.ChooseFromList<WeirdWorld>("weirdWorlds", Game.GameInstance.Deserialize("weirdWorlds", weirdWorlds))];
                    currentPlayedWeirdWorld.Continue();
                    break;
                case 4:
                    currentPlayedWeirdWorld = new WeirdWorld(Game.GameInstance.NumerateWeird(), Game.GameInstance.NameWorld());
                    weirdWorlds.Add(currentPlayedWeirdWorld);
                    Console.WriteLine("\nWeird world added!");
                    currentPlayedWeirdWorld.Introduction();
                    break;
            }
        }
    }
}
