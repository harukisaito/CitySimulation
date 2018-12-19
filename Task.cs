using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitySimulation{
    class Task {

        private string name;
        private string content;
        private int reward;
        private int id;
        private int[] conditions;
        private bool completed;
        private int repetition;

        public string Name 
        {
            get {return name;}
            set {name = value;}
        }

        public string Content 
        {
            get {return content;}
            set {content = value;}
        }

        public int Reward 
        {
            get {return reward;}
            set {reward = value;}
        }

        public int Id 
        {
            get {return id;}
            set {id = value;}
        }

        public int[] Conditions
        {
            get {return conditions;}
            set {conditions = value;}
        }

        public bool Completed
        {
            get {return completed;}
            set {completed = value;}
        }

        public int Repetition
        {
            get {return repetition;}
            set {repetition = value;}
        }
    }
}