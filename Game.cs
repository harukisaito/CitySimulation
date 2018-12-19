using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CitySimulation
{
    sealed class Game
    {
        public List<Universe> universes = new List<Universe>();
        public Universe currentPlayedUniverse;
        public static Game GameInstance = null;
        public string readJson;

        private Game()
        {
        }

        public static Game Instance()
        {
            if(GameInstance == null)
            {
                GameInstance = new Game();
            }
            return GameInstance;
        }

        public Human GetHuman()
        {
            return Game.GameInstance.currentPlayedUniverse.currentPlayedRealisticWorld.currentPlayedHuman;
        }

        public RealisticWorld GetRealisticWorld()
        {
            return Game.GameInstance.currentPlayedUniverse.currentPlayedRealisticWorld;
        }

        private Universe GetUniverse()
        {
            return Game.GameInstance.currentPlayedUniverse;
        }


        public void NewGame()
        {
            universes = Deserialize("universes", universes);
            Universe world = new Universe(Numerate(), NameUniverse());
            universes.Add(world);
            currentPlayedUniverse = world;
            // Serialize<Universe>("universes", universes);
            Serialize("universes");
            world.ChooseWorld();
        }

        internal void LoadGame()
        {
            int id = ChooseFromUniverseList();
            universes = Deserialize("universes", universes);
            currentPlayedUniverse = universes[id];
            currentPlayedUniverse.ChooseWorldContinue();
            // TODO: ADD THE OPPORTUNITY TO ADD DIFFERENT WORLDS.
        }

        private int Numerate()
        {
            // TODO: Think about how the GameInstance could know about all Games in worlds.json
            Console.WriteLine("UNIVERSE COUNT=" + universes.Count);
            return universes.Count + 1;
        }

        public int NumerateReal()
        {
            return GetUniverse().realisticWorlds.Count + 1;
        }

        public int NumerateWeird()
        {
            return GetUniverse().weirdWorlds.Count + 1;
        }

        public void ClearList()
        {
            universes.Clear();
            Serialize("universes");
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

        public int ChooseFromList<T>(string path, List<T> list)
        {
            Deserialize<T>(path, list);
            Console.WriteLine(list.Count);
            int counter = 1;
            foreach(T item in list)
            {
                Console.WriteLine("\n[" + counter + "]: " + item);
                counter++;
            }
            Console.WriteLine("\nChoose your preferred " + typeof(T).Name +  "! Press [1] to [" + list.Count + "]");
            return int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value) - 1;
        }

        public int ChooseFromUniverseList()
        {
            universes = Deserialize("universes", universes);
            DisplayUniverses();
            Console.WriteLine("\nChoose your preferred universe! Press [1] to [" + universes.Count + "]");
            return int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value) - 1;
        }

        public int ChooseFromRealisticWorldList()
        {
            universes = Deserialize("universes", universes);
            DisplayRealisticWorlds();
            Console.WriteLine("\nChoose your preferred world! Press [1] to [" + GetUniverse().realisticWorlds.Count + "]");
            return int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value) - 1;
        }

        public int ChooseFromHumanList()
        {
            universes = Deserialize("universes", universes);
            DisplayHumanList();
            Console.WriteLine("\nChoose your preferred human! Press [1] to [" + GetUniverse().realisticWorlds.Count + "]");
            return int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value) - 1;
        }

        public void DisplayUniverses()
        {
            int counter = 1;
            foreach(Universe u in universes)
            {
                Console.WriteLine("[" + counter + "]: " + u.Name);
                counter++;
            }
        }

        public void DisplayRealisticWorlds()
        {
            int counter = 1;
            foreach(RealisticWorld rw in GetUniverse().realisticWorlds)
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

        public void DisplayHumanList()
        {
            int counter = 1;
            foreach(Human h in GetRealisticWorld().humans)
            {
                Console.WriteLine("[" + counter + "] " + h.Name);
                h.HealthStatus();
                counter++;
            }
        }

        public void Quit()
        {
            Console.WriteLine("Autosaving your game...");
            Serialize("universes");
            Console.WriteLine("Saved!");
            Console.WriteLine("Byebye, please come again...");
            Environment.Exit(0);
        }

        public void ResetTask()
        {
            Deserialize<Task>("tasks", GetRealisticWorld().tasks);
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
                Serialize("universes");
            }
            return path;
        }

        public string ReadJson(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                return readJson = streamReader.ReadToEnd();
            }
        }

        public bool CheckJson(string certainPath)
        {
            string path = PathCreator(certainPath);
            ReadJson(path);
            bool empty = string.IsNullOrWhiteSpace(readJson);
            return empty;
        }

        public void FileWriter(string certainPath, string json)
        {
            string path = PathCreator(certainPath);
            System.IO.File.WriteAllText(path, json);
        }

        private void WriteToDifferentPaths(string certainPath, string json)
        {
            FileWriter(certainPath, json);
        }

        private string SerializeObj<T>(List<T> list)
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented, 
            new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            Console.WriteLine(json);
            return json;
        }

        private string SerializeObj()
        {
            string json = JsonConvert.SerializeObject(universes, Formatting.Indented, 
            new JsonSerializerSettings()
                { 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            Console.WriteLine("outside: " + json);
            return json;
        }

        public void Serialize<T>(string certainPath, List<T> list)
        {
            Console.WriteLine("SERIALIZING");
            string json = SerializeObj(list);
            WriteToDifferentPaths(certainPath, json);
        }

        public void Serialize(string certainPath)
        {
            string json;
            DisplayHumanList();
            Console.WriteLine("SERIALIZING");
            json = JsonConvert.SerializeObject(GetRealisticWorld().humans, Formatting.Indented, 
            new JsonSerializerSettings()
                { 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            WriteToDifferentPaths("humans", json);
            Console.WriteLine("1: inside" + json);
            DisplayHumanList();
            Console.WriteLine("SERIALIZING2");
            json = JsonConvert.SerializeObject(GetUniverse().realisticWorlds, Formatting.Indented, 
            new JsonSerializerSettings()
                { 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            WriteToDifferentPaths("realisticWorlds", json);
            Console.WriteLine("2: inside" + json);
            DisplayHumanList();
            Console.WriteLine("SERIALIZING3");
            json = JsonConvert.SerializeObject(Game.GameInstance.universes, Formatting.Indented, 
            new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            WriteToDifferentPaths("universes", json);
            Console.WriteLine("3 inside: " + json);
            DisplayHumanList();
            Console.WriteLine("SERIALIZING4");
            json = SerializeObj();
            WriteToDifferentPaths(certainPath, json);
            Console.WriteLine("4 inside: " + json);
            DisplayHumanList();
        }



        public void Deserialize<T>(string certainPath, List<T> list)
        {
            Console.WriteLine("DESERIALIZING");
            string path = PathCreator(certainPath);
            ReadJson(path);
            // Console.WriteLine(readJson);
            list = JsonConvert.DeserializeObject<List<T>>(readJson);
        }

        public List<Task> Deserialize(string certainPath, List<Task> tasks)
        {
            Console.WriteLine("DESERIALIZING");
            string path = PathCreator(certainPath);
            ReadJson(path);
            // Console.WriteLine(readJson);
            return JsonConvert.DeserializeObject<List<Task>>(readJson);
        }

        public List<Universe> Deserialize(string certainPath, List<Universe> universes)
        {
            Console.WriteLine("DESERIALIZING");
            string path = PathCreator(certainPath);
            readJson = ReadJson(path);
            Console.WriteLine(readJson);
            return JsonConvert.DeserializeObject<List<Universe>>(readJson);
        }

        public List<RealisticWorld> Deserialize(string certainPath, List<RealisticWorld> realisticworlds)
        {
            Console.WriteLine("DESERIALIZING");
            string path = PathCreator(certainPath);
            ReadJson(path);
            // Console.WriteLine(readJson);
            return realisticworlds = JsonConvert.DeserializeObject<List<RealisticWorld>>(readJson);
        }

        public List<WeirdWorld> Deserialize(string certainPath, List<WeirdWorld> weirdWorlds)
        {
            Console.WriteLine("DESERIALIZING");
            string path = PathCreator(certainPath);
            ReadJson(path);
            // Console.WriteLine(readJson);
            return weirdWorlds = JsonConvert.DeserializeObject<List<WeirdWorld>>(readJson);
        }

        public string[] Deserialize(string certainPath, string[] array)
        {
            Console.WriteLine("DESERIALIZING");
            string path = PathCreator(certainPath);
            ReadJson(path);
            // Console.WriteLine(readJson);
            return array = JsonConvert.DeserializeObject<string[]>(readJson);
        }

        public Food[] Deserialize(string certainPath, Food[] array)
        {
            Console.WriteLine("DESERIALIZING");
            string path = PathCreator(certainPath);
            ReadJson(path);
            // Console.WriteLine(readJson);
            return array = JsonConvert.DeserializeObject<Food[]>(readJson);
        }

        public Drink[] Deserialize(string certainPath, Drink[] array)
        {
            Console.WriteLine("DESERIALIZING");
            string path = PathCreator(certainPath);
            ReadJson(path);
            // Console.WriteLine(readJson);
            return array = JsonConvert.DeserializeObject<Drink[]>(readJson);
        }

        public Entertainment[] Deserialize(string certainPath, Entertainment[] array)
        {
            Console.WriteLine("DESERIALIZING");
            string path = PathCreator(certainPath);
            ReadJson(path);
            // Console.WriteLine(readJson);
            return array = JsonConvert.DeserializeObject<Entertainment[]>(readJson);
        }

        public PetFood[] Deserialize(string certainPath, PetFood[] array)
        {
            Console.WriteLine("DESERIALIZING");
            string path = PathCreator(certainPath);
            ReadJson(path);
            // Console.WriteLine(readJson);
            return array = JsonConvert.DeserializeObject<PetFood[]>(readJson);
        }

        public Exercise[] Deserialize(string certainPath, Exercise[] array)
        {
            Console.WriteLine("DESERIALIZING");
            string path = PathCreator(certainPath);
            ReadJson(path);
            // Console.WriteLine(readJson);
            return array = JsonConvert.DeserializeObject<Exercise[]>(readJson);
        }

        public Work[] Deserialize(string certainPath, Work[] array)
        {
            Console.WriteLine("DESERIALIZING");
            string path = PathCreator(certainPath);
            ReadJson(path);
            // Console.WriteLine(readJson);
            return array = JsonConvert.DeserializeObject<Work[]>(readJson);
        }

        public Human[] Deserialize(string certainPath, Human[] array)
        {
            Console.WriteLine("DESERIALIZING");
            string path = PathCreator(certainPath);
            ReadJson(path);
            // Console.WriteLine(readJson);
            return array = JsonConvert.DeserializeObject<Human[]>(readJson);
        }
    }
}