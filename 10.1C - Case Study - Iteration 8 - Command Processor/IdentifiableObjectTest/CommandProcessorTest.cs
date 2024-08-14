using SwinAdventure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class CommandProcessorTest
    {
        private CommandProcessor _commandProcessor;
        private Player _player;
        private Locations _location;

        [SetUp]
        public void Setup()
        {
            _commandProcessor = new CommandProcessor();
            _location = new Locations( "Start Location", "This is the starting location.");
            _player = new Player("player", "player description") { Location = _location };

            // Adding a path to test MoveCommand
            var destination = new Locations( "Destination", "This is the destination.");
            var path = new Paths(new string[] { "north" }, "North Path", "A path to the north.", _location, destination);
            _location.AddPath(path);
        }

        [Test]
        public void TestMoveCommand()
        {
            string result = _commandProcessor.Execute(_player, new string[] { "move", "north" });
            Assert.AreEqual("You moved to Destination.\n", result);
            Assert.AreEqual("Destination", _player.Location.Name);
        }

        [Test]
        public void TestLookCommand()
        {
            // Add an item to the player's inventory to test LookCommand
            var item = new Item(new string[] { "shovel" }, "a shovel", "This is a rusty old shovel.");
            _player.Inventory.Put(item);

            string result = _commandProcessor.Execute(_player, new string[] { "look", "at", "shovel" });
            Assert.AreEqual("a shovel (shovel): This is a rusty old shovel.", result);
        }

        [Test]
        public void TestInvalidCommand()
        {
            string result = _commandProcessor.Execute(_player, new string[] { "fly" });
            Assert.AreEqual("This command is not availble.", result);
        }

        [Test]
        public void TestMoveCommandInvalidDirection()
        {
            string result = _commandProcessor.Execute(_player, new string[] { "move", "south" });
            Assert.AreEqual("I don't know how to move that.", result);
        }

        [Test]
        public void TestLookCommandInvalidSyntax()
        {
            string result = _commandProcessor.Execute(_player, new string[] { "look", "shovel" });
            Assert.AreEqual("I don't know how to look like that", result);
        }
    }
}
