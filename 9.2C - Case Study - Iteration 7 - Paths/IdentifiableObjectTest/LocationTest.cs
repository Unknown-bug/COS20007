using SwinAdventure;

namespace LocationTest
{
    public class LocationTest
    {
        Command look;
        Player player;
        Bag bag;
        Item BronzeSword, BronzeAxe, gem;
        Locations home;

        [SetUp]
        public void Setup()
        {
            look = new LookCommand();

            player = new Player("Sen", "luv ur mom");

            bag = new Bag(new string[] { "bag", "inventory" }, "Bag", "A simple bag.");

            BronzeSword = new Item(new string[] { "sword", "weapon" }, "Bronze Sword", "A simple bronze sword.");
            BronzeAxe = new Item(new string[] { "axe", "weapon" }, "Bronze Axe", "A simple bronze axe.");
            gem = new Item(new string[] { "gem", "jewel" }, "Gem", "A shiny gem.");

            home = new Locations("Home", "Sen's home.");
            player.Location = home;
        }

        [Test]
        public void LocationLocateThemselvesTest()
        {
            player.Locate("home");
            Assert.That(player.Location, Is.EqualTo(home));
        }

        [Test]
        public void TestLocationLocateItem()
        {
            home.Inventory.Put(BronzeSword);
            home.Locate("sword");
            Assert.That(home.Locate("sword"), Is.EqualTo(BronzeSword));
        }

        [Test]
        public void TestFullDescription()
        {
            home.Inventory.Put(BronzeSword);
            home.Inventory.Put(BronzeAxe);
            home.Inventory.Put(gem);
            string expected = "You are in the Home.\nYou can see:\nBronze Sword (sword)\nBronze Axe (axe)\nGem (gem)\n";
            Assert.That(home.FullDescription, Is.EqualTo(expected));
        }

        [Test]
        public void PlayerLocateItemInLocation()
        {
            home.Inventory.Put(gem);
            player.Locate("gem");
            Assert.That(player.Locate("gem"), Is.EqualTo(gem));
        }
    }
}
