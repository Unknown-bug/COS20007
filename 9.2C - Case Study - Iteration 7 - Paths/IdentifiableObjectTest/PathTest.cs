using SwinAdventure;

namespace PathTest
{
    public class PathTest
    {
        Paths _path;
        Locations _roomA;
        Locations _roomB;

        [SetUp]
        public void Setup()
        {
            _roomA = new Locations("Room A", "A room.");
            _roomB = new Locations("Room B", "A room.");

            _path = new Paths(new string[] { "north" }, "Path", "A path from the source to the destination.", _roomA, _roomB);
        }

        [Test]
        public void PathisIdentifiableTest()
        {
            Assert.IsTrue(_path.AreYou("path"));
        }

        [Test]
        public void PathFullDescriptionTest()
        {
            string desc = _path.FullDescription;

            Assert.That(desc, Is.EqualTo("Path (north): A path from the source to the destination."));
        }

        [Test]
        public void PathIsLockedTest()
        {
            Assert.IsFalse(_path.IsLocked);
        }

        [Test]
        public void PathSetIsLockedTest()
        {
            _path.IsLocked = true;

            Assert.IsTrue(_path.IsLocked);
        }

        [Test]
        public void PathDestinationTest()
        {
            Assert.That(_path.Destination.Name, Is.EqualTo("Room B"));
        }
    }
}
