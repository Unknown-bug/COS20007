using SwinAdventure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTest
{
    internal class InventoryTest
    {
        Item BronzeSword = new Item(new string[] { "sword", "weapon" }, "Bronze Sword", "A simple bronze sword.");
        Item BronzeAxe = new Item(new string[] { "axe", "weapon" }, "Bronze Axe", "A simple bronze axe.");
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void FindItemTest()
        {
            Inventory inv = new Inventory();
            
            inv.Put(BronzeAxe);
            inv.Put(BronzeSword);

            bool sword = inv.HasItem("sword");
            bool axe = inv.HasItem("axe");

            Assert.IsTrue(sword);
            Assert.IsTrue(axe);
        }

        [Test]
        public void NoItemFindTest()
        {
            Inventory inv = new Inventory();
            
            Assert.IsFalse(inv.HasItem("sword"));
            Assert.IsFalse(inv.HasItem("axe"));
        }

        [Test]
        public void FetchItemTest()
        {
            Inventory inv = new Inventory();

            inv.Put(BronzeAxe);
            inv.Put(BronzeSword);

            Item sword = inv.Fetch("sword");
            Item axe = inv.Fetch("axe");

            Assert.AreEqual(BronzeSword, sword);
            Assert.AreEqual(BronzeAxe, axe);
        }

        [Test]
        public void TakeItemTest()
        {
            Inventory inv = new Inventory();

            inv.Put(BronzeAxe);
            inv.Put(BronzeSword);

            Item sword = inv.Take("sword");
            Item axe = inv.Take("axe");

            Item SwordRemain = inv.Fetch("sword");
            Item AxeRemain = inv.Fetch("axe");

            Assert.AreEqual(BronzeSword, sword);
            Assert.AreEqual(BronzeAxe, axe);
            Assert.IsTrue(SwordRemain == null);
            Assert.IsTrue(AxeRemain == null);
        }

        [Test]
        public void ItemListTest()
        {
            Inventory inv = new Inventory();

            inv.Put(BronzeAxe);
            inv.Put(BronzeSword);

            string list = inv.ItemList;

            Assert.AreEqual("Bronze Axe (axe)\nBronze Sword (sword)\n", list);
        }
    }
}
