using System;
using Q_SchemeModuleProject.Generators;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            double dt = 0.01;
            int requesCount = 1000;

            var qScheme = new QScheme(dt, requesCount);

            qScheme.Start();

            Console.ReadKey();
        }
    }
}
