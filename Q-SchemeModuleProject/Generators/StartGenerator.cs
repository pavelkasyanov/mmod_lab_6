using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q_SchemeModuleProject.Generators
{
    public class StartGenerator
    {
        private double _checkTime = 0.0;
        private double _handlingTime = 0.0;

        private readonly ExponentialGenerator _gen = new ExponentialGenerator(1);

        public bool Generate(double currentTime)
        {
            if (currentTime >= _checkTime)
            {
                _handlingTime = _gen.Generate();
                _checkTime = currentTime + _handlingTime;

                return true;
            }

            return false;
        }
    }
}
