using Lidgren.Network;
using System;
using System.Net.Sockets;
using design_patterns.Utils;

namespace design_patterns.Messaging; 

public class LocalPeer {
    private readonly NetPeer _peer;
    
    public delegate void MessageReceivedEventHandler(string message);
    public event MessageReceivedEventHandler MessageReceived;

    public LocalPeer() {
        var config = new NetPeerConfiguration("super-hackers") {
            Port = Settings.Port,
            AcceptIncomingConnections = true
        };
        _peer = new NetClient(config);
        _peer.Start();
        Console.WriteLine($"Peer started on port {_peer.Port}");
        var netThread = new Thread(new ThreadStart(ProcessNet));
        netThread.Start();
    }

    private void ProcessNet()
    {
        Console.WriteLine("NetWorker started, waiting for messages...");
        while (true) {
            while (_peer.ReadMessage() is { } msg)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        MessageReceived?.Invoke(msg.ReadString());
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
                        _peer.Connect(msg.SenderEndPoint);
                        break;
                    // case NetIncomingMessageType.WarningMessage:
                    //     Console.WriteLine($"Warning message from {msg.SenderEndPoint}: {msg.ReadString()}");
                    //     break;
                    default:
                        Console.WriteLine("Unhandled type: " + msg.MessageType);
                        break;
                }

                _peer.Recycle(msg);
            }
            Thread.Sleep(1000);
        }
    }
    
    public void SendMessage(string message) {
        NetOutgoingMessage outgoingMessage = _peer.CreateMessage();
        outgoingMessage.Write(message);
        if (_peer.Connections.Count < 1) {
            Console.WriteLine("No connections");
        }
        else {
            _peer.SendMessage(outgoingMessage, _peer.Connections[0], NetDeliveryMethod.ReliableOrdered);
        }
    }
    
    public void DiscoverLocalPeers(int port) {
        Console.WriteLine("Discovering peers...");
        _peer.DiscoverLocalPeers(port);
    }
}