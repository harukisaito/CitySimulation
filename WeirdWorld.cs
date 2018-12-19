using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CitySimulation
{
    class WeirdWorld : Universe
    {
        [JsonIgnore]
        public Food[] pizzaFoods = new Food[]
        {
        };
        [JsonIgnore]
        public Drink[] pizzaDrinks = new Drink[]
        {
        };
        [JsonIgnore]
        public Work[] works = new Work[]
        {
        };
        [JsonIgnore]
        public Food[] foods = new Food[]
        {
        };
        [JsonIgnore]
        public string[] nameList = new string[]
        {
        }; 
        [JsonIgnore]
        public string[] genderList = new string[]
        {
        }; 

        public List<Pizza> pizzas = new List<Pizza>();
        public static Game game = Game.Instance();

        public WeirdWorld(int id, string name) : base(id, name)
        {
        }

        private void DisplayPizzaAscii()
        {
            Console.WriteLine(
            "\n          _....._" +
            "\n       _.:`.--|--.`:._" +
            "\n     .: .'|o  | o /'. '." +
            "\n    // '.  | o|  /  o '.| " +
            "\n   /|'._o'. | |o/ o_.-'o\\" +
            "\n   || o '-.'.||/.-' o   ||" +
            "\n   ||--o--o-->|<o-----o-||" +
            "\n   \\  o _.-'/||'-._o  o//" +
            "\n    \\.-'  o/ |o| o '-.//" +
            "\n     '.'.o / o|  | o.'.'" +
            "\n       `-:/.__|__o|:-'" +
            "\n          `'--=--'`");
        }

        private void CreatePizza()
        {
            bool empty = Game.GameInstance.CheckJson("pizzas");
            if(empty == false)
            {
            //     Deserialize();
            }
            pizzas.Add(new Pizza(1, 1, ChoosePepperoni(), ChooseMushroom(), ChooseHam(), ChoosePineapple()));
            // Serialize(pizzas);
        }
        public void Introduction()
        {
            Console.WriteLine("\nStart the Simulation inside a Simulation inside a simulation...");
            Console.WriteLine("\nLet's create your first own pizza!");
            CreatePizza();
            DisplayPizzaAscii();
            int idchoice = Game.GameInstance.ChooseFromList<Pizza>("pizzas", pizzas);
            Console.WriteLine("\nNow that you created your first pizza, adopt a human.");
            pizzas[idchoice].AdoptingHuman(pizzas[idchoice], pizzas);
        }

        public void Continue()
        {
            Console.WriteLine("\nStart the Simulation inside a Simulation inside a simulation...");
            Console.WriteLine("\nPress [1] to create a new pizza\nPress [2] to play with your exisiting pizzas");
            int idchoice = Game.GameInstance.ChooseFromList<Pizza>("pizzas", pizzas);
        }


        private bool ChoosePepperoni()
        {
            Console.WriteLine("Would you like some pepperonies on your pizza?\n\nPress [1] for Yes\nPress [2] for No");
            int choicePepperoni = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            if(choicePepperoni == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ChooseMushroom()
        {
            Console.WriteLine("\nWould you like some mushrooms on your pizza?\n\nPress [1] for Yes\nPress [2] for No");
            int choiceMushroom = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            if(choiceMushroom == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ChooseHam()
        {
            Console.WriteLine("\nWould you like some ham on your pizza?\n\nPress [1] for Yes\nPress [2] for No");
            int choiceHam = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            if(choiceHam == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ChoosePineapple()
        {
            Console.WriteLine("\nWould you like some pineapple on your pizza?\n\nPress [1] for Yes (why though)\nPress [2] for No (like any sane person)");
            int choicePineapple = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            if(choicePineapple == 1)
            {
                Console.WriteLine("Ugh, so you are one of those people.");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ChooseMethod(Pizza pizza, List<Pizza> pizzas)
        {
            int choiceMethod;
            int luck;
            // Serialize(pizzas);
            Console.WriteLine("\nWhat do you want to do next?\nPress [1] to eat something\nPress [2] to Drink something\nPress [3] to rest\nPress [4] to go work\nPress [5] to lose some pounds\nPress [6] to do something entertaining\nPress [7] to either feed your human or play with it\nPress [8] adpot another Human\nPress [9] to quit");
            choiceMethod = int.Parse(Regex.Match(Console.ReadLine(), @"\d").Value);
            switch(choiceMethod)
            {
                case 1: pizza.SelectAndEatFood(pizza, pizzas);
                break;
                case 2: pizza.SelectAndDrink(pizza, pizzas);
                break;
                case 3: pizza.Rest(pizza, pizzas);
                break;
                case 4: luck = random.Next(0,100);
                        if(luck > 80)
                        {
                            // Deserialize();
                            pizza.RescueHuman(new Human(Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.nameList[random.Next(0,9)], 0, 1, 30, 5, 10, Game.GameInstance.currentPlayedUniverse.currentPlayedWeirdWorld.genderList[random.Next(0,1)], 10, 10, new KarmaKonto(0), 0, 10), pizza);
                        } 
                        pizza.InputWork(pizza, pizzas);

                break;
                case 5: pizza.LosePounds(pizza, pizzas);
                break;
                case 6: pizza.Entertainment(pizza, pizzas);
                break;
                case 7: pizza.ActivityWithHuman(pizza, pizzas);
                break;
                case 8: pizza.AdoptingHuman(pizza, pizzas);
                break;
                case 9: game.Quit();
                break;
            }
        }
    }
}
