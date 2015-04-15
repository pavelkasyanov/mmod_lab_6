
namespace Q_SchemeModuleProject.Generators
{
    public class LinearGenerator : Generator
    {
        public int Max { get; private set; }
        public int Min { get; private set; }

        public LinearGenerator(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public override double Generate()
        {
            var t = Min + rnd.NextDouble() * (Max - Min);
            return t;
        }
    }
}
