using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q_SchemeModule.Generators;
using Q_SchemeModuleProject.Generators;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator gen = new LinearGenerator(1, 10);

            for (int i = 0; i < 100; i++)
            {
                System.Console.WriteLine(gen.Generate());
            }
        }
    }
}
