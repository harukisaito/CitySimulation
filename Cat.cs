
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitySimulation
{
    class Cat : Pet 
    {
        /// <summary>    
        /// <param name="name">Pass the name of the cat as string.</param>
        /// <param name="loyalty">Pass the loyalty of the cat as int.</param>
        /// <param name="cuteness">Pass the cuteness of the cat as int.</param>
        /// <param name="age">Pass the age of the cat as int.</param>
        /// <param name="height">Pass the height of the cat as int.</param>
        /// <param name="weight">Pass the weight of the cat as int.</param>
        /// <param name="energy">Pass the energy of the cat as int.</param>
        /// <param name="gender">Pass the gender of the cat as string.</param>
        /// <param name="fitness">Pass the fitness level of the cat as int.</param>
        /// <param name="happiness"> Pass the happiness level of the cat as int.</param>
        /// </summary>
        public Cat(string id, string name, int loyalty, int cuteness, int age, int height, int weight, int energy, string gender, int fitness, int happiness, string owner, int health):base(id, name, loyalty, cuteness, age,height,weight,energy,gender,fitness,happiness,owner, health)
        {
        }

    }
}