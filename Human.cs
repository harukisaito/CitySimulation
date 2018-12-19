
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CitySimulation
{
    class Human : LivingBeing
    {
        public Debt debt = new Debt();
        public List<Pet> pets = new List<Pet>();
        private KarmaKonto karmaKonto;
        private Random random = new Random();
        private string name;
        private int money;
        private int competence;
        private float loanAmount;
        private int regularCounter;
        public KarmaKonto KarmaKonto
        {
            get {return karmaKonto;}
            set {karmaKonto = value;}
        }
        

        public string Name
        {
            get {return name;}
            set {name = value;}
        }

        public int Money
        {
            get {return money;}
            set {money = value;}
        }

        public int Competence
        {
            get {return competence;}
            set {competence = value;}
        }

        [JsonIgnore]
        public float LoanAmount
        {
            get {return loanAmount;}
            set {loanAmount = value;}
        }

        /// /// <param name="name">Pass the name of a human as string.</param>
        /// <summary>
        /// <param name="money">Pass the financial status of a human as int.</param>
        /// <param name="age">Pass the age of a human as int.</param>
        /// <param name="height">Pass the height of a human as int.</param>
        /// <param name="weight">Pass the weight of a human as int.</param>
        /// <param name="energy">Pass the energy of a human as int.</param>
        /// <param name="gender">Pass the gender of a human as string.</param>
        /// <param name="fitness">Pass the fitness level of a human as int.</param>
        /// <param name="happiness">Pass the happiness level of a human as int.</param>
        /// <param name="karmaKonto"></param>
        /// <param name="competence">Pass the competence of a human as int.</param>
        /// </summary>
        public Human(string name, int money, int age, int height, int weight, int energy, string gender, int fitness, int happiness, KarmaKonto karmaKonto, int competence, int health):base(age, height, weight, energy, gender, fitness, happiness, health)
        {
            this.name = name;
            this.money = money;
            this.age = age;
            this.height = height;
            this.weight = weight;
            this.energy = energy;
            this.gender = gender;
            this.fitness = fitness;
            this.happiness = happiness;
            this.karmaKonto = karmaKonto;
            this.competence = competence;
            this.health = health;
        }

        public void Describe()
        {
            Console.WriteLine("\nName: " + name + "\nFinancial status: " + money + "$" + "\nAge: " + age + "\nHeight: " + height + "\nWeight: " + weight + "\nEnergy level: " + energy + "\nSex: " + gender + "\nFitness level: " + fitness + "\nHappiness level: " + happiness + "\nCompetence: " + competence + "\n");
            PrintKarmaPoints();
            PrintPetList();
        }

        public void OutputName()
        {
            Console.WriteLine("Name: " + name);
        }

        public override string ToString()
        {
            return "\nName: " + name + "\nFinancial status: " + money + "$" + "\nAge: " + age + "\nHeight: " + height 
            + "\nWeight: " + weight + "\nEnergy level: " + energy + "\nSex: " + gender + "\nFitness level: " + fitness 
            + "\nHappiness level: " + happiness + "\nCompetence: " + competence + "\n" + karmaKonto + "\n" + PrintPetList();
        }

        private void PrintKarmaPoints()
        {
            karmaKonto.Describe();
        }

        private string PrintPetList()
        {
            if(this.pets.Count > 0)
            {
                StringBuilder petlist = new StringBuilder();
                petlist.Append("\nAll Pets of " + this.name + ":\n");
                int counter = 1;
                foreach (Pet pet in pets)
                {
                    petlist.Append(counter + ":\n" + pet);
                    counter++;
                }
                return petlist.ToString();
            }
            else
            {
                return this.name + " has no pets.";
            }
        }

        private void RegenerateEnergy()
        {
            Console.WriteLine("\nPress [1] to eat something\nPress [2] to drink something\nPress [3] to rest\n");
            int choiceFood = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            switch(choiceFood)
            {
                case 1: 
                    SelectAndEat();
                    break;
                case 2: 
                    SelectAndDrink();
                    break;
                case 3: 
                    Rest();
                    break;
            }
            if(energy <= 5)
            {
                Console.WriteLine("\nYour Energy Level is low you should eat something");
            }
        }

        public int HowtoEat()
        {
            Console.WriteLine("Press [1] to cook something yourself\nPress [2] to eat out\nPress [3] to search for a food item and eat it");
            int howToEat = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            return howToEat;
        }

        public void SelectAndEat()
        {
            int howToEat = HowtoEat();
            switch(howToEat)
            {
                case 1:
                    DisplayFoodList(Game.GameInstance.Deserialize("cookedFoods", Game.GameInstance.GetRealisticWorld().cookedFoods), howToEat);
                    break;
                case 2:
                    DisplayFoodList(Game.GameInstance.Deserialize("foods", Game.GameInstance.GetRealisticWorld().foods), howToEat);
                    break;
                case 3:
                    Game.GameInstance.GetRealisticWorld().foods = Game.GameInstance.Deserialize("foods", Game.GameInstance.GetRealisticWorld().foods);
                    SearchAndEat();
                    break;
            }
        }

        public void DisplayFoodList(Food[] foods, int cookOrEat)
        {
            int counter = 1;
            foreach(Food food in foods)
            {
                Console.Write("\n[" + counter + "]  ");
                Console.Write(food.Cost + "$ " + food.Name + "\n");
                counter++;
            }
            Console.WriteLine("\nChoose your preferred food item from [1] to [" + foods.Length + "]");
            FinancialStatus();
            int selectFood = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value)-1;
            Eat( foods[selectFood], cookOrEat);
        }


        public void SelectAndDrink()
        {   
            Game.GameInstance.GetRealisticWorld().drinks = Game.GameInstance.Deserialize("drinks", Game.GameInstance.GetRealisticWorld().drinks);
            int counter = 1;
            foreach (Drink drink in Game.GameInstance.GetRealisticWorld().drinks)
            {
                Console.Write("\n[" + counter + "]  " + drink.Cost + "$  ");
                Console.Write(drink.Name + "\n");
                counter++;
            }
            Console.WriteLine("\nChoose your preferred drink from [1] to [" + Game.GameInstance.GetRealisticWorld().drinks.Length + "]");
            FinancialStatus();
            int selectDrink = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            Drink( Game.GameInstance.GetRealisticWorld().drinks[selectDrink-1]);
        }

        private void Drink(Drink drink) 
        {
            if (money < drink.Cost)
            {
                Console.WriteLine(name + " does not have enough money to buy that drink.");
                InputForWork();
            }
            else
            {
                Game.GameInstance.GetRealisticWorld().CurrentTime +=drink.Time;
                energy += drink.Energy;
                happiness += drink.Taste;
                money -= drink.Cost;
                health -= drink.Healthiness;
                weight += drink.Healthiness;
                Console.WriteLine(name + " drank " + drink.Name);
                FinancialStatus();
                HealthStatus();
                InputForExercise();
            }
        }

        public void Rest()
        {
            Game.GameInstance.GetRealisticWorld().CurrentTime += 1;
            energy += 3;
            happiness ++;
            Console.WriteLine(name + " has rested and is feeling better!");
            HealthStatus();
            InputForExercise();
        }

        public void SearchAndEat()
        {
            Console.WriteLine("Search for a food item:");
            FinancialStatus();
            string search = Console.ReadLine();
            search = search.ToLower();
            char[] slicedSearchWord = search.ToCharArray();
            foreach(Food food in Game.GameInstance.GetRealisticWorld().foods)
            {
                food.Name = food.Name.ToLower();
                if(food.Name == search)
                {
                    Console.WriteLine(food.Name + " was found!");
                    Eat( food, 0);
                    return;
                }
                char[] slicedFoodWord = food.Name.ToCharArray();
                for(int h = 0; h < slicedSearchWord.Length; h++)
                {

                    bool searchWordContainsCharacter = slicedFoodWord.Contains(slicedSearchWord[h]);
                    if(searchWordContainsCharacter)
                    {   
                        float comparisonValue = 0.0f;
                        comparisonValue += 1/(float)slicedSearchWord.Length;
                        if(comparisonValue > 0.8f)
                        {
                            Console.WriteLine("Did you mean " + food.Name + "?");
                            Eat( food, 0);
                            Game.GameInstance.GetRealisticWorld().ChooseMethod();
                            return;
                        }
                    }
                } 
            }
        }

        private void Eat(Food food, int cookFood) 
        {
            if(money < food.Cost)
            {
                Console.WriteLine(name + " does not have enough money to buy that food item.");
                InputForWork();
            }
            else
            {
                if(cookFood == 1)
                {
                    EatFood(food);
                    Console.WriteLine(name + " has cooked " + food.Name + " and ate it.");
                    FinancialStatus();
                    HealthStatus();
                    GainWeightByFood(food);
                }
                else
                {
                    EatFood(food);
                    Console.WriteLine(name + " has bought " + food.Name + " and ate it.");
                    FinancialStatus();
                    HealthStatus();
                    GainWeightByFood(food);
                }
            }
        }

        private void EatFood(Food food)
        {
            Game.GameInstance.GetRealisticWorld().CurrentTime += food.Time;
            energy += food.Energy;
            happiness += food.Taste;
            weight += food.Healthiness;
            money -= food.Cost;
            health -= food.Healthiness;
        }

        private void GainWeightByFood(Food food)
        {
            if (food.Healthiness > 0)
            {
                Console.WriteLine("\n" + name + " has gained some pounds!");
                InputForExercise();
            }
            else
            {
                Game.GameInstance.GetRealisticWorld().ChooseMethod();
            }
        }

        private int InputForDecideForEntertainment()
        {
            Console.WriteLine("\nPress [1] to watch a movie\nPress [2] to hang out with a friend\nPress [3] to do nothing");
            FinancialStatus();
            HealthStatus();
            int choiceEntertainment = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value)-1;
            return choiceEntertainment;
        }

        public void DecideForEntertainment()
        {
            int choiceEntertainment = InputForDecideForEntertainment();
            if(choiceEntertainment == 2)
            {
                Console.WriteLine("\nBro can you spend your time more productively?");
                Game.GameInstance.GetRealisticWorld().ChooseMethod();
            }
            else
            {
                Entertain(Game.GameInstance.Deserialize("entertainments", Game.GameInstance.GetRealisticWorld().entertainments)[choiceEntertainment]);
            }
        }

        private void Entertain(Entertainment entertainment) 
        {
            if (money < entertainment.Cost)
            {
                Console.WriteLine(name + " does not have enough money to do this activity");
                InputForWork();
            }
            else
            {
                Game.GameInstance.GetRealisticWorld().CurrentTime += entertainment.Time;
                happiness += entertainment.Happiness;
                money -= entertainment.Cost;
                Console.WriteLine(name + " had quite some fun!\nThis cost " + entertainment.Cost);
                FinancialStatus();
                HealthStatus();
                Game.GameInstance.GetRealisticWorld().ChooseMethod();
            }
        }

        private int InputForActivityWithPet()
        {
            Console.WriteLine("\nPress [1] to feed one of your pets\nPress [2] to feed all your pets\nPress [3] to play with your pet/s\n");
            FinancialStatus();
            int choiceActivityPet = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            return choiceActivityPet;
        }

        public void ActivityWithPet()
        {
            int choiceActivityPet = InputForActivityWithPet();
            switch(choiceActivityPet)
            {
                case 1: 
                    FeedPet(SelectPet(), SelectPetFood());
                    Game.GameInstance.GetRealisticWorld().ChooseMethod();
                    break;
                case 2: 
                    FeedAllPets(SelectPetFood());
                    break;
                case 3:
                    PlayWithPet(SelectPet());
                    break;
            }
        }

        private void FeedPet(Pet pet, Food food)
        {
            if (money < food.Cost)
            {
                Console.WriteLine(name + " does not have enough money to buy food for " + pet.Name);
                InputForWork();
            }
            else
            {
                Game.GameInstance.GetRealisticWorld().CurrentTime += food.Time;
                pet.Energy += food.Energy;
                pet.Weight += food.Healthiness;
                pet.Happiness += food.Taste;
                money -= food.Cost;
                Console.WriteLine(name + " fed " + pet.Name + "\n" + name + " pet is a lot happier and rounder!");
                FinancialStatus();
                pet.Describe();
            }
        }

        private void FeedAllPets(Food food)
        {
            foreach (Pet pet in pets)
            {
                FeedPet(pet, food);
            }
            Game.GameInstance.GetRealisticWorld().ChooseMethod();
        }

        private void PlayWithPet(Pet pet)
        {
            Game.GameInstance.GetRealisticWorld().CurrentTime += 1;
            happiness += pet.Energy;
            pet.Energy -= happiness/4;
            HealthStatus();
            pet.Describe();
            Game.GameInstance.GetRealisticWorld().ChooseMethod();
        }

        private Pet SelectPet()
        {
            int counter = 1;
            foreach(Pet pet in pets)
            {
                Console.WriteLine("\n[" + counter + "]  " + pet.Name);
                counter++;
            }
            Console.WriteLine("\nSelect one of the pets from [1] to [" + pets.Count + "]\n");
            int selectPet = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value)-1;
            pets[selectPet].Describe();
            return pets[selectPet];
        }

        private PetFood SelectPetFood()
        {
            Game.GameInstance.GetRealisticWorld().petFoods = Game.GameInstance.Deserialize("petFoods", Game.GameInstance.GetRealisticWorld().petFoods);
            int counter = 1;
            foreach(Food petFood in Game.GameInstance.GetRealisticWorld().petFoods)
            {
                Console.Write("\n[" + counter + "]  " + petFood.Cost + "$  ");
                Console.Write(petFood.Name + "\n");
                counter++;
            }
            Console.WriteLine("\nSelect one of the pet food items from [1] to [" + Game.GameInstance.GetRealisticWorld().petFoods.Length + "]\n");
            int selectPetFood = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value)-1;
            FinancialStatus();
            return Game.GameInstance.GetRealisticWorld().petFoods[selectPetFood];
        }

        private int InputForDecideForSexChange()
        {
            Console.WriteLine("\nPress [1] to change your sex (Cost 1000$)\nPress [2] to save your money");
            FinancialStatus();
            int choiceSex = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            return choiceSex;
        }

        private void DecideForSexChange()
        {
            int choiceSex = InputForDecideForSexChange();
            if(choiceSex == 1)
            {
                SexChange();
            }
        }

        private void SexChange()
        {
            switch (gender)
            {
                case "male": gender = "female";
                break;
                case "female": gender = "male";
                break;
            }

            if (money < 1000)
            {
                Console.WriteLine(name + " does not have enough money to change to the preferred sex.");
                InputForWork();
            }
            else
            {
                money -= 1000;
                Console.WriteLine("The surgery was succesful. " + name + " has officially moved to the other side. (gender wise)");
                Game.GameInstance.GetHuman().ToString();
                Game.GameInstance.GetRealisticWorld().ChooseMethod();
            }
        }

        public void InputForExercise()
        {
            Console.WriteLine("\nPress [1] to go jogging\nPress [2] to go cycling\nPress [3] to go to the Gym\nPress [4] to not exercise\n");
            HealthStatus();
            int choiceExercise = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value)-1;
            if(choiceExercise == 3)
            {
                DecideForEntertainment();
            }
            Console.WriteLine("You went " + Game.GameInstance.Deserialize("exercises", Game.GameInstance.GetRealisticWorld().exercises)[choiceExercise].Name);
            Exercise(Game.GameInstance.GetRealisticWorld().exercises[choiceExercise]);
        }

        private void Exercise(Exercise exercise)
        {
            if (energy <= exercise.Energy)
            {
                Console.WriteLine(name + " does not have enough energy to exercise.");
                RegenerateEnergy();
            }
            else
            {
                Game.GameInstance.GetRealisticWorld().CurrentTime += exercise.Time;
                energy -= exercise.Energy;
                fitness += exercise.Fitness;
                happiness += exercise.Dopamine;
                health += exercise.Fitness;
                HealthStatus();
                Game.GameInstance.GetRealisticWorld().ChooseMethod();
            }
        }

        public void InputForWork()
        {
            Console.WriteLine("\nDo you want to go work?\n\nPress [1] to work in the office (Competence requirement 5)\nPress [2] to work in McDonalds (Competence requirement 1)\nPress [3] to work for a delivery service (Competence requirement 2)\nPress [4] to not work");
            FinancialStatus();
            CompetenceStatus();
            int choiceWork = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value)-1;
            if(choiceWork == 3)
            {
                DecideForEntertainment();
            }
            else
            {
                Work(Game.GameInstance.Deserialize("works", Game.GameInstance.GetRealisticWorld().works)[choiceWork]);
            }
        }

        private void Work(Work work) 
        {
            if (competence < work.Requirement)
            {
                Console.WriteLine(name + " does not meet the requirements for the " + work.Name + " job.");
                DecideForCompetence();
            }
            else if (energy <= work.Energy)
            {
                Console.WriteLine(name + " does not have enough energy to work.");
                SelectAndEat();
            }
            else
            {
                Game.GameInstance.GetRealisticWorld().CurrentTime += work.Time;
                energy -= work.Energy;
                happiness -= work.Stress;
                money += work.Income;
                Console.WriteLine("The hard work has been rewarded with " + work.Income + "$");
                FinancialStatus();
                Game.GameInstance.GetRealisticWorld().CompleteTask(2);
                if(energy <= 5)
                {
                    RegenerateEnergy();
                }
                Game.GameInstance.GetRealisticWorld().ChooseMethod();
            }
        }

        public void Employment(string path, Human human, Work work, List<Human> humans)
        {
            if(work.Count > 5)
            {
                Console.WriteLine(name + " seems to like this job. Do you want to apply for this job for proper employment?\nPress [1] to apply for the job\nPress [2] to stay a freelancer");
                int choiceEmployment = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value) - 1;
                if(choiceEmployment == 1)
                {
                    ApplyForJob(work);
                }

            }
            else
            {
                Console.WriteLine(name + " does not seem to have enough experience");
                Game.GameInstance.GetRealisticWorld().ChooseMethod();
            }
        }

        public void ApplyForJob(Work work)
        {
            if(competence >= work.Requirement)
            {
                Console.WriteLine("Congratulations! " + name + " got accepted!");
                WorkRegularily(work);
            }
            else
            {
                Console.WriteLine("It seems like " + name + " did not meet their requirements.");
                Game.GameInstance.GetRealisticWorld().ChooseMethod();
            }
        }

        public void WorkRegularily(Work work)
        {
            Console.WriteLine("Do you want " + name + " to go to work regularily from now on?\nPress [1] for Yes\nPress [2] for No");
            int choiceWorkRegular = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            if(choiceWorkRegular == 1)
            {
                if(Game.GameInstance.GetRealisticWorld().CurrentTime >= 24)
                {
                    Work(work);
                }
            }
            else
            {
                Game.GameInstance.GetRealisticWorld().ChooseMethod();
            }
        }
        
        private int InputForDecideForCompetence()
        {
            Console.WriteLine("\nDo you want to improve your current skills?\n\nPress [1] for Yes\nPress [2] for No");
            FinancialStatus();
            CompetenceStatus();
            int choiceSkill = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            return choiceSkill;
        }

        public void DecideForCompetence()
        {
            int choiceSkill = InputForDecideForCompetence();
            if(choiceSkill == 1)
            {
                ImproveCompetence();
            }
            else
            {
                InputForWork();
            }
        }

        private void ImproveCompetence()
        {
            if (regularCounter == 0 && money < 50)
            {
                Console.WriteLine(name + " does not have enough money to improve his/her skills. (Cost 50$)");
                Console.WriteLine(name + " should go work at mcdonalds if one wants to earn money with no requirements.");
                InputForWork();
            }
            else
            {
                Game.GameInstance.GetRealisticWorld().CurrentTime += 6;
                if(regularCounter == 0)
                {
                    money -= 50;
                    Console.WriteLine(name + " has paid 50$ to spend some time to improve his/her skills");
                }
                regularCounter ++;
                if(regularCounter == 5)
                {
                    Console.WriteLine(name + " has improved his/her expertise.");
                    competence ++;
                    regularCounter = 0;
                }
                else
                {
                    Console.WriteLine(name + " still needs to go " + (5-regularCounter) + " times to improve his/her expertise");
                }
                energy -= 5;
                happiness -= 5;
                InputForWork();
            }
        }

        private int InputForAdoptPet()
        {
            Console.WriteLine("Do you want to adopt a pet?\n\nPress [1] for Yes\nPress [2] for No");
            int choicePet = int.Parse(Regex.Match(Console.ReadLine(),@"\d").Value);
            return choicePet;
        }

        private int InputForDecidePet()
        {
            Console.WriteLine("You can choose between a cat and a dog\n\nPress [1] for Cat\nPress [2] for Dog");
            int choiceCatDog = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            return choiceCatDog;
        }

        private void DisplayCatAscii()
        {
           Console.WriteLine(
            "\n   A_A" +
            "\n  (-.-)" +
            "\n   |-|" +
            "\n  /   \\ " +
            "\n |     |  __" +
            "\n |  || | |  \\___" +
            "\n \\_||_/_/"); 
        }

        private void DisplayDogAscii()
        {
            Console.WriteLine(
            "\n                     ." +
            "\n                    / V\\" +
            "\n                  / `  /" +
            "\n                 <<   |" +
            "\n                 /    |" +
            "\n               /      |" +
            "\n             /        |" +
            "\n           /    \\  \\ /" +
            "\n          (      ) | |" +
            "\n  ________|   _/_  | |" +
            "\n<__________\\______)\\__)");
        }

        private void AddCatToHuman()
        {
            Cat cat = new Cat("", random.Next(1,10), random.Next(1,10), random.Next(1,3), random.Next(30,50), random.Next(5,10), 10, Game.GameInstance.Deserialize("genderList", Game.GameInstance.GetRealisticWorld().genderList)[random.Next(0, Game.GameInstance.GetRealisticWorld().genderList.Length)], random.Next(1,10), 10, Game.GameInstance.GetHuman(), 10);
            Game.GameInstance.GetHuman().pets.Add(cat);
            NamePet(cat);
            //Game.GameInstance.realisticWorld.CompleteTask(1);
            //RealisticWorld.CompleteTask(4);
            DisplayCatAscii();
        }

        private void AddDogToHuman()
        {
            Dog dog = new Dog ("", random.Next(5,10), random.Next(1,10), random.Next(1,3), random.Next(30,100), random.Next(5,30), 10, Game.GameInstance.Deserialize("genderList", Game.GameInstance.GetRealisticWorld().genderList)[random.Next(0, Game.GameInstance.GetRealisticWorld().genderList.Length)], random.Next(1,10), 10, Game.GameInstance.GetHuman(), 10);
            Game.GameInstance.GetHuman().pets.Add(dog);
            NamePet(dog);
            // RealisticWorld.CompleteTask(4);
            // RealisticWorld.CompleteTask(1);
            DisplayDogAscii();
        }

        public void AdoptPet()
        {
            Game.GameInstance.GetRealisticWorld().CurrentTime += 2;
            int choicePet = InputForAdoptPet();
            if(choicePet == 1)
            {
                KarmaKonto.GainKarma(1);
                int choiceCatDog = InputForDecidePet();
                if(choiceCatDog == 1)
                {
                    AddCatToHuman();
                }
                if(choiceCatDog == 2)
                {
                    AddDogToHuman();
                }
            }
            else
            {
                int adoptionCounter = 0;
                KarmaKonto.LoseKarmaPoints(1);
                if(adoptionCounter == 0)
                {
                    Console.WriteLine("Are you sure? I will ask you again.");
                    adoptionCounter++;
                    AdoptPet();
                    return;
                }
                if(adoptionCounter == 1)
                {
                    Console.WriteLine("You heartless bastard. I will ask one more time!");
                    adoptionCounter++;
                    AdoptPet();
                    return;
                }                    
                if(adoptionCounter == 2)
                {
                    Console.WriteLine("Okay, can't force you, but I hope nothing bad happens on your way home...");
                    // TODO: ADD SOMETHING 
                    adoptionCounter = 0;
                }
            }
            InputForWork();
        }

        private void NamePet(Pet petToName)
        {
            Console.WriteLine("Now that you adopted a pet, give it a name");
            petToName.Name = Console.ReadLine();
            Console.WriteLine(petToName.Name + " is happy to be part of your family!");
        }

        public void RescuePet(Pet petToRescue)
        {
            Game.GameInstance.GetRealisticWorld().CurrentTime += 2;
            Game.GameInstance.GetHuman().KarmaKonto.GainKarma(5);
            float randomChanceForOwning = random.Next(0, 100)/100.0f;
            if(randomChanceForOwning < 0.5f)
            {
                petToRescue.Owner = Game.GameInstance.GetRealisticWorld().currentPlayedHuman;
                Console.WriteLine("\n" + Game.GameInstance.GetHuman().Name + " has rescued an animal from a tree and adopted it.");
                Game.GameInstance.GetHuman().pets.Add(petToRescue);
                NamePet(petToRescue);
                Game.GameInstance.GetHuman().PrintPetList();
            }
            else
            {
                petToRescue.Owner = Game.GameInstance.Deserialize("owners", Game.GameInstance.GetRealisticWorld().owners)[random.Next(0,4)];
                Console.WriteLine(Game.GameInstance.GetHuman().Name + " has rescued an animal but it already has an owner called: \"" + petToRescue.Owner.Name + "\"");
                Console.WriteLine(petToRescue.Owner.Name + " is so grateful and invites you to a coffee!");
                Console.WriteLine("Slurrrrp! mhhhhhaaaaa...");
                Game.GameInstance.GetHuman().Drink(Game.GameInstance.GetRealisticWorld().freeCoffee);
            }
        }

        private int InputForLoan()
        {
            Console.WriteLine("Do you want to take a loan?\nPress [1] to take a Loan\nPress [2] to pay up the debt\nPress [3] to do none of those");
            int choiceLoan = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            FinancialStatus();
            return choiceLoan;
        }

        public void GetALoan()
        {
            int choiceLoan = InputForLoan();
            switch(choiceLoan)
            {
                case 1:
                    Console.WriteLine("\nType in the amount you need. (max. 999$)");
                    loanAmount = int.Parse(Regex.Match(Console.ReadLine(), @"\d{1,3}").Value);
                    money += (int)loanAmount;
                    Game.GameInstance.GetRealisticWorld().CurrentTime += 3;
                    DebtCounter(loanAmount);
                    FinancialStatus();
                    Game.GameInstance.GetRealisticWorld().ChooseMethod();
                    break;
                case 2:
                    if(money < (int)debt.DebtAmount)
                    {
                        Console.WriteLine(name + " does not have enough money to pay up.");
                        Game.GameInstance.GetRealisticWorld().ChooseMethod();
                    } 
                    else
                    {
                        money -= (int)debt.DebtAmount;
                        debt.DebtAmount -= debt.DebtAmount;
                        Console.WriteLine("You successfully paid the debt of " + debt.DebtAmount + ".00$\nYou are now debt free!");
                        FinancialStatus();
                        Game.GameInstance.GetRealisticWorld().ChooseMethod();
                    }
                    break;
                case 3:
                    Game.GameInstance.GetRealisticWorld().ChooseMethod();
                    break;
            }
        }

        public void DebtCounter(float loanAmount)
        {
            if(debt.DebtAmount == 0)
            {
                debt.TempDay = Game.GameInstance.GetRealisticWorld().Day;
                Console.WriteLine(debt.TempDay);
                float interest = loanAmount /= 10;
                loanAmount *= 10;
                for(int i = debt.TempDay; i <= Game.GameInstance.GetRealisticWorld().Day; i++)
                {
                    debt.DebtAmount = (loanAmount + interest);
                }
            }
            else 
            {
                Console.WriteLine("AFTER= " + debt.TempDay);
                float interest = loanAmount /= 10;
                loanAmount *= 10;
                for(int i = debt.TempDay ; debt.TempDay <= Game.GameInstance.GetRealisticWorld().Day; debt.TempDay++)
                {
                    debt.DebtAmount += interest;
                }
            }
        }

        public void GetSick()
        {
            if(health <= 0)
            {
                Console.WriteLine(name + " has gotten sick! " + name + " can go to the doctor to get treated.\n\nPress [1] to get treated\nPress [2] to get healthy naturally (a lot harder)");
                int choiceDoctor = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
                happiness = 0;
                energy = 0;
                HealthStatus();
                GoToDoctor(choiceDoctor);
            }
        }

        public void GoToDoctor(int choiceDoctor)
        {
            if(choiceDoctor == 1)
            {
                if(money < 50)
                {
                    Console.WriteLine("You do not have enough money to get treated by a doctor!");
                }
                else
                {
                    Console.WriteLine("A peculiar glowing fluid was injected into your arm! (This doctor seems suspicious, but you are feeling much better!");
                    happiness = 10;
                    energy = 10;
                    health = 10;
                    HealthStatus();
                    Game.GameInstance.GetRealisticWorld().ChooseMethod();
                }
            }
            else
            {
                Console.WriteLine("Well get well soon!");
            }
        }

        public void ChronicSuffering()
        {
            
        }

        public void GetHungry()
        {
            for(int i = 5; i <= Game.GameInstance.GetRealisticWorld().CurrentTime; i += 5)
            {
                energy -= 1;
            }
        }

        public void PetStatus()
        {
            Console.WriteLine("\n" + name + "'s pets:");
            foreach(Pet p in Game.GameInstance.GetHuman().pets)
            {
                Console.WriteLine(p.Name);
                Console.Write("\nEnergy:     ");
                for(int i = 0; i < p.Energy; i++)
                {
                    Console.Write("█");
                }
                for(int i = p.Energy; i < 30; i++)
                {
                    Console.Write("◆");
                }
                Console.Write("\nHealth:     ");
                for(int i = 0; i < p.Health; i++)
                {
                    Console.Write("█");
                }
                for(int i = p.Health; i < 30; i++)
                {
                    Console.Write("◆");
                }
                Console.Write("\nHappiness:  ");
                for(int i = 0; i < p.Happiness; i++)
                {
                    Console.Write("█");
                }
                for(int i = p.Happiness; i < 30; i++)
                {
                    Console.Write("◆");
                }
                Console.WriteLine("\n\nWeight:     " + p.Weight + "kg\n");
            }
        } 

        public void AllStatus()
        {
            FinancialStatus();
            HealthStatus();
            CompetenceStatus();
        }

        public void HealthStatus()
        {
            Console.Write("\nEnergy:     ");
            for(int i = 0; i < energy; i++)
            {
                Console.Write("█");
            }
            for(int i = energy; i < 30; i++)
            {
                Console.Write("◆");
            }
            Console.Write("\nHealth:     ");
            for(int i = 0; i < health; i++)
            {
                Console.Write("█");
            }
            for(int i = health; i < 30; i++)
            {
                Console.Write("◆");
            }
            Console.Write("\nHappiness:  ");
            for(int i = 0; i < happiness; i++)
            {
                Console.Write("█");
            }
            for(int i = happiness; i < 30; i++)
            {
                Console.Write("◆");
            }
            Console.WriteLine("");
            Console.WriteLine("\nWeight:     " + weight + "kg");
            Console.Write("Fitness:    ");
            for(int i = 0; i < fitness; i++)
            {
                Console.Write("█");
            }
            for(int i = fitness; i < 30; i++)
            {
                Console.Write("◆");
            }
            Console.WriteLine("");
        }

        public void FinancialStatus()
        {
            Console.WriteLine("\nMoney: " + money + ".00$");
            Console.WriteLine("Debt: " + debt.DebtAmount + "$");
        }

        public void CompetenceStatus()
        {
            Console.Write("\nCompetence: ");
            for(int i = 0; i < competence; i++)
            {
                Console.Write("█");
            }
            for(int i = competence; i < 30; i++)
            {
                Console.Write("◆");
            }
            Console.WriteLine("");
        }
    }
}