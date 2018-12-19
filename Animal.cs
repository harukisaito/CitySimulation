
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitySimulation{
    class Animal : LivingBeing 
    {
        public Animal(int age, int height, int weight, int energy, string gender, int fitness, int happiness, int health):base(age, height, weight, energy, gender, fitness, happiness, health)
        {
            this.age = age;
            this.height = height;
            this.weight = weight;
            this.energy = energy;
            this.gender = gender;
            this.fitness = fitness;
            this.happiness = happiness;
            this.health = health;
        }

    }
}