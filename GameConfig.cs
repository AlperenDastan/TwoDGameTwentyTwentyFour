using Newtonsoft.Json;
using System.IO;

namespace TwoDGameTwentyTwentyFour
{
    public class GameConfig
    {
        public int WorldMaxX { get; set; }
        public int WorldMaxY { get; set; }
        public List<EntityConfig> Entity { get; set; }
        public List<ItemConfig> ChestItems { get; set; }

        public static GameConfig Load(string path)
        {
            try
            {
                var configText = File.ReadAllText(path);
                var config = JsonConvert.DeserializeObject<GameConfig>(configText);
                Console.WriteLine("Configuration loaded successfully.");
                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load configuration: " + ex.Message);
                return null; // or handle the error as appropriate
            }
        }
    }

    public class EntityConfig
    {
        public string Name { get; set; }
        public int MaxHealth { get; set; }
        public int StartingX { get; set; }
        public int StartingY { get; set; }
    }

    public class ItemConfig
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
