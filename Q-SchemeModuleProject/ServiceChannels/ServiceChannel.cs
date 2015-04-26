using Q_SchemeModuleProject.Generators;

namespace Q_SchemeModuleProject.ServiceChannels
{
    public class ServiceChannel
    {
        private readonly Generator _generator;

        private double _checkTime;
        private double _handlingTime;

        /// <summary>
        /// Status = 0 - свододен
        /// Status = 1 - обрабатывает заявку
        /// Status = 2 - не может передать завку дальше 
        /// </summary>
        public int Status { get; set; }

        public ServiceChannel(Generator generator)
        {
            _generator = generator;
            _checkTime = 0.0;
            _handlingTime = 0.0;
            Status = 0;
        }

        public void PushRequest(double currentTime)
        {
            if (currentTime <= _checkTime)
            {
                return;
            }

            _handlingTime = _generator.Generate();
            _checkTime = currentTime + _handlingTime;
            
            Status = 1;
        }


        /// <summary>
        /// Проверка статуса Канала 
        /// </summary>
        /// <returns>
        ///      1 : канал занят
        ///      0 : канал свободен
        /// </returns>
        public int GetStatus(double currentTime)
        {
            //if (_checkTime == 0.0) { return 1; }

            if (currentTime > _checkTime && Status == 1) { Status = 2; } 
            else if (currentTime < _checkTime) { Status = 1; }
            else if (currentTime > _checkTime) { Status = 0;}

            return Status;
        }
    }
}
