using SwinAdventure;

namespace ItemTest
{
    public class ItemTest
    {
        Item BronzeSword = new Item(new string[] { "sword", "weapon" }, "Bronze Sword", "A simple bronze sword.");
        Item BronzeAxe = new Item(new string[] { "axe", "weapon" }, "Bronze Axe", "A simple bronze axe.");
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void TestItenIsIdentifiable()
        {
            bool SwordisIdentifiable = BronzeSword.AreYou("sword");
            bool AxeisIdentifiable = BronzeAxe.AreYou("sword");

            Assert.IsTrue(SwordisIdentifiable);
            Assert.IsFalse(AxeisIdentifiable);
        }

        [Test]
        public void TestShortDescription()
        {
            string SwordShortDesc = BronzeSword.ShortDescription;
            string AxeShortDesc = BronzeAxe.ShortDescription;

            Assert.That(SwordShortDesc, Is.EqualTo("Bronze Sword (sword)"));
            Assert.That(AxeShortDesc, Is.EqualTo("Bronze Axe (axe)"));
        }

        [Test]
        public void TestFullDescription()
        {
            string SwordFullDesc = BronzeSword.FullDescription;
            string AxeFullDesc = BronzeAxe.FullDescription;

            Assert.That(SwordFullDesc, Is.EqualTo("Bronze Sword (sword): A simple bronze sword."));
            Assert.That(AxeFullDesc, Is.EqualTo("Bronze Axe (axe): A simple bronze axe."));
        }
    }
}
