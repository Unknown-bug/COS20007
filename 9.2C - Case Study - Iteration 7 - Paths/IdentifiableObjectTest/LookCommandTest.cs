using SwinAdventure;

namespace LookCommandTest
{
    public class LookCommandTest
    {
        Command look;
        Player player;
        Bag bag;
        Item BronzeSword, BronzeAxe, gem;

        [SetUp]
        public void Setup()
        {
            look = new LookCommand();
            player = new Player("Sen", "luv ur mom");
            bag = new Bag(new string[] { "bag", "inventory" }, "Bag", "A simple bag.");
            BronzeSword = new Item(new string[] { "sword", "weapon" }, "Bronze Sword", "A simple bronze sword.");
            BronzeAxe = new Item(new string[] { "axe", "weapon" }, "Bronze Axe", "A simple bronze axe.");
            gem = new Item(new string[] { "gem", "jewel" }, "Gem", "A shiny gem.");
        }

        [Test]
        public void LookAtMeTest()
        {
            string[] text = new string[] { "look", "at", "me" };
            Assert.That(look.Execute(player, text), Is.EqualTo("You are Sen, you are carrying:\n"));
        }

        [Test]
        public void LookAtGemTest()
        {
            player.Inventory.Put(gem);
            string[] text = new string[] { "look", "at", "gem" };
            Assert.That(look.Execute(player, text), Is.EqualTo("Gem (gem): A shiny gem."));
        }

        [Test]
        public void LookAtUnkTest()
        {
            string exp = "I can't find the gem";
            string[] text = new string[] { "look", "at", "gem" };
            Assert.That(look.Execute(player, text), Is.EqualTo(exp));
        }

        [Test]
        public void LookAtGemInMeTest()
        {
            player.Inventory.Put(gem);
            string[] text = new string[] { "look", "at", "gem", "in", "inventory" };
            Assert.That(look.Execute(player, text), Is.EqualTo("Gem (gem): A shiny gem."));
        }

        [Test]
        public void LookAtGemInBagTest()
        {
            player.Inventory.Put(bag);
            bag.Inventory.Put(gem);
            string[] text = new string[] { "look", "at", "gem", "in", "bag" };
            Assert.That(look.Execute(player, text), Is.EqualTo("Gem (gem): A shiny gem."));
        }

        [Test]
        public void LookAtGemInNoBagTest()
        {
            bag.Inventory.Put(gem);
            string exp = "I can't find the bag";
            string[] text = new string[] { "look", "at", "gem", "in", "bag" };
            Assert.That(look.Execute(player, text), Is.EqualTo(exp));
        }

        [Test]
        public void LookAtNoGemInBagTest()
        {
            player.Inventory.Put(bag);
            string exp = "I can't find the gem";
            string[] text = new string[] { "look", "at", "gem", "in", "bag" };
            Assert.That(look.Execute(player, text), Is.EqualTo(exp));
        }

        [Test]
        public void InvalidLookTest()
        {
            string exp1 = "I don't know how to look like that";
            string exp2 = "I can't find the gem";
            string[] text = new string[] { "look", "at" };
            Assert.That(look.Execute(player, text), Is.EqualTo(exp1));
            player.Inventory.Put(gem);
            player.Inventory.Put(bag);
            text = new string[] { "look", "at", "gem", "in", "bag" };
            Assert.That(look.Execute(player, text), Is.EqualTo(exp2));
            text = new string[] { "hello" };
            Assert.That(look.Execute(player, text), Is.EqualTo(exp1));
        }
    }
}
