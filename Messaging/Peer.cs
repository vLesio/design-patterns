using Lidgren.Network;
using System;
using System.Net.Sockets;
using design_patterns.Utils;

namespace design_patterns.Messaging; 

public class Peer {
    
    private static Peer instance;
    private static readonly object Padlock = new object();
    
    public static Peer Instance
    {
        get
        {
            lock (Padlock)
            {
                if (instance == null)
                {
                    instance = new Peer();
                }
                return instance;
            }
        }
    }
   
    private NetPeer peer;
    
    private Peer() {
        var config = new NetPeerConfiguration("super-hackers") {
            Port = Settings.Port
        };
        peer = new NetPeer(config);
        peer.Start();
        Console.WriteLine($"Peer started on port {peer.Port}. Waiting for messages...");
    }

    public void ConnectToPeer(string host, int port) {
        try {
            Console.WriteLine($"Listening for local peers");
            peer.DiscoverLocalPeers(42069);
            // Sprawdzanie, czy połączenie jest nawiązane
            // if () {
            //     Console.WriteLine($"Successfully connected to {host}:{port}");
            // } else {
            //     Console.WriteLine($"Failed to connect to {host}:{port}. Connection was not established.");
            // }
        } catch (SocketException ex) {
            // Obsługa błędów związanych z gniazdem sieciowym
            Console.WriteLine($"SocketException: Could not connect to {host}:{port}. Error: {ex.Message}");
        } catch (Exception ex) {
            // Obsługa innych rodzajów wyjątków
            Console.WriteLine($"Exception: An error occurred while trying to connect to {host}:{port}. Error: {ex.Message}");
        }
    }

    public void SendMessage(string message) {
        NetOutgoingMessage outgoingMessage = peer.CreateMessage();
        outgoingMessage.Write(message);
        if (peer.Connections.Count < 1) {
            Console.WriteLine("No connections");
        }
        else {
            peer.SendMessage(outgoingMessage, peer.Connections[0], NetDeliveryMethod.ReliableOrdered);
        }
    }
    
    public void DiscoverLocalPeers(int port) {
        peer.DiscoverLocalPeers(port);
    }

    public void ListenForMessages() {
        NetIncomingMessage incomingMessage;
        while ((incomingMessage = peer.ReadMessage()) != null) {
            switch (incomingMessage.MessageType) {
                case NetIncomingMessageType.Data:
                    string text = incomingMessage.ReadString();
                    Console.WriteLine("Received: " + text);
                    break;

                case NetIncomingMessageType.StatusChanged:
                    // Handle status changes, e.g., when a connection is established
                    break;
                case NetIncomingMessageType.DiscoveryRequest:
                    peer.SendDiscoveryResponse(peer.CreateMessage("Hello"), incomingMessage.SenderEndPoint);
                    break;
                case NetIncomingMessageType.DiscoveryResponse:
                    Console.WriteLine("Received discovery response from " + incomingMessage.SenderEndPoint + ": " + incomingMessage.ReadString());
                    break;
                default:
                    Console.WriteLine("Unhandled type: " + incomingMessage.MessageType);
                    break;
            }

            peer.Recycle(incomingMessage);
        }
        Console.WriteLine("Finished listening for messages...");
    }
}