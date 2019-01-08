namespace CitySimulation
{
    class RobotFood
    {
        private string name;
        private int copper;
        private int aluminium;
        private int steel;
        private int salt;
        private float time;

        public string Name
        {
            get {return name;}
            set {name = value;}
        }

        public int Copper
        {
            get {return copper;}
            set {copper = value;}
        }

        public int Aluminium
        {
            get {return aluminium;}
            set {aluminium = value;}
        }

        public int Steel
        {
            get {return steel;}
            set {steel = value;}
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

       public RobotFood(int copper, int aluminium, int steel, int salt, float time)
       {
           this.copper = copper;
           this.aluminium = aluminium;
           this.steel = steel;
           this.salt = salt;
           this.time = time;
       }
    }
}