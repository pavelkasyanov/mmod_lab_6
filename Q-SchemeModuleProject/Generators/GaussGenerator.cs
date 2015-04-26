using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q_SchemeModuleProject.Generators
{
    public class GaussGenerator : Generator
    {
        public int N { get; private set; }
        public double M { get; private set; }
        public double Sigma { get; private set; }

        private readonly double _sqrtFrom2 = Math.Sqrt(2);

        public GaussGenerator(double m, double sigma, int n = 6)
        {
            N = n;
            M = m;
            Sigma = sigma;
        }

        public override double Generate()
        {
            double r = 0.0;
            for (int i = 0; i < N; i++)
            {
                r += Rnd.NextDouble();
            }

            double t = M + ( Sigma * _sqrtFrom2 * (r - (double)(N/2)) );

            return t;
        }
    }
}
