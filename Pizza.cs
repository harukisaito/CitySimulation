
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CitySimulation{
    class Pizza {
        private int sauce;
        private int cheese;
        private bool pepperonies;
        private bool mushrooms;
        private bool ham;
        private bool pineapple;
        private int counter = 1;

        Random random = new Random();
        public List<Human> pizzaHumans = new List<Human>();

        public int Sauce
        {
            get {return sauce;}
            set {sauce = value;}
        }

        public int Cheese
        {
            get {return cheese;}
            set {cheese = value;}
        }

        public bool Pepperonies
        {
            get {return pepperonies;}
            set {pepperonies = value;}
        }

        public bool Mushrooms
        {
            get {return mushrooms;}
            set {mushrooms = value;}
        }

        public bool Ham
        {
            get {return ham;}
            set {ham = value;}
        }

        public bool Pineapple
        {
            get {return pineapple;}
            set {pineapple = value;}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sauce">Pass the amount of sauce as int.</param>
        /// <param name="cheese">Pass the amount of cheese as int.</param>
        /// <param name="pepperonies">Pass the existence of pepperonies as bool.</param>
        /// <param name="mushrooms">Pass the existence of mushrooms as bool.</param>
        /// <param name="ham">Pass the existence of ham as bool.</param>
        /// <param name="pineapple">Pass the existence of pineapple as bool.</param>
        public Pizza(int sauce, int cheese, bool pepperonies, bool mushrooms, bool ham, bool pineapple)
        {
            this.sauce = sauce;
            this.cheese = cheese;
            this.pepperonies = pepperonies;
            this.mushrooms = mushrooms;
            this.ham = ham;
            this.pineapple = pineapple;
        }

        public void Describe()
        {
            Console.WriteLine("\nAmount of sauce: " + sauce + "\nAmount of cheese: " + cheese + "\nPepperonies: " + pepperonies + "\nMushrooms: " + mushrooms + "\nHam: " + ham + "\nPineapple: " + pineapple);
            PrintHumanList();
        }

        private void DisplayHumanAscii()
        {
            Console.WriteLine(
            "\n        .-."+
            "\n       (e.e)"+
            "\n        (m)"+
            "\n      .-= =-.  W"+
            "\n     // =T= \\,/"+
            "\n    () ==|== ()"+
            "\n     \\ =V="+
            "\n      M(oVo)"+
            "\n       // \\"+
            "\n      //   \\"+
            "\n     ()     ()"+
            "\n      \\    ||"+
            "\n       \\   '|"+
            "\n     ==       ==");
        }

        public void AdoptingHuman(Pizza pizza, List<Pizza> pizzas)
        {
            //pizza.pizzaHumans.Add(new Human(NamingHuman(), 0, 1, 50, 20, 10, "male", 0, 10, new KarmaKonto(0), 0, 10, Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld);
            pizza.Describe();
            DisplayHumanAscii();
            InputWork(pizza, pizzas);
        }

        public static string NamingHuman()
        {
            Console.WriteLine("Give your human a name!");
            string humanName = Console.ReadLine();
            return humanName;
        }
        public void InputWork(Pizza pizza, List<Pizza> pizzas)
        {
            Console.WriteLine("\nDo you want to go work?\n\nPress [1] to work in the office (Competence requirement 5)\nPress [2] to work in McDonalds (Competence requirement 0)\nPress [3] to work for a delivery service (Competence requirement 2)\nPress [4] to not work");
            int choiceWork = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            if(choiceWork == 4)
            {
                Entertainment(pizza, pizzas);
            }
            Work(Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.works[choiceWork-1], pizza, pizzas);
            Describe();
        }       

        public void RegeneratingEnergy(Pizza pizza, List<Pizza> pizzas)
        {
            Console.WriteLine("Your Sauce level is low. How about eating, drinking or resting a bit?");
            Console.WriteLine("\nPress [1] to eat something\nPress [2] to drink something\nPress [3] to rest\n");
            int choiceFood = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            switch(choiceFood)
            {
                case 1: SelectAndEatFood(pizza, pizzas);
                break;
                case 2: SelectAndDrink(pizza, pizzas);
                break;
                case 3: pizza.Rest(pizza, pizzas);
                break;
            }
        }

        public void SelectAndEatFood(Pizza pizza, List<Pizza> pizzas)
        {
            Console.WriteLine("\nChoose something to eat from [1] to [2]\n\nPress [1] to eat cheese\nPress [2] to eat dough");
            int choiceFood = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            Consume(Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.pizzaFoods[choiceFood-1], pizza, pizzas);
        }

        public void SelectAndDrink(Pizza pizza, List<Pizza> pizzas)
        {
            Console.WriteLine("\nChoose something to eat from [1] to [2]\n\nPress [1] to drink sauce\nPress [2] to drink Olive oil");
            int choiceFood = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            Consume(Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.pizzaDrinks[choiceFood-1], pizza, pizzas);
            Describe();
        }

        public void LosePounds(Pizza pizza, List<Pizza> pizzas)
        {
            Console.WriteLine("Have your human eat some bits off of you\nPress [1] to agree\nPress [2] to disagree");
            int choiceExercise = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            if(choiceExercise == 1)
            {
                Lose(pizza, pizzas);
            }
            else
            {
                Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.ChooseMethod(pizza, pizzas);
            }
        }

        public void Entertainment(Pizza pizza, List<Pizza> pizzas)
        {
            Console.WriteLine("Press [1] to lie in the oven\nPress [2] to sit by a fire");
            int choiceEntertainment = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            if(choiceEntertainment == 1)
            {
                pizza.Rest(pizza, pizzas);
            }
            if(choiceEntertainment == 2)
            {
                pizza.Rest(pizza, pizzas);
            }
        }

        public void ActivityWithHuman(Pizza pizza, List<Pizza> pizzas)
        {
            Console.WriteLine("Press [1] to feed your Human\nPress [2] to feed all your humans\nPress [3] to play with your human");
            int choiceActivityPet = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            switch(choiceActivityPet)
            {
                case 1: pizza.FeedHuman(pizza, pizzas, pizzaHumans[SelectHuman()], SelectHumanFood());
                break;
                case 2: pizza.FeedAllHumans(pizza, pizzas, SelectHumanFood());
                break;
                case 3: pizza.PlayWithHuman(pizza, pizzas, pizzaHumans[SelectHuman()]);
                break;
            }
        }

        public int SelectHuman()
        {
            counter = 1;
            foreach(Human human in pizzaHumans)
            {
                Console.WriteLine("\n[" + counter + "]  " + human.Name);
                counter++;
            }
            Console.WriteLine("\nSelect one of the humans from [1] to [" + pizzaHumans.Count + "]");
            int selectPet = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value)-1;
            return selectPet;
        }

        public Food SelectHumanFood()
        {
            counter = 1;
            for (int i = 0; i < Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.foods.Length; i++)
            {
                Console.WriteLine("\n[" + counter + "]  " + Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.foods[i].Name);
                counter++;
            }
            Console.WriteLine("\nSelect one of the food items from [1] to [" + Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.foods.Length + "]\n");
            int selectPetFood = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value)-1;
            return Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.foods[selectPetFood];
        }

        public void RescueHuman(Human humanToRescue, Pizza hero)
        {
            float randomChanceForOwning = random.Next(0, 100)/100.0f;
            Console.WriteLine("RandomChance= " + randomChanceForOwning);
            if(randomChanceForOwning < 0.5f)
            {
                hero.pizzaHumans.Add(humanToRescue);
                Console.WriteLine("Pizza has rescued a human from a tree and adopted it.");
            }
            else
            {
                Console.WriteLine("Pizza has rescued a human but it already has an owner");
            }
        }

        public void Work(Work work, Pizza pizza, List<Pizza> pizzas)
        {
            cheese += work.Income/10;
            sauce -= work.Stress;
            Console.WriteLine("\nYou gained some cheese and lost some sauce.");
            Describe();
            Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.ChooseMethod(pizza, pizzas);
        }

        public void PrintHumanList()
        {
            if(pizzaHumans.Count > 0)
            {
                foreach(Human human in pizzaHumans)
                {
                    Console.WriteLine("\nList of adopted Humans\n" + counter);
                    Console.WriteLine(human.Name);
                    counter++;
                }
            }
        }
        
        public void Rest(Pizza pizza, List<Pizza> pizzas)
        {
            cheese++;
            sauce++;
            Console.WriteLine("\nYou gained some cheese and sauce");
            Describe();
            Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.ChooseMethod(pizza, pizzas);

        }

        public void Consume(Food food, Pizza pizza, List<Pizza> pizzas)
        {
            cheese += food.Energy;
            sauce += food.Energy;
            Console.WriteLine("\nThe " + food.Name + " was delicous");
            Describe();
            Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.ChooseMethod(pizza, pizzas);
        }

        public void Lose(Pizza pizza, List<Pizza> pizzas)
        {
            cheese -= 5;
            sauce -= 5;
            Console.WriteLine("\nYou lost some cheese and sauce");
            Describe();
            Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.ChooseMethod(pizza, pizzas);
        }

        public void FeedHuman(Pizza pizza, List<Pizza> pizzas, Human human, Food food)
        {
            human.Energy += food.Energy;
            Console.WriteLine("\nYour human is happier");
            Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.ChooseMethod(pizza, pizzas);
        }

        public void FeedAllHumans(Pizza pizza, List<Pizza> pizzas, Food food)
        {
            foreach(Human human in pizzaHumans)
            {
                FeedHuman(pizza, pizzas, human, food);
            }
        }

        public void PlayWithHuman(Pizza pizza, List<Pizza> pizzas, Human human)
        {
            cheese -= 5;
            sauce -= 5;
            Console.WriteLine("\nYou lost some cheese and sauce but your human is happier");
            Describe();
            Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.ChooseMethod(pizza, pizzas);
        }
    }
}