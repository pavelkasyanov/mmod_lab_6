namespace Q_SchemeModuleProject.Generators
{
    public class LinearGenerator : Generator
    {
        public int B { get; private set; }
        public int A { get; private set; }

        public LinearGenerator(int a, int b)
        {
            A = a;
            B = b;
        }

        public override double Generate()
        {
            double t = A + Rnd.NextDouble() * (B - A);
            return t;
        }
    }
}
