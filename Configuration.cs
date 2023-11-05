namespace Adocao;

public static class Configuration
{
    public static string Secret = "NzBhODQ5OWEtNDdjNi00ZTM5LTk1N2EtYTAzMDhkMmVhNDc3=";
    public static ConfiguracaoSmtp Smtp = new();
    public static string BlobConnectionString;
    public static string ConnectionString;
    public class ConfiguracaoSmtp
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string Username { get; set; }
        public string Password { get; set; }
    }
}