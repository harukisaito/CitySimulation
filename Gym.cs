
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitySimulation{
    public class Gym : Exercise {

        protected int cost;

        protected int Cost
        {
            get {return cost;}
            set {cost = value;}
        }
        
        public Gym(string name, int cost, int energy, int fitness, int dopamine, double time):base(name, energy, fitness, dopamine, time)
        {
            this.cost = cost;
            this.energy = energy;
            this.fitness = fitness;
            this.dopamine = dopamine;
            this.time = time;
        }



    }
}