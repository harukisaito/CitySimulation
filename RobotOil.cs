namespace CitySimulation
{
    class RobotOil
    {
        private string name;
        private int oil;
        private int salt;
        private float time;

        public string Name
        {
            get {return name;}
            set {name = value;}
        }

        public int Oil
        {
            get {return oil;}
            set {oil = value;}
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