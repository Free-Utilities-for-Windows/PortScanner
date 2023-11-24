namespace PortScanner.Scripts;

public abstract class Script
{
    public abstract Task ExecuteAsync(string host, int port);
}