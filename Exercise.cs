
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitySimulation{
    public class Exercise {
        
        protected string name;
        protected int energy;
        protected int fitness;
        protected int dopamine;
        protected double time;
        
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

        public int Fitness
        {
            get {return fitness;}
            set {fitness = value;}
        }

        public int Dopamine
        {
            get {return dopamine;}
            set {dopamine = value;}
        }
        public double Time
        {
            get {return time;}
            set {time = value;}
        }

        /// <summary>
        /// <param name="energy">Pass the energy loss of the exercise as int.</param>
        /// <param name="fitness">Pass the increase of the fitness level as int.</param>
        /// <param name="dopamine">Pass the amount of dopamine you get as int.</param>
        /// </summary>
        public Exercise(string name, int energy, int fitness, int dopamine, double time) 
        {
            this.name = name;
            this.energy = energy;
            this.fitness = fitness;
            this.dopamine = dopamine;
            this.time = time;
        }
    }
}