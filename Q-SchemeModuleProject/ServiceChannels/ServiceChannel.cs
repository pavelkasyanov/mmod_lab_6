using System;
using Q_SchemeModuleProject.Generators;

namespace Q_SchemeModuleProject.ServiceChannels
{
    public class ServiceChannel
    {
        private readonly Generator _generator;

        public double CheckTime { get; set; }
        private double _handlingTime;

        private readonly double EPS = 0.0000000000000000000000001;

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
        }

        public void PushRequest(double currentTime)
        {
            _handlingTime = _generator.Generate();
            CheckTime = currentTime + _handlingTime;
            
            Status = 1;
        }


        public int GetStatus(double currentTime)
        {
            if (currentTime > CheckTime && Status == 1)
            {
                //this.Status = 2;
                return 2;
            }
            //else if (currentTime < CheckTime) { Status = 1;}
            //else if (currentTime > CheckTime) { Status = 0;}

            return this.Status;
        }
    }
}
