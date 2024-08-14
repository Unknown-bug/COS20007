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
            _player = new Player("Sen", "luv ur mom");
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

            Assert.That(_player.Locate("sword"), Is.EqualTo(BronzeSword));
        }

        [Test]
        public void PlayerLocateItselfTest()
        {
            Assert.That(_player.Locate("me"), Is.EqualTo(_player));
            Assert.That(_player.Locate("inventory"), Is.EqualTo(_player));
        }

        [Test]
        public void PlayerLocateNothingTest()
        {
            Assert.IsNull(_player.Locate("sword"));
        }

        [Test]
        public void PlayerFullDescriptionTest()
        {
            _player.Inventory.Put(BronzeSword);
            string desc = _player.FullDescription;

            Assert.That(desc, Is.EqualTo("You are Sen, you are carrying:\nBronze Sword (sword)\n"));
        }
    }
}
