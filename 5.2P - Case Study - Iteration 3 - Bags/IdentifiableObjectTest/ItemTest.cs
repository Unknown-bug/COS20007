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

            Assert.AreEqual("Bronze Sword (sword)", SwordShortDesc);
            Assert.AreEqual("Bronze Axe (axe)", AxeShortDesc);
        }

        [Test]
        public void TestFullDescription()
        {
            string SwordFullDesc = BronzeSword.FullDescription;
            string AxeFullDesc = BronzeAxe.FullDescription;

            Assert.AreEqual("The Bronze Sword (sword) A simple bronze sword.", SwordFullDesc);
            Assert.AreEqual("The Bronze Axe (axe) A simple bronze axe.", AxeFullDesc);
        }
    }
}
