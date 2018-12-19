
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitySimulation{
    public class Drink : Food {


        protected bool alcohol;

        public bool Alcohol
        {
            get {return alcohol;}
            set {alcohol = value;}
        }


        /// <summary>
        /// <param name="energy">Pass the amount of energy in the drink as int. (0-2)</param>
        /// <param name="taste">Pass the taste of the drink as int. (0-5)</param>
        /// <param name="healthiness">Pass how bad the drink is as int. (0-5)</param>
        /// <param name="cost">Pass the cost of the drink as int.</param>
        /// <param name="alcohol">Pass the presence of alcohol as bool.</param>
        /// </summary>        
        public Drink(string name, int energy, int taste, int healthiness, int cost, bool alcohol, double time):base(name, energy, taste, healthiness, cost, time)
        {
            this.name = name;
            this.energy = energy;
            this.taste = taste;
            this.healthiness = healthiness;
            this.cost = cost;
            this.alcohol = alcohol;
            this.time = time;
        }

    }
}