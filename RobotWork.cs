namespace CitySimulation
{
    class RobotWork : Work
    {
        private int difficulty;

        public int Difficulty
        {
            get {return difficulty;}
            set {difficulty = value;}
        }

        public RobotWork(string name, int income, int stress, int requirement, float time, int difficulty) : base(name, income, stress, requirement, time)
        {
            this.difficulty = difficulty;
        }
    }
}