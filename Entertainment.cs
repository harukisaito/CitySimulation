
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitySimulation{
    public class Entertainment {
        

        /// <summary>
        /// 
        /// </summary>
        private int cost;
        private int happiness;
        private bool social;
        private double time;

        public int Cost
        {
            get {return cost;}
            set {cost = value;}
        }

        public int Happiness
        {
            get {return cost;}
            set {cost = value;}
        }

        public bool Social
        {
            get {return social;}
            set {social = value;}
        }

        public double Time
        {
            get {return time;}
            set {time = value;}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cost">Pass the cost of the activity as int.</param>
        /// <param name="happiness">Pass the happiness gained from the activity as int.</param>
        /// <param name="social">Pass the social aspect of the activity as bool.</param>
        public Entertainment(int cost, int happiness, bool social, double time) 
        {
            this.cost = cost;
            this.happiness = happiness;
            this.social = social;
            this.time = time;
        }


    }
}