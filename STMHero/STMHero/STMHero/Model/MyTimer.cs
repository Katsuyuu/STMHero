using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace STMHero.Model
{
    public class MyTimer : System.Timers.Timer
    {
        public MyTimer(double interval)
            : base(interval)
        {
        }

        public object Tag { get; set; }
    }
}
