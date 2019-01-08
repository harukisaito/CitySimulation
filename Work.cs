
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitySimulation{
    public class Work {

        private string name;
        private int income;
        private int stress;
        private int requirement;
        private float time;

        public string Name
        {
            get {return name;}
            set {name = value;}
        }

        public int Income
        {
            get {return income;}
            set {income = value;}
        }

        public int Stress
        {
            get {return stress;}
            set {stress = value;}
        }

        public int Requirement
        {
            get {return requirement;}
            set {requirement = value;}
        }

        public float Time
        {
            get {return time;}
            set {time = value;}
        }

        /// <summary>
        /// <param name="income">Pass the income of the work as int.</param>
        /// <param name="stress">Pass the amount of stress you experience due to the work as int.</param>
        /// <param name="energy">Pass the energy loss you experience due to the work as int.</param>
        /// <param name="requirement">Pass he requirement needed for the work as int.</param>
        /// </summary>
        public Work(string name, int income, int stress, int requirement, float time) 
        {
            this.name = name;
            this.income = income;
            this.stress = stress;
            this.requirement = requirement;
            this.time = time;
        }
    }
}