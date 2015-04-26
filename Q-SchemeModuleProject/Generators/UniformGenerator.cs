using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q_SchemeModuleProject.Generators
{
    public class UniformGenerator : Generator
    {
        public override double Generate()
        {
            double t =  Rnd.NextDouble();
            return t;
        }
    }
}
