namespace CitySimulation
{
    class HumanWork : Work
    {

        private int energy;
        private int count;
        public int Energy
        {
            get {return energy;}
            set {energy = value;}
        }

        public int Count
        {
            get {return count;}
            set {count = value;}
        }
        public HumanWork(string name, int income, int stress, int energy, int requirement, float time, int count) : base(name, income, stress, requirement, time)
        {
            this.energy = energy;
            this.count = count;
        }

    }
}