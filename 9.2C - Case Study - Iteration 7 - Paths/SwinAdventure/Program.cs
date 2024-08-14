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
            Locations forest = new Locations("Forest", "A dense forest.");
            Locations cave = new Locations("Cave", "A dark cave.");
            Paths home2forest = new Paths(new string[] { "north" }, "Path 1", "A path from home to the forest.", home, forest);
            Paths forest2home = new Paths(new string[] { "south" }, "Path 2", "A path from the forest to the cave.", forest, cave);
            home.AddPath(home2forest);
            forest.AddPath(forest2home);

            Console.WriteLine(home.PathsList);

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
                string[] _temp = _input.Split(" ");

                if (_input.ToLower() == "quit")
                {
                    break;
                }
                else 
                {
                    Console.WriteLine(command.Execute(player, _temp));
                }
            }
        }
    }
}
