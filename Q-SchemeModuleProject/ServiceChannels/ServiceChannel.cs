using System;
using Q_SchemeModuleProject.Generators;

namespace Q_SchemeModuleProject.ServiceChannels
{
    public class ServiceChannel
    {
        private readonly Generator _generator;

        public double CheckTime { get; set; }
        private double _handlingTime;

        public int CheckedRequests;

        public double AllServiceTime;

        private Request _request;

        /// <summary>
        /// Status = 0 - свододен
        /// Status = 1 - обрабатывает заявку
        /// Status = 2 - не может передать завку дальше 
        /// </summary>
        public int Status;

        public ServiceChannel(Generator generator)
        {
            _generator = generator;
            CheckTime = 0.0;
            _handlingTime = 0.0;
            Status = 0;
            CheckedRequests = 0;
            AllServiceTime = 0;
        }

        public void PushRequest(double currentTime)
        {
            _handlingTime = _generator.Generate();
            CheckTime = currentTime + _handlingTime;
            
            Status = 1;

            //CheckedRequests += 1;

            AllServiceTime += _handlingTime;
        }

        public int GetStatus(double currentTime)
        {
            if (currentTime > CheckTime && Status == 1)
            {
                return 2;
            }
            return this.Status;
        }

        public void PushRequest(Request request)
        {
            _request = request;
            _request.ServiceTime += _handlingTime;

            CheckedRequests += 1;
        }

        public Request PopRequest()
        {
            //CheckedRequests += 1;

            return _request;
        }
    }
}
