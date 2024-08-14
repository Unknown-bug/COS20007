using SwinAdventure;

namespace BagTest
{
    internal class BagTest
    {
        Bag bag = new Bag(new string[] { "bag" }, "bag", "a bag");
        Item BronzeSword = new Item(new string[] { "sword", "weapon" }, "Bronze Sword", "A simple bronze sword.");
        Item BronzeAxe = new Item(new string[] { "axe", "weapon" }, "Bronze Axe", "A simple bronze axe.");
        [SetUp]
        public void Setup()
        {
            bag = new Bag(new string[] { "bag" }, "bag", "a bag");
        }

        [Test]
        public void TestBagLocate()
        {
            bag.Inventory.Put(BronzeSword);

            GameObject obj = bag.Locate("sword");

            Assert.That(obj, Is.EqualTo(BronzeSword));
        }

        [Test]
        public void TestBagLocateItself()
        {
            GameObject obj = bag.Locate("bag");

            Assert.That(obj, Is.EqualTo(bag));
        }

        [Test]
        public void TestBagLocateNothing()
        {
            GameObject obj = bag.Locate("sword");

            Assert.IsNull(obj);
        }

        [Test]
        public void TestBagFullDescription()
        {
            bag.Inventory.Put(BronzeSword);
            bag.Inventory.Put(BronzeAxe);

            string desc = bag.FullDescription;

            Assert.That(desc, Is.EqualTo("In the bag you can see:\nBronze Sword (sword)\nBronze Axe (axe)\n"));
        }

        [Test]
        public void BagInBag()
        {
            Bag b1 = new Bag(new string[] { "bag" }, "bag1", "a bag");
            Bag b2 = new Bag(new string[] { "bag" }, "bag2", "a bag");

            b1.Inventory.Put(b2);
            b1.Inventory.Put(BronzeSword);
            b2.Inventory.Put(BronzeAxe);

            GameObject obj1 = b1.Locate("bag");
            GameObject obj2 = b1.Locate("sword");
            Assert.IsNotNull(obj1);
            Assert.IsNotNull(obj2);

            GameObject obj3 = b1.Locate("axe");
            Assert.IsNull(obj3);
        }
    }
}
