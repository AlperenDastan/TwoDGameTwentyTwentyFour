using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TwoDGameTwentyTwentyFour
{
   
    // Introduces a Factory class to create game objects 
    public static class GameObjectFactory
    {
        // A method to create an entity in the game's world
        public static Entity CreateEntity(string name, int hitPoint, int maxHitPoints, int x, int y)
        {
            Logger.Log($"Creating entity: {name}");
            return new Entity(name, hitPoint, maxHitPoints, x, y);
        }

        // Method to create a weapon
        public static Weapon CreateWeapon(string name, int hitPoint, int range)
        {
            Logger.Log($"Creating Weapon: {name}");
            return new Weapon(name, hitPoint, range);
        }

        // Method to create armour
        public static Armour CreateArmour(string name, int reduceHitPoint)
        {
            Logger.Log($"Creating Armour: {name}");
            return new Armour(name, reduceHitPoint);
        }

        //Method to create a treasure
        public static Treasure CreateTreasure()
        {
            Treasure chest = new Treasure();
            // Assume treasure constructor handles randomizing the contents of the treasure.
            return chest;
        }

        // Method to create a Potion 
        public static Potions CreatePotion(string name, int healthRestore)
        {
            Logger.Log($"Creating potion...");
            return new Potions(name, healthRestore);
        }



    }
}
