
using System;
using design_patterns.Messaging;
using design_patterns.Utils;

public class PeerApp {
    public static void Main(string[] args) {

        var app = new PeerApp();
        app.ParseArguments(args);

        var peer = new LocalPeer();
        var messageHandler = new MessageHandler(peer);

        while (true) {
            peer.DiscoverLocalPeers(Settings.ConnectToPort);
            Thread.Sleep(3000);
            peer.SendMessage("dupeczka");
        }
    }

    private void ParseArguments(string [] args) {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PeerApp [port] [destPort]");
            Environment.Exit(1);
        }

        try {
            int port = int.Parse(args[0]);
            Settings.Port = port;
            Console.WriteLine("Port set to " + port);
        } catch (FormatException) {
            Console.WriteLine("Port must be an integer");
            Environment.Exit(1);
        }
        try {
            int port = int.Parse(args[1]);
            Settings.ConnectToPort = port;
            Console.WriteLine("Remote host port set to " + port);
        } catch (FormatException) {
            Console.WriteLine("Destination port must be an integer");
            Environment.Exit(1);
        }
    }
}