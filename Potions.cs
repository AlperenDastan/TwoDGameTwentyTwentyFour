using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDGameTwentyTwentyFour
{
    public class Potions
    {
        public string Name { get; private set; }
        public int HealthRestore { get; private set; }
        public bool IsUsed { get; private set; }

        public Potions(string name, int healthRestore)
        {
            Name = name;
            HealthRestore = healthRestore;
            IsUsed = false;
        }

        public bool Use(Entity target)
        {
            if (!IsUsed && target != null)
            {
                target.RestoreHealth(HealthRestore);
                IsUsed = true;
                Console.WriteLine($"{Name} Has been used on: {target.Name}, and is now restoring {HealthRestore} amounts of health.");
                return true;
            }
            else
            {
                Console.WriteLine($"{Name} Cannot be used again or target doesn't exist / is null.");
                return false;
            }
        }
    }}

