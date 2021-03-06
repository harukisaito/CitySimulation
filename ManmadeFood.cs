
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitySimulation{
    public class ManmadeFood : Food {
        
        /// <summary>
        /// <param name="energy">Pass the amount of energy in the food as int. (0-5)</param>
        /// <param name="taste">Pass the taste of the food as int. (0-5)</param>
        /// <param name="healthiness">Pass how bad the food is as int. (0-5)</param>
        /// <param name="cost">Pass the cost of the food as int.</param>
        /// </summary>
        public ManmadeFood(string name, int energy, int taste, int healthiness, int cost, double time):base(name, energy, taste, healthiness, cost, time)
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