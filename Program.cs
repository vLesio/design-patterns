
using System;
using design_patterns.Messaging;
using design_patterns.Utils;

public class PeerApp {
    public static void Main(string[] args) {

        var app = new PeerApp();
        app.ParseArguments(args);
        
        Peer.Instance.Start();

        while (true) {
            Thread.Sleep(500);
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