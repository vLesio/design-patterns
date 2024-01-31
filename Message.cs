public enum MessageType
{
    RangeReservation,
    RangeCompletion,
    NewTask
}

public class Message
{
    public MessageType MessageType { get; set; }
    public string Content { get; set; }

    public Message(MessageType messageType, string content)
    {
        MessageType = messageType;
        Content = content;
    }
}
