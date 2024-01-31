Peer peer = Peer.Instance;
WorkManager workManager = new WorkManager(peer);
peer.CreateMessageHandler(workManager);
peer.ListenForMessages();