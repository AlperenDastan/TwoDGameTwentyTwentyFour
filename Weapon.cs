using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDGameTwentyTwentyFour
{
    // Weapon class that inherits world object, which is used to perform an attack. 
    public class Weapon : WorldObject, IAttack
    {
        public int Damage { get; private set; }
        public int Range { get; private set; }

        public Weapon(string name, int damage, int range)
            : base(name, true, true)
        {
            Damage = damage;
            Range = range;
        }


        public void Attack(Entity target)
        {
            // Logic for attack through the interface.
            if (target != null)
            {
                Logger.Log($"{Name} Attacks {target.Name}");
                target.ReceiveHit(Damage);
            }
        }
    }
}
