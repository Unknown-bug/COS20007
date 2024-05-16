namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Message myMessage = new Message("Hello, World! Greetings from Message Object.");
            myMessage.Print();

            Message[] messages = 
            {
                new Message("It's nice to meet you. "),
                new Message("It's a pleasure to meet you. "),
                new Message("Hi."),
                new Message("Hello."),
                new Message("Greetings.")
            };
            
            Console.WriteLine("Enter name: ");
            string name = Console.ReadLine();
            name = name.ToLower();
            if (name == "wilma")
                messages[0].Print();
            else if (name == "fred")
                messages[1].Print();
            else if (name == "barney")
                messages[2].Print();
            else if (name == "betty")
                messages[3].Print();
            else
                messages[4].Print();
        }
    }
}
