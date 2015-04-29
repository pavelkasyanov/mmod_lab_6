namespace Q_SchemeModuleProject.Queues
{
    public class ServiceQueue
    {
        public int QueueLength { get; private set; }

        public int MaxQueueLength { get; private set; }

        public bool Pop()
        {
            if (QueueLength > 0)
            {
                --QueueLength;
                return true;
            }

            return false;
        }

        public bool Push()
        {
            if (QueueLength < MaxQueueLength)
            {
                QueueLength++;
                return true;
            }

            return false;
        }

        public ServiceQueue(int queueLength, int maxQueueLength)
        {
            QueueLength = queueLength;
            MaxQueueLength = maxQueueLength;
        }

        public int GetStatus()
        {
            return (QueueLength > MaxQueueLength ? -1 : 0);
        }
    }
}
