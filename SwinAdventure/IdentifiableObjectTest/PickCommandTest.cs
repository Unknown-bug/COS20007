using SwinAdventure;

namespace Test
{
    public class PickCommandTest
    {
        Bag _bag;
        Item _item;
        Player _player;

        [SetUp]
        public void Setup()
        {
            _bag = new Bag(new string[] { "bag" }, "bag", "a bag");
            _item = new Item(new string[] { "item" }, "item", "an item");
            _player = new Player("Sen", "luv ur mom");
            _bag.Inventory.Put(_item);
            _player.Inventory.Put(_bag);
        }

        [Test]
        public void test()
        {

        }
    }
}
