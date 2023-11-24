using System.Net.Sockets;
using PortScanner.Context;
using PortScanner.Scripts;

namespace PortScanner.Models;

public class RangePortScanner : PortScanner
{
    private int _startPort;
    private int _endPort;
    private HttpEnumScript _httpScript;
    private SemaphoreSlim _semaphore;

    public RangePortScanner(int startPort, int endPort)
    {
        _startPort = startPort;
        _endPort = endPort;
        _httpScript = new HttpEnumScript();
        _semaphore = new SemaphoreSlim(100);
    }

    public override async Task ScanAsync(string host)
    {
        var tasks = new List<Task>();

        for (int port = _startPort; port <= _endPort; port++)
        {
            await _semaphore.WaitAsync();

            int localPort = port;

            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    using (TcpClient tcpClient = new TcpClient())
                    {
                        await tcpClient.ConnectAsync(host, localPort);
                        string service = PortScannerService(localPort);
                        Console.WriteLine($"Port {localPort} is open. Service: {service}");
                        SaveToFile.WriteToFile($"Port {localPort} is open. Service: {service}");

                        await _httpScript.ExecuteAsync(host, localPort);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Port {localPort} is closed");
                }
                finally
                {
                    _semaphore.Release();
                }
            }));
        }

        await Task.WhenAll(tasks);
    }
}