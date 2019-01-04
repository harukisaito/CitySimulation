
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitySimulation
{
    class Animal : LivingBeing 
    {
        public Animal(string id, int age, int height, int weight, int energy, string gender, int fitness, int happiness, int health):base(id, age, height, weight, energy, gender, fitness, happiness, health)
        {
        }
    }
}