using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDGameTwentyTwentyFour
{
    // An entity class which represents what kind of entities exist in the game's world 
    public class Entity
    {
        public string Name { get; set; }
        public int HitPoint { get; set; } // This displays the current healthpoint of the Entity
        public int MaxHitPoints { get; set; }   // This displays the maximum health points the entity can have
        public int X { get; set; } // The entity's position in the world - Same as Y (the one below)
        public int Y { get; set; }
        public List<Weapon> WeaponItems { get; set; } // A list of weapons that the entity has in its possession 
        public List<Armour> ArmourItems { get; set; } // A list of armour pieces that the entity has in its possession
        public List<Potions> Pots { get; private set; } // A list of potions that the entity has in its possession

        public Entity(string name, int hitPoint, int maxHitPoints, int x, int y)
        {
            Name = name;
            HitPoint = hitPoint;
            MaxHitPoints = maxHitPoints;
            X = x;
            Y = y;
            WeaponItems = new List<Weapon>();
            ArmourItems = new List<Armour>();
            Pots = new List<Potions>();
        }

        // This function is a helping function to determine whether or not the target is in range of the weapon. 
        private bool IsInRange(Entity target, Weapon weapon)
        {
            int distance = Math.Abs(X - target.X) + Math.Abs(Y - target.Y);
            return distance <= weapon.Range;
        }

        // Executes an attack with a chosen weapon
        public void Hit(Entity target)
        {
            // Checks if the entity actually has any weapons equipped, if it doesn't, it will instead do an unarmed hit, which has its own default dmg
            if (WeaponItems == null || WeaponItems.Count == 0)
            {
                UnarmedHit(target);
            }
            else
            {
                // It will choose the weapon with the highest damage, as that makes the most sense, since the different weapons don't have any special effects. Ensuring that you always have the highest dmg weapon equipped
                Weapon weapon = WeaponItems.OrderByDescending(w => w.Damage).FirstOrDefault();

                if (weapon != null && IsInRange(target, weapon))
                {
                    int damage = weapon.Damage;
                    target.ReceiveHit(damage);
                    Console.WriteLine($"{Name} Attacked {target.Name} with {weapon.Name} for {damage} Damage!");
                }
                else if (weapon != null)
                {
                    Console.WriteLine($"{Name} Is too far away from {target.Name} to be able to attack with {weapon.Name}.");
                }
            }
        }
        private void UnarmedHit(Entity target)
        {
            // If entity is left without a weapon, then it would do no damage, but this ensures that the entity can still do damage whether or not it has a weapon, but not as strong as having a weapon. 
            int damage = 4;
            target.ReceiveHit(damage);
            Console.WriteLine($"{Name} Attacked {target.Name} with an unarmed Hit for {damage} damage!");
        }

        // The defense mechanism which is used when the entity takes damage and the armour that the entity is wielding, reduces an X amount of damage that is taken from the enemy. 
        public void ReceiveHit(int hitPoints)
        {
            int totalDefense = ArmourItems.Sum(item => item.ReduceHitPoint);
            int damageTaken = Math.Max(hitPoints - totalDefense, 0);

            HitPoint -= damageTaken;
            Console.WriteLine($"{Name} Received {damageTaken} Damage, after a defense stance and now has {HitPoint} health left.");

            if (HitPoint <= 0)
                Die();
        }

        
        // This gives the entity the ability loot objects in the game's world, such weapons etc. 
        // Logik for at opsamle et objekt.
        public void Loot(WorldObject worldObject)
        {
            if (worldObject is Weapon weapon && weapon.Lootable)
            {
                WeaponItems.Add(weapon);
                Console.WriteLine($"{Name} Picked up a weapon: {weapon.Name}.");
            }
            else if (worldObject is Armour armour && armour.Lootable)
            {
                ArmourItems.Add(armour);
                Console.WriteLine($"{Name} Picked up armour: {armour.Name}.");
            }
            else
            {
                Console.WriteLine($"{Name} Is not able to pick up: {worldObject.Name}.");
            }
        }

        // This is the logic behind the entity's death.
        private void Die()
        {
            Console.WriteLine($"{Name} is dead.");
        }

        // This is what a template which defines the moves in a fight. 
        public void PerformAction(Entity target)
        {       

            ChooseAction(); // The entity can perform an action depending on its status and what it has in its inventory.
            Hit(target); // To perform an attack or another action. 
            Logger.Log($"{Name} Performed an action.");
        }

        // A virtual method that can be adapted in derived classes. 
        protected virtual void ChooseAction()
        {
            // Choose action. Can be more complex in derived classes.
        }

        // Like being able to loot chest, the entities can also loot eachother, and grab the enemy's items as well. 
        public void LootAllEntity(Entity deadEntity)
        {
            if (deadEntity == null || deadEntity.HitPoint > 0)
            {
                Console.WriteLine("Nothing to pick up - Entity may not be dead yet.");
                return;
            }

            WeaponItems.AddRange(deadEntity.WeaponItems);
            ArmourItems.AddRange(deadEntity.ArmourItems);
            Console.WriteLine($"{Name} has picked up all items from the corpse of {deadEntity.Name}.");
        }

        // Can choose to loot specific item instead of all items, if the other items are garbage and not needed for character.
        public void LootSpecificEntity(Entity deadEntity, WorldObject item)
        {
            if (deadEntity == null || deadEntity.HitPoint > 0)
            {
                Console.WriteLine("Nothing to pick up - Entity may not be dead yet.");
                return;
            }

            if (item is Weapon attackItem && deadEntity.WeaponItems.Contains(attackItem))
            {
                WeaponItems.Add(attackItem);
                deadEntity.WeaponItems.Remove(attackItem);
            }
            else if (item is Armour defenceItem && deadEntity.ArmourItems.Contains(defenceItem))
            {
                ArmourItems.Add(defenceItem);
                deadEntity.ArmourItems.Remove(defenceItem);
            }

            Console.WriteLine($"{Name} Has picked up a specific item: {item.Name} from the corpse of {deadEntity.Name}.");
        }

        // Like being able to loot corpses, the entities can also loot treasures, and grab the loot inside. 
        public void LootAllTreasure(Treasure treasure)
        {
            if (treasure == null || !treasure.IsLootable)
            {
                Console.WriteLine("Nothing to pick up from the treasure - Treasure may not be available");
                return;
            }

            WeaponItems.AddRange(treasure.WeaponItems);
            ArmourItems.AddRange(treasure.ArmourItems);
            Console.WriteLine($"{Name} Has picked up everything from the treasure.");
        }

        // Can choose to loot specific item instead of all items, if the other items are garbage and not needed for character.
        public void LootSpecificTreasure(Treasure treasure, WorldObject item)
        {
            if (treasure == null || !treasure.IsLootable)
            {
                Console.WriteLine("Nothing to pick up from the treasure - Treasure may not be available.");
                return;
            }

            if (item is Weapon weapon && treasure.WeaponItems.Contains(weapon))
            {
                WeaponItems.Add(weapon);
                treasure.WeaponItems.Remove(weapon);
            }
            else if (item is Armour armour && treasure.ArmourItems.Contains(armour))
            {
                ArmourItems.Add(armour);
                treasure.ArmourItems.Remove(armour);
            }

            Console.WriteLine($"{Name} has picked up the following item: {item.Name} from the treasure.");
        }

        public void UsePotion(Potions potion)
        {
            // Looks and checks if there is a potion in the entity's inventory
            if (potion != null && Pots.Contains(potion) && !potion.IsUsed)
            {
                if (potion.Use(this)) // Handles the restoration of the entity's health and sets IsUsed to true.
                {
                    Console.WriteLine($"{Name} has used a {potion.Name} and restored health to itself.");
                    Pots.Remove(potion); // Removes the potion after use to ensure no unlimited amount of potions.
                }
                else
                {
                    Console.WriteLine($"{potion.Name} cannot be used again or target doesn't exist.");
                }
            }
            else
            {
                Console.WriteLine($"Potion has either been used already or doesn't exist in entity's inventory.");
            }
        }


        public void RestoreHealth(int amount)
        {
            HitPoint += amount;
            if (HitPoint > MaxHitPoints)
            {
                HitPoint = MaxHitPoints; // Cap the HitPoint to MaxHitPoints
            }
            Console.WriteLine($"{Name} now has {HitPoint}/{MaxHitPoints} health.");
        }



    }
}
