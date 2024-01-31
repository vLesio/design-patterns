using Lidgren.Network;

namespace design_patterns.Messaging; 

public class NetWorker {
    private readonly NetPeer _peer;
    private object _customContext;

    public NetWorker(NetPeer peer, object context)
    {
        this._peer = peer;
        this._customContext = context;
    }

    public void ProcessNet()
    {
        Console.WriteLine("NetWorker started, waiting for messages...");
        while (true) {
            while (_peer.ReadMessage() is { } msg)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        string text = msg.ReadString();
                        Console.WriteLine($"Message received from {msg.SenderEndPoint}: {text}");
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        Console.WriteLine($"Status of {msg.SenderConnection.Peer} changed: {msg.SenderConnection.Status}");
                        break;
                    case NetIncomingMessageType.DiscoveryRequest:
                        Console.WriteLine($"Receiver DiscoveryRequest from {msg.SenderEndPoint}");
                        _peer.SendDiscoveryResponse(_peer.CreateMessage($"{_peer.Socket} ACK"), msg.SenderEndPoint);
                        break;
                    case NetIncomingMessageType.DiscoveryResponse:
                        Console.WriteLine("Received discovery response from " + msg.SenderEndPoint + ": " + msg.ReadString());
                        break;
                    default:
                        Console.WriteLine("Unhandled type: " + msg.MessageType);
                        break;
                }

                _peer.Recycle(msg);
            }
            Thread.Sleep(1000);
        }
    }
}
