using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDGameTwentyTwentyFour
{
    // An Armour object class that inherits from WorldObject, which is used to reduce damage. 
    public class Armour : WorldObject
    {
        public int ReduceHitPoint { get; private set; }

        public Armour(string name, int reduceHitPoint)
            : base(name, true, true)
        {
            ReduceHitPoint = reduceHitPoint;
        }
    }
}
