using Q_SchemeModuleProject.Generators;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            double dt = 1;
            int requesCount = 1000;

            var qScheme = new QScheme(dt, requesCount);

            qScheme.Start();
        }
    }
}
