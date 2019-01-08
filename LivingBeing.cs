using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CitySimulation
{
    class LivingBeing
    {
        protected string id;
        protected int age;
        protected int height;
        protected int weight;
        protected int energy;
        protected string gender;
        protected int fitness;
        protected int happiness;
        protected int health;

        // protected int digestion;


        public LivingBeing(string id, int age, int height, int weight, int energy, string gender, int fitness, int happiness, int health) 
        {
            this.id = id;
            this.age = age;
            this.height = height;
            this.weight = weight;
            this.energy = energy;
            this.gender = gender;
            this.fitness = fitness;
            this.happiness = happiness;
            this.health = health;
        }


        public string Id
        {
            get {return id;}
            set {id = value;}
        }

        public int Age
        {
            get {return age;}
            set {age = value;}
        }
        public int Height
        {
            get {return height;}
            set {height = value;}
        }
        public int Weight
        {
            get {return weight;}
            set {weight = value;}
        }
        public int Energy
        {
            get {return energy;}
            set {energy = value;}
        }
        public string Gender
        {
            get {return gender;}
            set {gender = value;}
        }
        public int Fitness
        {
            get {return fitness;}
            set {fitness = value;}
        }
        public int Happiness
        {
            get {return happiness;}
            set {happiness = value;}
        }

        public int Health
        {
            get {return health;}
            set {health = value;}
        }

        public void Sleep() 
        {
            Console.WriteLine("How long should " + Game.GameInstance.GetHuman().Name + " sleep? Type in the amount in hours. (max. 9,9 hours)");
            float amountOfSleep = float.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            Game.GameInstance.GetHuman().HealthStatus();
            Game.GameInstance.GetRealisticWorld().CurrentTime += amountOfSleep;
            health += 2;
            energy += 2;
            Game.GameInstance.GetHuman().HealthStatus();
            Game.GameInstance.GetRealisticWorld().ChooseMethod();
        }
    }
}