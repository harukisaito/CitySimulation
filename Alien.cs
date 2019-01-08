namespace CitySimulation
{
    class Alien : LivingBeing
    {

        //CHANCE FOR A SUPER RARE POWER ALIEN

        private string name;
        private bool superPower;

        public string Name
        {
            get {return name;}
            set {name = value;}
        }

        public bool SuperPower
        {
            get {return superPower;}
            set {superPower = value;}
        }

        public Alien(string name, string id, int age, int height, int weight, int energy, string gender, int fitness, int happiness, int health, bool superPower) : base(id, age, height, weight, energy, gender, fitness, happiness, health)
        {
            this.name = name;
            this.superPower = superPower;
        }
    }
}