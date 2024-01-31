public class MessageHandler
{
    private readonly WorkManager workManager;

    public MessageHandler(WorkManager workManager)
    {
        this.workManager = workManager;
    }

    public void HandleMessage(Message message)
    {
        switch (message.MessageType)
        {
            case MessageType.RangeReservation:
                // Handle range reservation logic
                break;
            case MessageType.RangeCompletion:
                // Handle range completion logic
                workManager.UpdateRanges();
                break;
            case MessageType.NewTask:
                // Extract new encryption algorithm and target hash from the message
                string newEncryptionAlgo = ""; // Extract from message.Content
                string newTargetHash = ""; // Extract from message.Content
                workManager.SetTask(newEncryptionAlgo, newTargetHash);
                break;
        }
    }
}
