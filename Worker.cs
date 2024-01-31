using System.Threading;

public class Worker
{
    private readonly WorkManager workManager;

    public Worker(WorkManager workManager)
    {
        this.workManager = workManager;
    }

    public void StartThread()
    {
        Thread workerThread = new Thread(new ThreadStart(WorkLoop));
        workerThread.Start();
    }

    private void WorkLoop()
    {
        while (true)
        {
            // Brute force logic using workManager.encryptionStrategy.Encrypt()
            // Check if result matches targetHash
            // Broadcast completion and get next range
            workManager.peer.Broadcast(new Message(MessageType.RangeCompletion, "Range Completed"));
            var nextRange = workManager.rangeIterator.GetNext();
            workManager.peer.Broadcast(new Message(MessageType.RangeReservation, "New Range Taken"));
        }
    }
}
