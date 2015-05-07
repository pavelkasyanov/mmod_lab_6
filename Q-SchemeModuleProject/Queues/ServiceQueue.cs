using System.Collections.Generic;

namespace Q_SchemeModuleProject.Queues
{
    public class ServiceQueue
    {
        public int QueueLength { get; private set; }

        public int MaxQueueLength { get; private set; }

        public Queue<Request> RequestsQueue;

        public ServiceQueue(int queueLength, int maxQueueLength)
        {
            QueueLength = queueLength;
            MaxQueueLength = maxQueueLength;

            RequestsQueue = new Queue<Request>(maxQueueLength);
        }

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

        public void PushRequest(Request request)
        {
            RequestsQueue.Enqueue(request);
        }

        public Request PopRequest()
        {
            if (RequestsQueue.Count == 0) return null;

            var t = RequestsQueue.Dequeue();
            RequestsQueue.TrimExcess();
            
            return t;
        }
    }
}
