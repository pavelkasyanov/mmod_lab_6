using Q_SchemeModuleProject.Generators;

namespace Q_SchemeModuleProject.ServiceChannels
{
    public class ServiceChannel
    {
        private readonly Generator _generator;

        private double _checkTime;
        private double _handlingTime;

        public ServiceChannel(Generator generator)
        {
            _generator = generator;
            _checkTime = 0.0;
            _handlingTime = 0.0;
        }

        public void SetRequest(double currentTime)
        {
            if (currentTime <= _checkTime)
            {
                return;
            }

            _handlingTime = _generator.Generate();
            _checkTime = currentTime + _handlingTime;
        }


        /// <summary>
        /// Проверка статуса Канала 
        /// </summary>
        /// <returns>
        ///     -1 : канал занят
        ///      0 : канал свободен
        /// </returns>
        public int GetStatus(double currentTime)
        {
            return (currentTime <= _checkTime ? -1 : 0);
        }
    }
}
