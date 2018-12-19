
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitySimulation
{
    class Pet : Animal 
    {
        private Human owner;
    
        private string name;

        private int loyalty;

        private int cuteness;



        public string Name
        {
            get { return name;}
            set { name = value;}
        }

        public int Loyalty
        {
            get { return loyalty; }
            set { loyalty = value; }
        }

        public int Cuteness
        {
            get { return cuteness; }
            set { cuteness = value; }
        }

        public Human Owner
        {
            get { return owner;}
            set { owner = value;}
        }

        public Pet(string name, int loyalty, int cuteness, int age, int height, int weight, int energy, string gender, int fitness, int happiness, Human owner, int health):base(age, height, weight, energy, gender, fitness, happiness, health)
        {
            this.name = name;
            this.loyalty = loyalty;
            this.cuteness = cuteness;
            this.owner = owner;
            this.health = health;
        }

        public void OutputName()
        {
            Console.WriteLine("Name: " + name);
        }

        public void Describe()
        {
            Console.WriteLine("Pet name: " + name + "\nHappiness: " + happiness + "\nEnergy: " + energy + "\nWeight: " + weight);
        }

        public override string ToString()
        {
            return "\nName: " + name + "\nLoyalty: " + loyalty + "\nCuteness: " + cuteness + "\nAge: " + age + "\nHeight: " + height + "\nWeight: " + weight + "\nEnergy: " +  energy + "\nGender: " + gender + "\nFitness: " + fitness + "\nHappiness: " + happiness;
        }
    }
}