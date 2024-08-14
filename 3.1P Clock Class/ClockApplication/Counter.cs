using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockApplication
{
    public class Counter
    {
        private int _count;

        public Counter()
        {
            _count = 0;
        }

        public void Increment()
        {
            _count++;
        }

        public void Reset()
        {
            _count = 0;
        }

        public int Tick()
        {
            return _count;
        }
    }
}
