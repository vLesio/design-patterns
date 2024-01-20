using System.Text.Json;
using design_patterns.Messaging.Exceptions;

namespace design_patterns.Messaging; 

public static class MessageParser {
    public static Message Deserialize(string message) {
        var messageObject = JsonSerializer.Deserialize<Message>(message);
        if (messageObject is null) {
            throw new MessageBadFormatException($"[{message}] is not a valid message.");
        }
        return messageObject;
    }
    
    public static string Serialize(Message message) {
        return JsonSerializer.Serialize(message);
    }
}