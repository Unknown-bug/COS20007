using ClockApplication;

namespace ClockTest
{
    public class CounterTest
    {
        Counter _countertest;

        [SetUp]
        public void Setup()
        {
            _countertest = new Counter();
        }

        [Test]
        public void TestInitializes()
        {
            Counter ini = new Counter();
            Assert.IsNotNull(ini);
            Assert.That(ini.Tick, Is.EqualTo(0));
        }

        [Test]
        public void TestStart()
        {
            Assert.That(_countertest.Tick, Is.EqualTo(0));
        }

        [Test]
        public void TestCountReset()
        {
            _countertest.Increment();
            _countertest.Reset();
            Assert.That(_countertest.Tick, Is.EqualTo(0));
        }

        [TestCase(60, 60)]
        [TestCase(100, 100)]
        public void TestIncrement(int tick, int result)
        {
            for (int i = 0; i < tick; i++)
            {
                _countertest.Increment();
            }
            Assert.That(_countertest.Tick, Is.EqualTo(result));
        }
    }
}
