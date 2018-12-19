
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitySimulation
{
    class Dog : Pet
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Pass the name of the dog as string.</param>
        /// <param name="loyalty">Pass the loyalty of the dog as int.</param>
        /// <param name="cuteness">Pass the cuteness of the dog as int.</param>
        /// <param name="age">Pass the age of the dog as int.</param>
        /// <param name="height">Pass the height of the dog as int.</param>
        /// <param name="weight">Pass the weight of the dog as int.</param>
        /// <param name="energy">Pass the energy of the dog as int.</param>
        /// <param name="gender">Pass the gender of the dog as string.</param>
        /// <param name="fitness">Pass the fitness of the dog as int.</param>
        /// <param name="happiness">Pass the happiness of the dog as int.</param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public Dog(string name, int loyalty, int cuteness, int age, int height, int weight, int energy, string gender, int fitness, int happiness, Human owner, int health):base(name, loyalty, cuteness, age, height, weight, energy, gender, fitness, happiness, owner, health) 
        {
        }
    }
}