using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDGameTwentyTwentyFour
{
    public class World
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public List<Entity> Entities { get; set; }
        public List<WorldObject> WorldObjects { get; set; }
        public List<Treasure> Treasures { get; set; }  

        public World(int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
            Entities = new List<Entity>();
            WorldObjects = new List<WorldObject>();
            Treasures = new List<Treasure>(); 
        }

        public void AddEntity(Entity entity)
        {
            if (entity.X <= MaxX && entity.Y <= MaxY)
                Entities.Add(entity);
        }

        public void AddWorldObject(WorldObject worldObject)
        {
            WorldObjects.Add(worldObject);
        }

   
        public void AddTreasure(Treasure treasure)
        {
            if (Treasures != null)
                Treasures.Add(treasure);
        }

        public void Initialize()
        {
            // Uses the GameObjectFactory to add starting objects and entities to the world.
            AddEntity(GameObjectFactory.CreateEntity("Gordon Freeman", 150, 150, 1, 1));
            AddEntity(GameObjectFactory.CreateEntity("Nihilath", 300, 300, 2, 1));
            AddWorldObject(GameObjectFactory.CreateWeapon("Sword", 20, 4));
            AddWorldObject(GameObjectFactory.CreateArmour("Shield", 5));
            AddWorldObject(GameObjectFactory.CreateWeapon("Fist", 4, 2));
            // AddWorldObject(GameObjectFactory.CreatePotion("Health Potion", 40));

            // Adds a treasure with random loot inside. 
            Treasure treasure = GameObjectFactory.CreateTreasure();
            AddTreasure(treasure);
        }
    }
}
