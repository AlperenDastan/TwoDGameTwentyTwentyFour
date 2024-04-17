using TwoDGameTwentyTwentyFour;

class Program
{
    static void Main()
    {
        // Load configuration from a specified path
        string configPath = "C:\\Users\\Alper\\Desktop\\TwoDGameTwentyTwentyFour\\gameConfig.json";
        GameConfig config = GameConfig.Load(configPath);

        // Initialize the game world with configuration dimensions
        World gameWorld = new World(config.WorldMaxX, config.WorldMaxY);

        // Add entities and items to the world
        gameWorld.Initialize();

        // Display initial world state
        foreach (var entity in gameWorld.Entities)
        {
            Console.WriteLine($"Entity: {entity.Name}, HP: {entity.HitPoint}/{entity.MaxHitPoints}, Location: ({entity.X}, {entity.Y})");
        }

        // Simulate actions in the game world
        if (gameWorld.Entities.Count >= 2)
        {
            Entity player = gameWorld.Entities[0];
            Entity enemy = gameWorld.Entities[1];

            // Player loots a treasure if available before combat
            if (gameWorld.Treasures.Any())
            {
                player.LootAllTreasure(gameWorld.Treasures.First());
                Console.WriteLine($"{player.Name} has looted a treasure containing weapons, armour, and potions.");
            }

            // Combat loop: continues until one of them dies
            while (player.HitPoint > 0 && enemy.HitPoint > 0)
            {
                player.PerformAction(enemy); // Player takes action against the enemy

                if (enemy.HitPoint > 0) // Enemy retaliates if still alive
                {
                    enemy.PerformAction(player);
                }
            }

            // Display who died
            if (player.HitPoint <= 0)
            {
                Console.WriteLine($"{player.Name} is dead & {enemy.Name} Wins!");
            }
            if (enemy.HitPoint <= 0)
            {
                Console.WriteLine($"{enemy.Name} is dead & {player.Name} Wins!");
            }
        }
       

        // Display message once combat and actions are concluded
        Console.WriteLine("Combat has ended. Continue with the rest of your game setup...");
    }
}

