using System;

namespace Q_SchemeModule.Generators
{
    public abstract class Generator
    {
        protected static Random rnd = new Random(1);

        public abstract double Generate();
    }
}
