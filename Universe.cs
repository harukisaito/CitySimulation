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

        public World currentWorld;

        public Universe(int id, string name)
        {
            this.id = id;
            this.universeName = name;
        }

        public void CreateNewWorld()
        {
            currentWorld = new World(1, "world");
            currentWorld.ChooseWorld();
        }

        public void ContinueWorld()
        {
            currentWorld.ChooseWorldContinue();
        }
    }
}
