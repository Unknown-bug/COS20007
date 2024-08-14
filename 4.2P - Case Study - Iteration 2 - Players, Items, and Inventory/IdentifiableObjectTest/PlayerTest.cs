using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinAdventure;

namespace PlayerTest
{
    internal class PlayerTest
    {
        Player _player = new Player("Sen", "luv ur mom");
        Item BronzeSword = new Item(new string[] { "sword", "weapon" }, "Bronze Sword", "A simple bronze sword.");
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void PlayerisIdentifiableTest()
        {
            Assert.IsTrue(_player.AreYou("me"));
            Assert.IsTrue(_player.AreYou("inventory"));
        }

        [Test]
        public void PlayerLocateItemTest()
        {
            _player.Inventory.Put(BronzeSword);

            Assert.AreEqual(BronzeSword, _player.Locate("sword"));
        }

        [Test]
        public void PlayerLocateItselfTest()
        {
            Assert.AreEqual(_player, _player.Locate("me"));
            Assert.AreEqual(_player, _player.Locate("inventory"));
        }

        [Test]
        public void PlayerLocateNothingTest()
        {
            Assert.IsNull(_player.Locate("sadaw"));
        }

        [Test]
        public void PlayerFullDescriptionTest()
        {
            _player.Inventory.Put(BronzeSword);
            string desc = _player.FullDescription;

            Assert.AreEqual("You are Sen, luv ur mom. You are carrying: \tBronze Sword (sword)\n", desc);
        }
    }
}
