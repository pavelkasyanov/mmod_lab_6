using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q_SchemeModuleProject
{
    public class Request
    {
        public double StartTime;
        public double EndTime;
        public double ServiceTime;

        public Request(double startTime)
        {
            StartTime = startTime;
        }
    }
}
