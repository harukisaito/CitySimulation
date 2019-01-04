
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitySimulation{
    public class Food {
        
        protected string name;

        protected int energy;

        protected int taste;

        protected int healthiness;

        protected int cost;

        protected float time;

        public string Name
        {
            get {return name;}
            set {name = value;}
        }

        public int Energy
        {
            get {return energy;}
            set {energy = value;}
        }
        public int Taste
        {
            get {return taste;}
            set {taste = value;}
        }
        public int Healthiness
        {
            get {return healthiness;}
            set {healthiness = value;}
        }
        public int Cost
        {
            get {return cost;}
            set {cost = value;}
        }

        public float Time
        {
            get {return time;}
            set {time = value;}
        }

        /// <summary>
        /// <param name="energy">Pass the amount of energy in the food as int. (0-5)</param>
        /// <param name="taste">Pass the taste of the food as int. (0-5)</param>
        /// <param name="healthiness">Pass how bad the food is as int. (0-5)</param>
        /// <param name="cost">Pass the cost of the food as int.</param>
        /// </summary>
        public Food(string name, int energy, int taste, int healthiness, int cost, float time) 
        {
            this.name = name;
            this.energy = energy;
            this.taste = taste;
            this.healthiness = healthiness;
            this.cost = cost;
            this.time = time;
        }
    }
}