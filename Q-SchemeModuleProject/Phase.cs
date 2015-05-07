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

        public int _phaseId = 0;

        public int RequestCheck;

        public Phase(int queueLen, int chanelCount, Generator generator, int phaseId)
        {
            ChanelCount = chanelCount;
            _phaseId = phaseId;
            SQueue = new ServiceQueue(0, queueLen);

            SChannels = new ServiceChannel[ChanelCount];
            for (int i = 0; i < chanelCount; i++)
            {
                SChannels[i] = new ServiceChannel(generator);
            }

            RequestCheck = 0;
        }

        public double Run(double currentTime, Phase previousPhase)
        {
            if (currentTime == 0.0) return 0.0;

            for (int i = 0; i < previousPhase.ChanelCount; i++)
            {
                var chanel = previousPhase.SChannels[i];
                if (chanel.GetStatus(currentTime) == 2)
                {
                    if (SQueue.Push())
                    {
                        chanel.Status = 0;
                    }
                }
            }

            for (int i = 0; i < ChanelCount; i++)
            {
                if (SQueue.QueueLength == 0) break;

                if (SChannels[i].GetStatus(currentTime) == 0)
                {
                    SChannels[i].PushRequest(currentTime);
                    SQueue.Pop();

                    RequestCheck += 1;
                }

                //Logger.Write(string.Format("push phase:{0} chanel:{1} time:{2}, status:{3}\n", 
                //    this._phaseId, i, currentTime, status));
            }

            return 0;
        }

        public int ChanelStatus(int chanelId, double time)
        {
            return SChannels[chanelId].GetStatus(time);
        }

        public bool IsStop()
        {
            if (SQueue.QueueLength != 0)
                return false;

            foreach (var chanel in SChannels)
            {
                if (chanel.Status != 0)
                    return false;
            }

            return true;
        }
    }
}
