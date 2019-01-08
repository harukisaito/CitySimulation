
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CitySimulation
{
    class Robot
    {
        //PRETTY MUCH LIKE A MEESEEKS NEED A PURPOSE FALL INTO DEPRESSION
        // SOLVE MATH PROBLEMS FOR WORK
        //EATING robot GIVES THEM SUPERPOWERS SOLVING EVERY PROBLEM WITHOUT A PROBLEM
        // HOW DO THEY PROCESS IT? NO ONE KNOWS!
        // ILLEGAL SALT EXTRACTION FROM THE SEAS
        private string id;
        private int oil;
        private int copper;
        private int aluminium;
        private int steel;
        private int salt;
        private int circuitHealth;
        private int competence;

        private int regularCounter;

        private Random random = new Random();
        public List<Alien> aliens = new List<Alien>();
        public Alien currentAlien;

        public string Id
        {
            get {return id;}
            set {id = value;}
        }
        public int Oil
        {
            get {return oil;}
            set {oil = value;}
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

        public int CircuitHealth
        {
            get {return circuitHealth;}
            set {circuitHealth = value;}
        }

        public int Competence
        {
            get {return competence;}
            set {competence = value;}
        }

        public Robot(string id, int oil, int copper, int aluminium, int steel, int salt, int circuitHealth, int competence)
        {
            this.id = id;
            this.oil = oil;
            this.copper = copper;
            this.aluminium = aluminium;
            this.steel = steel;
            this.salt = salt;
            this.circuitHealth = circuitHealth;
            this.competence = competence;
        }

        private void DisplayAlienAscii()
        {
            Console.WriteLine(
            "     o   o\n" +
            "      )-(\n" +
            "     (O O)\n" +
            "      \\=/\n" +
            "     .- -.\n" +
            "     / \\/\\\n" +
            "  _ / /\\ \\_\n" +
            " =./ {,-.}\\.=\n" +
            "     || ||\n" +
            "     || ||\n" +
            "   __|| ||__\n" +
            "  `---   ---'\n");
        }

        public void AdoptingAlien()
        {
            Console.WriteLine("You found a abandoned small alien on the street. Do you want to take it in?\n\nPress [1] for Yes\nPress [2] for No");
            int choice = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            if(choice == 1)
            {
                Console.WriteLine("You should decide more responsibly considering that you are handicapped...");
                currentAlien = new Alien(NameAlien(), Guid.NewGuid().ToString(), 3, 150, 50, 10, "", 10, 2, 3, SuperPowerRoulette());
                DisplayAlienAscii();
            }
            else if (choice == 2)
            {
                Console.WriteLine("Well maybe next time");
            }
            InputForWork();
        }

        private string NameAlien()
        {
            Console.WriteLine("Give your alien a name!");
            return Console.ReadLine();
        }

        private bool SuperPowerRoulette()
        {
            Random random = new Random();
            if(random.Next(0, 100) == 1)
            {
                Console.WriteLine("Your alien has superpowers and can help you tremendously");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void InputForWork()
        {
            Console.WriteLine("\nDo you want to go work?\n\nPress [1] to work in the office (Competence requirement 5)\nPress [2] to work in McDonalds (Competence requirement 0)\nPress [3] to work for a delivery service (Competence requirement 2)\nPress [4] to not work");
            CompetenceStatus();
            int choiceWork = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value)-1;
            if(choiceWork == 3)
            {
                Game.GameInstance.GetWeirdWorld().ChooseMethod();
            }
            else
            {
                Game.GameInstance.GetWorld().robotWorks = Game.GameInstance.Deserialize("robotWorks", Game.GameInstance.GetWorld().robotWorks);
                Work(Game.GameInstance.GetWorld().robotWorks[choiceWork]);
            }
        }       

        private void Work(RobotWork robotWork)
        {
            if (competence < robotWork.Requirement)
            {
                Console.WriteLine("Your robots abilites are lacking to do tasks of this level. Improve his problem solving?");
                DecideForCompetence();
            }
            else
            {
                int counter = 5;
                Console.WriteLine("Solve these Problems!");
                for(int i = counter; i >= 0; i = counter)
                {
                    int num1 = random.Next(0, robotWork.Difficulty), num2 = random.Next(0, robotWork.Difficulty);
                    Console.WriteLine(num1 + " + " + num2 + " = [TYPE IN THE ANSWER]"); 
                    int answer = int.Parse(Console.ReadLine());
                    if(answer == num1 + num2)
                    {
                        Console.WriteLine("Correct! Only " + counter + " tasks left");
                        counter --;
                        if(counter == 0)
                        {
                            Game.GameInstance.GetWeirdWorld().CurrentTime += robotWork.Time;
                            salt += robotWork.Income;
                            oil -= robotWork.Stress;
                            copper -= robotWork.Stress;
                            aluminium -= robotWork.Stress;

                            Game.GameInstance.GetWeirdWorld().CurrentTime += robotWork.Time;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect!");
                    }
                }
                FinancialStatus();
                Game.GameInstance.GetWeirdWorld().ChooseMethod();
            }
        }

        public void RegeneratingEnergy()
        {
            if(oil <= 2 || copper <= 2 || aluminium <= 2)
            {
                Console.WriteLine("You are about to break. How about consuming metal or tanking oil?");
                Console.WriteLine("\nPress [1] to eat some metal\nPress [2] to tank oil\n");
                switch(int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value))
                {
                    case 1: SelectAndEatFood();
                    break;
                    case 2: SelectAndDrinkOil();
                    break;
                }
            }
        }

        public void SelectAndEatFood()
        {
            RobotFood[] temp = Game.GameInstance.Deserialize("robotFoods", Game.GameInstance.GetWorld().robotFoods);
            DisplayFoodList(Game.GameInstance.Deserialize("robotFoods", Game.GameInstance.GetWorld().robotFoods));
        }

        private void DisplayFoodList(RobotFood[] robotFoods)
        {
            int counter = 1;
            foreach(RobotFood rf in robotFoods)
            {
                Console.WriteLine("\n[" + counter + "] " + rf.Name);
                Console.WriteLine("   Salt: " + rf.Salt);
                counter++;
            }
            Console.WriteLine("\nChoose your preferred food item from [1] to [" + robotFoods.Length + "]");
            FinancialStatus();
            Consume(robotFoods[int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value)-1]);
        }

        public void SelectAndDrinkOil()
        {
            DisplayOilList(Game.GameInstance.Deserialize("robotOils", Game.GameInstance.GetWorld().robotOils));
        }

        private void DisplayOilList(RobotOil[] robotOils)
        {
            int counter = 1;
            foreach(RobotOil ro in robotOils)
            {
                Console.WriteLine("\n[" + counter + "] " + ro.Name);
                Console.WriteLine("   Salt: " + ro.Salt);
                counter++;
            }
            Console.WriteLine("\nChoose your preferred oil from [1] to [" + robotOils.Length + "]");
            FinancialStatus();
            Tank(robotOils[int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value)-1]);
        }

        public void Clean()
        {
            // DO SOMETHING!!!
        }

        public void Rest()
        {
            Game.GameInstance.GetWeirdWorld().CurrentTime += 3;
            Console.WriteLine("You just wasted 3 hours of your day by staring at a wall. What did you think is going to happen if a robot 'rests'?");
            Game.GameInstance.GetWeirdWorld().ChooseMethod();
        }

        public void Entertainment()
        {
            Console.WriteLine("There is no such thing as entertainment for a robot other than working! Go work!!");
            InputForWork();
        }

        private int InputForDecideForCompetence()
        {
            Console.WriteLine("\nDo you want to improve your current skills?\n\nPress [1] for Yes\nPress [2] for No");
            FinancialStatus();
            CompetenceStatus();
            return int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
        }

        public void DecideForCompetence()
        {
            if(InputForDecideForCompetence() == 1)
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
            if (regularCounter == 0 && salt < 20)
            {
                Console.WriteLine(id + " does not have enough salt to improve his/her skills. (Cost 20 salts)");
                Console.WriteLine("Dumb robots should go work at mcdonalds if they want to earn salt with no requirements.");
                InputForWork();
            }
            else
            {
                Game.GameInstance.GetWeirdWorld().CurrentTime += 6;
                if(regularCounter == 0)
                {
                    salt -= 20;
                    Console.WriteLine(id + " has paid 50$ to spend some time to improve his/her skills");
                }
                regularCounter ++;
                if(regularCounter == 5)
                {
                    Console.WriteLine(id + " has improved his/her expertise.");
                    competence ++;
                    regularCounter = 0;
                }
                else
                {
                    Console.WriteLine(id + " still needs to go " + (5-regularCounter) + " times to improve his/her expertise");
                }
                oil -= 5;
                copper -= 2;
                InputForWork();
            }
        }
        
        public void Consume(RobotFood food)
        {
            salt -= food.Salt;
            copper += food.Copper;
            aluminium += food.Aluminium;
            steel += food.Steel;
            Game.GameInstance.GetWeirdWorld().CurrentTime += food.Time;
            Status();
            Game.GameInstance.GetWeirdWorld().ChooseMethod();
        }

        public void Tank(RobotOil robotOil)
        {
            oil += robotOil.Oil;
            salt -= robotOil.Salt;
            Game.GameInstance.GetWeirdWorld().CurrentTime += robotOil.Time;
            Status();
            Game.GameInstance.GetWeirdWorld().ChooseMethod();
        }

        public void FeedingAlien()
        {
            Console.WriteLine("Press [1] to feed one of your aliens\nPress [2] to feed all you aliens");
            if(int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value) == 1)
            {
                FeedAlien(SelectAlien(), DisplayAlienFoodList(Game.GameInstance.GetWorld().alienFoods));
            }
            else
            {
                FeedAllAliens(DisplayAlienFoodList(Game.GameInstance.GetWorld().alienFoods));
            }
        }

        private Alien SelectAlien()
        {
            if(aliens.Count != 0)
            {
                int counter = 1;
                foreach(Alien a in aliens)
                {
                    Console.WriteLine("[" + counter + "] " + a.Name);
                    counter ++;
                }
                return aliens[int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value)];
            }
            else
            {
                return currentAlien;
            }
        }

        private AlienFood DisplayAlienFoodList(AlienFood[] alienFoods)
        {
            int counter = 1;
            foreach(AlienFood af in alienFoods)
            {
                Console.WriteLine("[" + counter + "]" + af.Name);
                Console.WriteLine("Salt: " + af.Salt);
                counter ++;
            }
            Console.WriteLine("\nChoose your preferred food item from [1] to [" + alienFoods.Length + "]");
            return alienFoods[int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value)-1];
        }

        private void FeedAlien(Alien alien, AlienFood food)
        {
            salt -= food.Salt;
            alien.Energy += food.Energy;
            Game.GameInstance.GetWeirdWorld().CurrentTime += food.Time;
            Game.GameInstance.GetWeirdWorld().ChooseMethod();
        }

        private void FeedAllAliens(AlienFood food)
        {
            foreach(Alien a in aliens)
            {
                FeedAlien(a, food);
            }
        }

        private void FinancialStatus()
        {
            Console.WriteLine("\nSalt: " + salt + " salts");
        }

        private void Status()
        {
            Console.Write("\nCopper:     ");
            DisplayStats(copper);
            Console.Write("\nAluminum:   ");
            DisplayStats(aluminium);
            Console.Write("\nSteel:      ");
            DisplayStats(steel);
            Console.Write("\nOil:        ");
            DisplayStats(oil);
        }

        private void CompetenceStatus()
        {
            Console.Write("\nCompetence: ");
            DisplaySquare(competence);
            DisplayStar(competence);
            Console.WriteLine("");
        }

        public void AlienStatus()
        {
            Console.WriteLine("\n" + currentAlien.Name);
            Console.Write("\nEnergy:     ");
            DisplayStats(currentAlien.Energy);
            Console.Write("\nHappiness:  ");
            DisplayStats(currentAlien.Happiness);
            Console.Write("\nHealth:     ");
            DisplayStats(currentAlien.Health);
            Console.Write("\n\nSuper Powers: ");
            Console.Write(currentAlien.SuperPower + "\n");
        }

        public void AllStatus()
        {
            FinancialStatus();
            Status();
            CompetenceStatus();
        }

        private void DisplayStar(int value)
        {
            for(int i = value; i < 30; i++)
            {
                Console.Write("◆");
            }
        }

        private void DisplayStar()
        {
            for(int i = 0; i < 30; i++)
            {
                Console.Write("◆");
            }
        }

        private void DisplaySquare(int value)
        {
            for(int i = 0; i < value; i++)
            {
                Console.Write("█");
            }
        }

        private void IfStatement(int value)
        {
            if(value >= 0)
            {
                DisplayStar(value);
            }
            else
            {
                DisplayStar();
            }
        }

        private void DisplayStats(int value)
        {
            DisplaySquare(value);
            IfStatement(value);
        }
    }
}