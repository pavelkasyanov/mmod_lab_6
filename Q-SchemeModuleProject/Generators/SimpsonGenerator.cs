using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q_SchemeModuleProject.Generators
{
    public class SimpsonGenerator : Generator
    {
        public double A { get; private set; }
        public double B { get; private set; }

        public SimpsonGenerator(double a, double b)
        {
            A = a;
            B = b;
        }

        public override double Generate()
        {
            double x = (A/2) + Rnd.NextDouble()*((B/2) - (A/2));
            double y = (A / 2) + Rnd.NextDouble() * ((B / 2) - (A / 2));

            double t = x + y;

            return t;
        }
    }
}
