namespace SwinAdventure
{
    internal class Program
    {
        static string[] CommandExe(string input)
        {
            return input.Split(' ');
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter player's name:");
            string playerName = Console.ReadLine();

            Console.WriteLine("Enter player's description:");
            string playerDescription = Console.ReadLine();

            Player player = new Player(playerName, playerDescription);

            Locations home = new Locations("Home", playerName + "'s home.");
            Item laptop = new Item(new string[] { "laptop", "computer" }, "Laptop", "A simple laptop.");
            home.Inventory.Put(laptop);

            Locations forest = new Locations("Forest", "A dense forest.");
            Locations cave = new Locations("Cave", "A dark cave.");
            Locations garden = new Locations("Garden", "A beautiful garden.");

            Paths home2forest = new Paths(new string[] { "north", "n" }, "Path 1", "A path from home to the forest.", home, forest);
            Paths forest2home = new Paths(new string[] { "south", "s" }, "Path 2", "A path from the forest to the home.", forest, home);
            Paths home2garden = new Paths(new string[] { "east", "e" }, "Path 3", "A path from home to the garden.", home, garden);
            Paths garden2home = new Paths(new string[] { "west", "w" }, "Path 4", "A path from the garden to the home.", garden, home);


            home.AddPath(home2forest);
            home.AddPath(home2garden);
            forest.AddPath(forest2home);
            garden.AddPath(garden2home);

            player.Location = home;

            Item BronzeSword = new Item(new string[] { "sword", "weapon" }, "Bronze Sword", "A simple bronze sword.");
            Item BronzeAxe = new Item(new string[] { "axe", "weapon" }, "Bronze Axe", "A simple bronze axe.");

            player.Inventory.Put(BronzeSword);
            player.Inventory.Put(BronzeAxe);

            Bag bag = new Bag(new string[] { "bag", "inventory" }, "Bag", "A simple bag.");
            player.Inventory.Put(bag);

            Item gem = new Item(new string[] { "gem", "jewel" }, "Gem", "A shiny gem.");
            bag.Inventory.Put(gem);

            while (true)
            {
                Console.WriteLine("Enter a command: ");
                Command command =  new CommandProcessor();
                string _input = Console.ReadLine();

                if (_input.ToLower() == "quit")
                {
                    break;
                }
                else 
                {
                    Console.WriteLine(command.Execute(player, CommandExe(_input)));
                }
            }
        }
    }
}
