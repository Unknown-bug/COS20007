using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemTest
{
    public class Folder : Thing
    {
        private List<Thing> _contents;

        public Folder(string name) : base(name)
        {
            _contents = new List<Thing>();
        }

        public void Add(Thing toAdd)
        {
            _contents.Add(toAdd);
        }

        public override int Size()
        {
            int size = 0;
            foreach (Thing thing in _contents)
            {
                size += thing.Size();
            }
            return size;
        }

        public override void Print()
        {
            if(_contents.Count == 0)
            {
                Console.WriteLine("The folder '{0}' is empty!", Name);
                return;
            }
            Console.WriteLine("The folder '{0}' contains ({1} bytes total:)", Name, Size());
            foreach (Thing item in _contents)
            {
                item.Print();
            }
        }
    }
}
