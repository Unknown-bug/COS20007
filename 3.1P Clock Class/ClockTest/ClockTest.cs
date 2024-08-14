using ClockApplication;

namespace ClockTest
{
    public class Tests
    {
        private Clock clock;

        [SetUp]
        public void Setup()
        {
            clock = new Clock();
        }

        [Test]
        public void ClockInitializes()
        {
            Clock ini = new Clock();
            Assert.IsNotNull(ini);
            Assert.AreEqual(0, ini.Seconds);
            Assert.AreEqual(0, ini.Minutes);
            Assert.AreEqual(0, ini.Hours);
        }

        [Test]
        public void TestTickShouldIncrementSecondsByOne()
        {
            int initialSeconds = clock.Seconds;

            clock.Tick();

            Assert.AreEqual(initialSeconds + 1, clock.Seconds);
        }

        [Test]
        public void TestTickShouldIncrementMinutesByOneWhenSecondsReachSixty()
        { 
            int initialMinutes = clock.Minutes;
            for (int i = 0; i <= 60; i++)
            {
                clock.Tick();
            }

            Assert.AreEqual(initialMinutes + 1, clock.Minutes);
        }

        [Test]
        public void TestTickShouldIncrementHoursByOneWhenMinutesReachSixty()
        {
            int initialHours = clock.Hours;
            for (int i = 0; i <= 3600; i++)
            {
                clock.Tick();
            }

            Assert.AreEqual(initialHours + 1, clock.Hours);
        }
    }
}