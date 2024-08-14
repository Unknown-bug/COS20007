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
                Command command = new LookCommand();
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
