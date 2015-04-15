using System;
using Q_SchemeModule.Generators;

namespace Q_SchemeModuleProject.Generators
{
    class ExponentialGenerator : Generator
    {
        public double Mean { get; private set; }

        public ExponentialGenerator(double mean)
        {
            this.Mean = mean;
        }

        public override double Generate()
        {
            var t = -Mean * Math.Log(rnd.NextDouble());
            return t;
        }
    }
}
