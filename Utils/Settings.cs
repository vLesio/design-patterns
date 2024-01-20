namespace design_patterns.Utils;
using System.Text.Json;

public static class Settings {
    public const int DefaultPort = 42069;
    public static int Port = DefaultPort;
    public static int ConnectToPort = DefaultPort;
}