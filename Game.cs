using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CitySimulation
{
    sealed class Game
    {
        public List<Universe> universes = new List<Universe>();
        public Universe currentPlayedUniverse;
        public static Game GameInstance = null;

        private Game()
        {
        }

        public static Game Instance()
        {
            if(GameInstance == null)
            {
                GameInstance = new Game();
            }
            else
            {
                throw new Exception("This Singleton already has an instance!");            
            }
            return GameInstance;
        }

        public Human GetHuman()
        {
            return Game.GameInstance.currentPlayedUniverse.currentWorld.currentPlayedRealisticWorld.currentPlayedHuman;
        }

        public RealisticWorld GetRealisticWorld()
        {
            return Game.GameInstance.currentPlayedUniverse.currentWorld.currentPlayedRealisticWorld;
        }

        public Robot GetRobot()
        {
            return Game.GameInstance.currentPlayedUniverse.currentWorld.currentPlayedWeirdWorld.currentPlayedRobot;
        }

        public WeirdWorld GetWeirdWorld()
        {
            return Game.GameInstance.currentPlayedUniverse.currentWorld.currentPlayedWeirdWorld;
        }

        public World GetWorld()
        {
            return Game.GameInstance.currentPlayedUniverse.currentWorld;
        }

        public string AssignId()
        {
            return Guid.NewGuid().ToString();
        }


        public void NewGame()
        {
            universes = Deserialize("universes", universes);
            currentPlayedUniverse = new Universe(Numerate(), NameUniverse());
            universes.Add(currentPlayedUniverse);
            currentPlayedUniverse.CreateNewWorld();
        }

        public void LoadGame()
        {
            universes = Deserialize("universes", universes);
            if(universes == null)
            {
                Console.WriteLine("You have not created an universe yet. Creating Universe now...");
                NewGame();
            }
            else
            {
                currentPlayedUniverse = universes[ChooseFromUniverseList()];
                currentPlayedUniverse.ContinueWorld();
            }
        }

        private int Numerate()
        {
            Console.WriteLine("UNIVERSE COUNT=" + universes.Count);
            return universes.Count + 1;
        }

        public int NumerateReal()
        {
            return GetWorld().realisticWorlds.Count + 1;
        }

        public int NumerateWeird()
        {
            return GetWorld().weirdWorlds.Count + 1;
        }

        public void ClearList()
        {
            universes.Clear();
            SerializeUniverse("universes");
        }

        private string NameUniverse()
        {
            Console.WriteLine("Give your universe a name!");
            return Console.ReadLine();
        }

        public string NameWorld()
        {
            Console.WriteLine("Give your world a name!");
            return Console.ReadLine();
        }

        // public int ChooseFromList<T>(string path, List<T> list)
        // {
        //     Deserialize<T>(path, list);
        //     Console.WriteLine(list.Count);
        //     int counter = 1;
        //     foreach(T item in list)
        //     {
        //         Console.WriteLine("\n[" + counter + "]: " + item);
        //         counter++;
        //     }
        //     Console.WriteLine("\nChoose your preferred " + typeof(T).Name +  "! Press [1] to [" + list.Count + "]");
        //     return int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value) - 1;
        // }

        public int ChooseFromUniverseList()
        {
            DisplayUniverses();
            Console.WriteLine("\nChoose your preferred universe! Press [1] to [" + universes.Count + "]");
            return int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value) - 1;
        }

        public int ChooseFromRealisticWorldList()
        {
            DisplayRealisticWorlds();
            Console.WriteLine("\nChoose your preferred world! Press [1] to [" + GetWorld().realisticWorlds.Count + "]");
            return int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value) - 1;
        }

        public int ChooseFromWeirdWorldList()
        {
            DisplayWeirdWorlds();
            Console.WriteLine("\nChoose your preferred world! Press [1] to [" + GetWorld().weirdWorlds.Count + "]");
            return int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value) - 1;
        }

        public int ChooseFromHumanList()
        {
            DisplayHumanList();
            Console.WriteLine("\nChoose your preferred human! Press [1] to [" + GetRealisticWorld().humans.Count + "]");
            return int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value) - 1;
        }

        public int ChooseFromRobotList()
        {
            DisplayRobotList();
            Console.WriteLine("\nChoose your preferred robot! Press [1] to [" + GetWeirdWorld().robots.Count + "]");
            return int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value) - 1;
        }

        private void DisplayUniverses()
        {
            int counter = 1;
            foreach(Universe u in universes)
            {
                Console.WriteLine("[" + counter + "]: " + u.UniverseName);
                counter++;
            }
        }

        private void DisplayRealisticWorlds()
        {
            int counter = 1;
            foreach(RealisticWorld rw in GetWorld().realisticWorlds)
            {
                Console.WriteLine("[" + counter + "]: " + rw.Name);
                Console.WriteLine("\nTime: " + rw.CurrentTime);
                Console.WriteLine("Day: " + rw.Day);
                counter++;
                Console.WriteLine("\nHumans on world " + rw.Name + ": ");
                int counterHuman = 1;
                foreach(Human h in rw.humans)
                {
                    Console.WriteLine("\n[" + counterHuman + "] " + h.Name);
                    h.HealthStatus();
                    Console.WriteLine("");
                    counterHuman++;
                }
            }
        }

        private void DisplayWeirdWorlds()
        {
            int counter = 1;
            foreach(WeirdWorld ww in GetWorld().weirdWorlds)
            {
                Console.WriteLine("[" + counter + "]: " + ww.Name);
                Console.WriteLine("\nTime: " + ww.CurrentTime);
                Console.WriteLine("Day: " + ww.Day);
                counter++;
                Console.WriteLine("\nHumans on world " + ww.Name + ": ");
                int counterRobot = 1;
                foreach(Robot r in ww.robots)
                {
                    Console.WriteLine("\n[" + counterRobot + "] " + r.Id);
                    r.AllStatus();
                    Console.WriteLine("");
                    counterRobot++;
                }
            }
        }

        private void DisplayHumanList()
        {
            int counter = 1;
            foreach(Human h in GetRealisticWorld().humans)
            {
                Console.WriteLine("[" + counter + "] " + h.Name);
                h.HealthStatus();
                counter++;
            }
        }
        private void DisplayRobotList()
        {
            int counter = 1;
            foreach(Robot r in GetWeirdWorld().robots)
            {
                Console.WriteLine("[" + counter + "] " + r.Id);
                r.AllStatus();
                counter++;
            }
        }

        public void Quit()
        {
            Console.WriteLine("Autosaving your game...");
            SerializeUniverse("universes");
            Console.WriteLine("Saved!");
            Console.WriteLine("Byebye, please come again...");
            Environment.Exit(0);
        }

        public void ResetTask()
        {
            // Deserialize<Task>("tasks", GetRealisticWorld().tasks);
            foreach(Task t in GetRealisticWorld().tasks)
            {
                t.Completed = false;
                t.Repetition = 0;
            }
        }

        private string PathCreator(string certainPath)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Json/" + certainPath + ".json");
            if(!File.Exists(path))
            {
                File.Create(path).Close();
                // SerializeUniverse("universes");
            }
            return path;
        }

        private string ReadJson(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                return streamReader.ReadToEnd();
            }
        }

        public bool CheckJson(string certainPath)
        {
            return string.IsNullOrWhiteSpace(ReadJson(PathCreator(certainPath)));
        }

        private void FileWriter(string certainPath, string json)
        {
            System.IO.File.WriteAllText(PathCreator(certainPath), json);
        }

        // private string SerializeObj<T>(List<T> list)
        // {
        //     string json = JsonConvert.SerializeObject(list, Formatting.Indented, 
        //     new JsonSerializerSettings()
        //         {
        //             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //         }
        //     );
        //     Console.WriteLine(json);
        //     return json;
        // }

        private string SerializeUniverseList()
        {
            return JsonConvert.SerializeObject(universes, Formatting.Indented);
        }

        public void SerializeUniverse(string certainPath)
        {
            Console.WriteLine("SERIALIZING UNIVERSE");
            FileWriter(certainPath, SerializeUniverseList());
        }

        // public List<Human> Deserialize(string certainPath, List<Human> humans)
        // {
        //     Console.WriteLine("DESERIALIZING");
        //     return JsonConvert.DeserializeObject<List<Human>>(ReadJson(PathCreator(certainPath)));
        // }

        public List<Task> Deserialize(string certainPath, List<Task> tasks)
        {
            Console.WriteLine("DESERIALIZING");
            return JsonConvert.DeserializeObject<List<Task>>(ReadJson(PathCreator(certainPath)));
        }

        public List<Universe> Deserialize(string certainPath, List<Universe> universes)
        {
            Console.WriteLine("DESERIALIZING");
            return JsonConvert.DeserializeObject<List<Universe>>(ReadJson(PathCreator(certainPath)));
        }

        public string[] Deserialize(string certainPath)
        {
            return JsonConvert.DeserializeObject<string[]>(ReadJson(PathCreator(certainPath)));
        }

        public Food[] Deserialize(string certainPath, Food[] array)
        {
            return JsonConvert.DeserializeObject<Food[]>(ReadJson(PathCreator(certainPath)));
        }

        public RobotFood[] Deserialize(string certainPath, RobotFood[] array)
        {
            return JsonConvert.DeserializeObject<RobotFood[]>(ReadJson(PathCreator(certainPath)));
        }

        public Drink[] Deserialize(string certainPath, Drink[] array)
        {
            return JsonConvert.DeserializeObject<Drink[]>(ReadJson(PathCreator(certainPath)));
        }

        public RobotOil[] Deserialize(string certainPath, RobotOil[] array)
        {
            return JsonConvert.DeserializeObject<RobotOil[]>(ReadJson(PathCreator(certainPath)));
        }

        public Entertainment[] Deserialize(string certainPath, Entertainment[] array)
        {
            return JsonConvert.DeserializeObject<Entertainment[]>(ReadJson(PathCreator(certainPath)));
        }

        public PetFood[] Deserialize(string certainPath, PetFood[] array)
        {
            return JsonConvert.DeserializeObject<PetFood[]>(ReadJson(PathCreator(certainPath)));
        }

        public Exercise[] Deserialize(string certainPath, Exercise[] array)
        {
            return JsonConvert.DeserializeObject<Exercise[]>(ReadJson(PathCreator(certainPath)));
        }

        public HumanWork[] Deserialize(string certainPath, HumanWork[] array)
        {
            return JsonConvert.DeserializeObject<HumanWork[]>(ReadJson(PathCreator(certainPath)));
        }

        public RobotWork[] Deserialize(string certainPath, RobotWork[] array)
        {
            return JsonConvert.DeserializeObject<RobotWork[]>(ReadJson(PathCreator(certainPath)));
        }

        public Human[] Deserialize(string certainPath, Human[] array)
        {
            return JsonConvert.DeserializeObject<Human[]>(ReadJson(PathCreator(certainPath)));
        }
    }
}