using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q_SchemeModuleProject.Generators;
using Q_SchemeModuleProject.Queues;
using Q_SchemeModuleProject.ServiceChannels;

namespace Q_SchemeModuleProject
{
    public class Phase
    {
        public ServiceChannel[] SChannels;

        public int ChanelCount { get; private set; }

        public ServiceQueue SQueue;

        public Phase(int queueLen, int chanelCount, Generator generator)
        {
            ChanelCount = chanelCount;
            SQueue = new ServiceQueue(0, queueLen);

            for (int i = 0; i < chanelCount; i++)
            {
                SChannels[i] = new ServiceChannel(generator);
            }
        }

        public double Run(double currentTime, Phase previousPhase)
        {
            return 0;
        }
    }
}
