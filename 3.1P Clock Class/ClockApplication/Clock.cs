using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockApplication
{
    public class Clock
    {
        Counter _second = new Counter();
        Counter _minute = new Counter();
        Counter _hour = new Counter();

        public Clock()
        {
            _second.Reset();
            _minute.Reset();
            _hour.Reset();
        }

        public int Seconds
        {
            get { return _second.Tick(); }
        }

        public int Minutes 
        { 
            get { return _minute.Tick(); } 
        }

        public int Hours 
        { 
            get { return _hour.Tick(); } 
        }

        public void Tick()
        {
            _second.Increment();
            if (_second.Tick() == 60)
            {
                _second.Reset();
                _minute.Increment();
            }
            if (_minute.Tick() == 60)
            {
                _minute.Reset();
                _hour.Increment();
            }
            if (_hour.Tick() == 24)
            {
                _hour.Reset();
            }
        }
    }
}
