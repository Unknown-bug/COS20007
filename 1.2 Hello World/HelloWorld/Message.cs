using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    internal class Message
    {
        private string _text;

        public Message(string text)
        {
            _text = text;
        }
        public void Print()
        {
            Console.WriteLine(_text);
        }
    }
}
