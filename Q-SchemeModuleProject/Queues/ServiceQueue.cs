namespace Q_SchemeModuleProject.Queues
{
    public class ServiceQueue
    {
        public int QueueLength { get; private set; }

        public int MaxQueueLength { get; private set; }

        public ServiceQueue(int queueLength, int maxQueueLength)
        {
            QueueLength = queueLength;
            MaxQueueLength = maxQueueLength;
        }

        public void PushRequest()
        {
            if (QueueLength > MaxQueueLength)
            {
                return;
            }

            QueueLength++;
        }

        public int PopReqest()
        {
            if (QueueLength <= 0)
            {
                return -1;
            }

            QueueLength--;
            {
                return 1;
            }
        }

        public int GetStatus()
        {
            return (QueueLength > MaxQueueLength ? -1 : 0);
        }
    }
}
