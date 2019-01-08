namespace CitySimulation
{
    class AlienFood
    {
        private string name;
        private int energy;
        private int salt;
        private float time;

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

        public int Salt
        {
           get {return salt;}
           set {salt = value;}
        } 

        public float Time
        {
            get {return time;}
            set {time = value;}
        }
    }
}