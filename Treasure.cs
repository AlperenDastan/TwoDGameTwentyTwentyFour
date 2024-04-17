using System;
using System.Collections.Generic;

namespace TwoDGameTwentyTwentyFour
{
    public class Treasure
    {
        public List<Weapon> WeaponItems { get; set; }
        public List<Armour> ArmourItems { get; set; }
        public List<Potions> Pots { get; set; }
        public bool IsLootable { get; set; }

        public Treasure()
        {
            WeaponItems = new List<Weapon>();
            ArmourItems = new List<Armour>();
            Pots = new List<Potions>();
            IsLootable = true; // Since treasures aren't locked because we don't have any lockpick feature in the game, all treasures are lootable by the ''player'' by default. 
            InitializeContents();
        }

        private void InitializeContents()
        {
            Logger.Instance.Log("Creating Treasure with random items.");

            // Our random number generator.
            Random rnd = new Random();

            // Randomly decides the number of weapon, armour, and potion items to spawn in the treasure.
            int weaponItemscount = rnd.Next(0, 3); // Number of weapons
            int armourItemscount = rnd.Next(0, 3); // Number of armour
            int potsCount = rnd.Next(0, 3); // Number of potions

            // Populates treasures with weapons.
            for (int i = 0; i < weaponItemscount; i++)
            {
                // adds a new weapon to the treasure and into the weapons list with increasing damage and a fixed range on weapon.
                WeaponItems.Add(GameObjectFactory.CreateWeapon($"Crowbar{i + 1}", 10 * (i + 1), 1));
            }

            // Populates treasures with armour
            for (int i = 0; i < armourItemscount; i++)
            {
                // adds a new armour to the treasure and into the armour list with increasing damage reduction.
                ArmourItems.Add(GameObjectFactory.CreateArmour($"HEV Suit{i + 1}", 5 * (i + 1)));
            }

            // Populates treasures with potions
            for (int i = 0; i < potsCount; i++)
            {
                // adds a new health potion to the treasure and into the potions list with how much health it restores.
                Pots.Add(GameObjectFactory.CreatePotion($"Health Potion{i + 1}", 20 * (i + 1)));
            }

           
        }

        // A method to check if the treasure is empty.
        public bool IsEmpty()
        {
            return !WeaponItems.Any() && !ArmourItems.Any() && !Pots.Any();
        }
    }
}
