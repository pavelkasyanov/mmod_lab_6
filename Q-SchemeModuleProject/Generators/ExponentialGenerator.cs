using System;

namespace Q_SchemeModuleProject.Generators
{
    class ExponentialGenerator : Generator
    {
        public double Mean { get; private set; }

        public ExponentialGenerator(double mean)
        {
            Mean = mean;
        }

        public override double Generate()
        {
            double t = -Mean * Math.Log(Rnd.NextDouble());
            return t;
        }
    }
}
