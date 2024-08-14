namespace ClockApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Clock clock = new Clock();
            while (true)
            {
                Console.Clear();
                clock.Tick();
                Console.WriteLine($"{clock.Hours}:{clock.Minutes}:{clock.Seconds}");
                Thread.Sleep(1000);
            }
        }
    }
}
