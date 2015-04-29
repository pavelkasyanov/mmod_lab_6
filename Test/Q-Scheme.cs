using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Q_SchemeModuleProject;
using Q_SchemeModuleProject.Generators;
using Q_SchemeModuleProject.ServiceChannels;

namespace Test
{
    public class QScheme
    {
        private double _time;
        private readonly double _dt;

        private readonly int _requestCount;

        private Phase _phase1;
        private Phase _phase2;
        private Phase _phase3;
        private Phase _phase4;
        private Phase _phase5;

        private List<double> _checkRequests;
        private List<double> _discardedRequests;
        private int _resCheck = 0;

        private readonly StartGenerator _u = new StartGenerator();

        public QScheme(double dt, int requestCount)
        {
            _dt = dt;
            _time = 0;
            _requestCount = requestCount;

            _phase1 = new Phase(3, 3, new SimpsonGenerator(2, 5), 1);

            _phase2 = new Phase(3, 4, new GaussGenerator(5, 1), 2);

            _phase3 = new Phase(3, 3, new LinearGenerator(3, 9), 3);

            _phase4 = new Phase(3, 4, new GaussGenerator(5, 1), 4);

            _phase5 = new Phase(3, 3, new LinearGenerator(3, 9), 5);

            _checkRequests = new List<double>();
            _discardedRequests = new List<double>();
        }

        public int Start()
        {
            int reqGen = 0;
            while (true)
            {
                //if (reqGen < _requestCount)
                if (_checkRequests.Count < _requestCount)
                {
                    if (_u.Generate(_time))
                    {
                        reqGen++;
                        if (_phase1.SQueue.Push() == false)
                        {
                            _discardedRequests.Add(_time);
                        }
                    }
                }

                CheckFirstPhase(_phase1, _time);

                //ScheckPhase(_time, _phase5, _phase4);
                //ScheckPhase(_time, _phase4, _phase3);
                //ScheckPhase(_time, _phase3, _phase2);
                ScheckPhase2(_time, _phase1, _phase2);
                ScheckPhase2(_time, _phase2, _phase3);
                ScheckPhase2(_time, _phase3, _phase4);
                ScheckPhase2(_time, _phase4, _phase5);

                CheckLastPhase(_phase5, _time);

                _time += _dt;

                Console.WriteLine("curTime = {0} request #{1} check: {2}", _time, reqGen, _checkRequests.Count);

                //if (reqGen > _requestCount)
                if (_checkRequests.Count > _requestCount)
                {
                    if (IsExit())
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("check: {0}", _resCheck);
            Console.WriteLine("discarded: {0}", _discardedRequests.Count);

            Statistic();

            return 0;
        }

        private void ScheckPhase(double time, Phase phase, Phase prevPhase)
        {
            //phase.Run(time, prevPhase);

            for (int i = 0; i < phase.ChanelCount; i++)
            {
                if (phase.SChannels[i].GetStatus(time) == 0)
                {
                    if (phase.SQueue.Pop())
                    {
                        phase.SChannels[i].PushRequest(time);
                    }
                }
            }

            for (int i = 0; i < prevPhase.ChanelCount; i++)
            {
                if (prevPhase.SChannels[i].GetStatus(time) == 2)
                {
                    if (phase.SQueue.Push() == true)
                    {
                        prevPhase.SChannels[i].Status = 0;
                        phase.RequestCheck++;
                    }
                }
            }
        }

        private void ScheckPhase2(double time, Phase phase, Phase nextPhase)
        {
            for (int i = 0; i < phase.ChanelCount; i++)
            {
                if (phase.SChannels[i].GetStatus(time) == 2)
                {
                    if (nextPhase.SQueue.Push() == true)
                    {
                        nextPhase.RequestCheck++;
                        phase.SChannels[i].Status = 0;
                    }
                }
            }

            for (int i = 0; i < nextPhase.ChanelCount; i++)
            {
                if (nextPhase.SChannels[i].GetStatus(time) == 0)
                {
                    if (nextPhase.SQueue.Pop() == true)
                    {
                        nextPhase.SChannels[i].PushRequest(time);
                    }
                }
            }
        }

        private void CheckLastPhase(Phase phase, double time)
        {
            for (int i = 0; i < phase.ChanelCount; i++)
            {
                //int status = phase.SChannels[i].GetStatus(time);
                //if (status == 2)
                if (_time > phase.SChannels[i].CheckTime && phase.SChannels[i].Status == 1)
                {
                    _resCheck++;
                    phase.SChannels[i].Status = 0;
                    _checkRequests.Add(time);
                }
            }
        }

        private void CheckFirstPhase(Phase phase, double time)
        {
            //if (time == 0.0) return;
            for (int i = 0; i < phase.ChanelCount; i++)
            {
                if (phase.SQueue.QueueLength == 0) return;

                if (phase.SChannels[i].Status == 0)
                {
                    _phase1.RequestCheck += 1;
                    phase.SChannels[i].PushRequest(time);
                    phase.SQueue.Pop();
                }
            }
        }

        private bool IsExit()
        {
            if (!_phase1.IsStop()) return false;
            if (!_phase2.IsStop()) return false;
            if (!_phase3.IsStop()) return false;
            if (!_phase4.IsStop()) return false;
            if (!_phase5.IsStop()) return false;

            return true;
        }

        private void Statistic()
        {
            Console.WriteLine("phase1 check: {0}", _phase1.RequestCheck);
            Console.WriteLine("phase2 check: {0}", _phase2.RequestCheck);
            Console.WriteLine("phase3 check: {0}", _phase3.RequestCheck);
            Console.WriteLine("phase4 check: {0}", _phase4.RequestCheck);
            Console.WriteLine("phase5 check: {0}", _phase5.RequestCheck);
        }
    }
}
