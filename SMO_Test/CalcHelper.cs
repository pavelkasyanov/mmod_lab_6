using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q_SchemeModuleProject;

namespace SMO_Test
{
    public class CalcHelper
    {
        public double[] CalcUniformity2(List<Request> list, int intervalCount, double simulateTime)
        {
            double[] result = new double[intervalCount];

            //double offset = simulateTime / intervalCount;
            double offset = 2;

            double intStart = 0.0;
            double intEnd = offset;
            for (int i = 0; i < intervalCount; i++)
            {
                double count = HitCount2(list, intStart, intEnd);
                result[i] = count/(double) simulateTime;

                intStart += offset;
                intEnd += offset;
            }

            return result;
        }

        public double[] CalcUniformity3(List<Request> list, int intervalCount, double simulateTime)
        {
            double[] result = new double[intervalCount];

            //double offset = simulateTime / intervalCount;
            double offset = 2;

            double intStart = 0.0;
            double intEnd = offset;
            for (int i = 0; i < intervalCount; i++)
            {
                double count = HitCount3(list, intStart, intEnd);
                result[i] = count / (double)simulateTime;

                intStart += offset;
                intEnd += offset;
            }

            return result;
        }
        private double HitCount2(List<Request> arr, double start, double end)
        {
            double count = 0;
            foreach (var item in arr)
            {
                var delta = item.EndTime - item.StartTime;
                if (delta >= start && delta < end)
                {
                    count += delta;
                    //count++;
                }
            }

            return count;
        }

        private double HitCount3(List<Request> arr, double start, double end)
        {
            double count = 0;
            foreach (var item in arr)
            {
                var delta = item.EndTime;
                if (delta >= start && delta < end)
                {
                    count += delta;
                    //count++;
                }
            }

            return count;
        }
    }
}
