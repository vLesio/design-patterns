using System.Collections.Generic;
using System.Net;

public class Peer
{
    private static Peer _instance;
    private readonly List<IPEndPoint> peers;
    private MessageHandler messageHandler;

    // Singleton instance
    public static Peer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Peer();
            }
            return _instance;
        }
    }

    // Private constructor to enforce singleton pattern
    private Peer()
    {
        peers = new List<IPEndPoint>();
    }

    public void CreateMessageHandler(WorkManager workManager)
    {
        messageHandler = new MessageHandler(workManager);
    }

    // Method to broadcast a message to all peers
    public void Broadcast(Message message)
    {
        foreach (var peer in peers)
        {
            // Code to send the message using Lidgren.Network
        }
    }

    // Method to listen for incoming messages
    public void ListenForMessages()
    {
        // Code to listen for messages using Lidgren.Network
        // On receiving a message, invoke messageHandler.HandleMessage()
    }

    // Method to add a new peer
    public void AddPeer(IPEndPoint newPeer)
    {
        if (!peers.Contains(newPeer))
        {
            peers.Add(newPeer);
        }
    }
}
