using System.Collections.Generic;
using Q_SchemeModuleProject;
using Q_SchemeModuleProject.Generators;

namespace SMO_Test
{
    public class QScheme
    {
        public double Time { get{ return _time; } }
        private double _time;
        private readonly double _dt;

        private readonly int _requestCount;

        public Phase Phase1;
        public Phase Phase2;
        public Phase Phase3;
        public Phase Phase4;
        public Phase Phase5;

        public List<double> CheckRequests;
        public List<double> DiscardedRequests;

        public int RequestGenerate = 0;

        public List<Request> RequestsCheckList;

        private readonly StartGenerator _u = new StartGenerator();

        public QScheme(double dt, int requestCount)
        {
            _dt = dt;
            _time = 0;
            _requestCount = requestCount;

            Phase1 = new Phase(3, 5, new SimpsonGenerator(2, 5), 1);

            Phase2 = new Phase(3, 4, new GaussGenerator(5, 1), 2);

            Phase3 = new Phase(3, 3, new LinearGenerator(3, 9), 3);

            Phase4 = new Phase(3, 4, new GaussGenerator(5, 1), 4);

            Phase5 = new Phase(3, 3, new LinearGenerator(3, 9), 5);

            CheckRequests = new List<double>();
            DiscardedRequests = new List<double>();

            RequestsCheckList = new List<Request>();
        }

        public int Start()
        {
            while (true)
            {
                if (CheckRequests.Count >= _requestCount)
                {
                    //if (IsExit())
                    {
                        break;
                    }
                }

                CheckLastPhase(Phase5, _time);

                ScheckPhase2(_time, Phase4, Phase5);
                ScheckPhase2(_time, Phase3, Phase4);
                ScheckPhase2(_time, Phase2, Phase3);
                ScheckPhase2(_time, Phase1, Phase2);

                CheckFirstPhase(Phase1, _time);

                if (CheckRequests.Count < _requestCount)
                {
                    GenerateRequest(_time);
                }

                _time += _dt;
            }

            return 0;
        }

        private void ScheckPhase2(double time, Phase phase, Phase nextPhase)
        {
            for (int i = 0; i < nextPhase.ChanelCount; i++)
            {
                if (nextPhase.SChannels[i].GetStatus(time) != 0) continue;
                if (nextPhase.SQueue.Pop() != true) continue;
                nextPhase.SChannels[i].PushRequest(time);

                var request = nextPhase.SQueue.PopRequest();
                nextPhase.SChannels[i].PushRequest(request);

            }

            for (int i = 0; i < phase.ChanelCount; i++)
            {
                if (phase.SChannels[i].GetStatus(time) == 2)
                {
                    var req = phase.SChannels[i].PopRequest();

                    if (this.PushToPhase(nextPhase, time, req))
                    {
                        phase.SChannels[i].Status = 0;
                        nextPhase.RequestCheck++;
                    }
                    else
                    {
                        if (nextPhase.SQueue.Push())
                        {
                            phase.SChannels[i].Status = 0;
                            nextPhase.SQueue.PushRequest(req);
                            nextPhase.RequestCheck++;
                        }
                    }
                }
            }
        }

        private void CheckLastPhase(Phase phase, double time)
        {
            for (int i = 0; i < phase.ChanelCount; i++)
            {
                if (_time > phase.SChannels[i].CheckTime && phase.SChannels[i].Status == 1)
                {
                    phase.SChannels[i].Status = 0;
                    CheckRequests.Add(time);

                    var request = phase.SChannels[i].PopRequest();
                    request.EndTime = time;
                    RequestsCheckList.Add(request);
                }
            }
        }

        private void CheckFirstPhase(Phase phase, double time)
        {
            for (int i = 0; i < phase.ChanelCount; i++)
            {
                if (phase.SQueue.QueueLength == 0) return;

                if (phase.SChannels[i].Status == 0)
                {
                    phase.RequestCheck += 1;
                    phase.SChannels[i].PushRequest(time);
                    phase.SQueue.Pop();

                    var request = phase.SQueue.PopRequest();
                    phase.SChannels[i].PushRequest(request);
                }
            }
        }

        private bool PushToPhase(Phase phase, double time, Request req)
        {
            foreach (var sChannel in phase.SChannels)
            {
                if (sChannel.GetStatus(time) == 0)
                {
                    sChannel.PushRequest(time);
                    sChannel.PushRequest(req);
                    return true;
                }
            }

            return false;
        }

        private void GenerateRequest(double time)
        {
            if (_u.Generate(time))
            {
                RequestGenerate++;
                var request = new Request(time);
                foreach (var sChannel in Phase1.SChannels)
                {
                    if (sChannel.GetStatus(time) == 0)
                    {
                        sChannel.PushRequest(time);
                        sChannel.PushRequest(request);
                        
                        Phase1.RequestCheck++;
                        
                        return;
                    }
                }

                if (Phase1.SQueue.Push() == true)
                {
                    Phase1.SQueue.PushRequest(request);
                    
                    //Phase1.RequestCheck++;
                }
                else
                {
                    DiscardedRequests.Add(time);
                }
            }
        }

        private bool IsExit()
        {
            return Phase1.IsStop() && (Phase2.IsStop() && (Phase3.IsStop() && (Phase4.IsStop() && Phase5.IsStop())));
        }
    }
}
