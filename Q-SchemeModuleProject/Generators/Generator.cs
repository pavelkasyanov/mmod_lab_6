using System;

namespace Q_SchemeModuleProject.Generators
{
    public abstract class Generator
    {
        protected static Random rnd = new Random(1);

        public abstract double Generate();
    }
}
