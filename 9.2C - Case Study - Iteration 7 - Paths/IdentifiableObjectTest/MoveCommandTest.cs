using SwinAdventure;

namespace MoveCommandTest
{
    public class MoveCommandTest
    {
        Command _moveCommand;
        Player _player;
        Locations _location, _destination;
        Paths _path, _returnPath;

        [SetUp]
        public void Setup()
        {
            _moveCommand = new MoveCommand();
            _player = new Player("Sen", "luv ur mom");
            _location = new Locations("Location", "A location");
            _destination = new Locations("Destination", "A destination");
            _path = new Paths(new string[] { "north" }, "Path", "A path from the source to the destination.", _location, _destination);
            _returnPath = new Paths(new string[] { "south" }, "Path", "A path from the source to the destination.", _destination, _location);
            _location.AddPath(_path);
            _destination.AddPath(_returnPath);
            _player.Location = _location;
        }

        [Test]
        public void MoveToDestinationTest()
        {
            string[] text = new string[] { "move", "to", "north" };
            Assert.That(_moveCommand.Execute(_player, text), Is.EqualTo("You moved to Destination.\n"));
        }

        [Test]
        public void MoveToLockedPathTest()
        {
            _path.IsLocked = true;
            string[] text = new string[] { "move", "to", "north" };
            Assert.That(_moveCommand.Execute(_player, text), Is.EqualTo("The path is blocked."));
        }

        [Test]
        public void MoveToInvalidPathTest()
        {
            string[] text = new string[] { "move", "to", "east" };
            Assert.That(_moveCommand.Execute(_player, text), Is.EqualTo("I don't know how to move that."));
        }

        [Test]
        public void MoveAndReturnTest()
        {
            string[] text = new string[] { "move", "to", "north" };
            _moveCommand.Execute(_player, text);
            text = new string[] { "move", "to", "south" };
            Assert.That(_moveCommand.Execute(_player, text), Is.EqualTo("You moved to Location.\n"));
        }
    }
}
