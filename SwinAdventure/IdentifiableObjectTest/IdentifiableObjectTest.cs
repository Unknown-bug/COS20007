using SwinAdventure;

namespace IdentifiableObjectTest
{
    public class IdentifiableObjectTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestAreYou()
        {
            IdentifiableObject obj = new IdentifiableObject(new string[] {"fred", "bob"});

            bool result1 = obj.AreYou("fred");
            bool result2 = obj.AreYou("bob");

            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void TestNotAreYou()
        {
            IdentifiableObject obj = new IdentifiableObject(new string[] { "fred", "bob" });
            
            bool result1 = obj.AreYou("wilma");
            bool result2 = obj.AreYou("boby");

            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }

        [Test]
        public void TestCaseSensitive()
        {
            IdentifiableObject obj = new IdentifiableObject(new string[] { "fred", "bob" });

            bool result1 = obj.AreYou("FRED");
            bool result2 = obj.AreYou("bOB");

            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void TestFirstID()
        {
            IdentifiableObject obj = new IdentifiableObject(new string[] { "fred", "bob" });

            string firstID = obj.FirstId;

            Assert.AreEqual("fred", firstID);
        }

        [Test]
        public void TestFirstIDWithNoIDs()
        {
            IdentifiableObject obj = new IdentifiableObject(new string[] { });

            string firstID = obj.FirstId;

            Assert.AreEqual("", firstID);
        }

        [Test] 
        public void TestAddIdentifier()
        {
            IdentifiableObject obj = new IdentifiableObject(new string[] { "fred", "bob" });

            obj.AddIdentifier("wilma");

            Assert.IsTrue(obj.AreYou("fred"));
            Assert.IsTrue(obj.AreYou("bob"));
            Assert.IsTrue(obj.AreYou("wilma"));
        }
    }
}