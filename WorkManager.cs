public class WorkManager
{
    public Peer peer;
    public Worker worker;
    public TaskRangeIterator rangeIterator;
    public EncryptionStrategy encryptionStrategy;
    public string targetHash;

    public WorkManager(Peer peer)
    {
        this.peer = peer;
        this.worker = new Worker(this);
        this.rangeIterator = new TaskRangeIterator(this);
        // Set a default encryption strategy, e.g., SHA1
        this.encryptionStrategy = new SHA1EncryptionStrategy();
    }

    public void UpdateRanges()
    {
        rangeIterator.Update();
    }

    public void SetTask(string newEncryptionAlgo, string newTargetHash)
    {
        // Change encryption strategy based on newEncryptionAlgo
        // Example: if(newEncryptionAlgo == "SHA1") encryptionStrategy = new SHA1EncryptionStrategy();
        this.targetHash = newTargetHash;
    }
}
