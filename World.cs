using System;

namespace CitySimulation
{
    class World
    {
        private int id;
        private string name;
        private float currentTime = 1;
        private int day = 1;

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

        public World(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}