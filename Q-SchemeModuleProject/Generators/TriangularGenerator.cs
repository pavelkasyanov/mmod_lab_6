using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q_SchemeModuleProject.Generators
{
    public class TriangularGenerator : Generator
    {
        public double A { get; private set; }
        
        public double B { get; private set; }
        
        public TriangularGenerator(double a, double b)
        {
            A = a;
            B = b;
        }

        public override double Generate()
        {
            double r1 = Rnd.NextDouble();
            double r2 = Rnd.NextDouble();

            double t = A + (B-A)*Math.Max(r1, r2);

            return t;
        }
    }
}
