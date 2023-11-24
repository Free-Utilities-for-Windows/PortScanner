using System.Net.Sockets;
using PortScanner.Context;
using PortScanner.Scripts;

namespace PortScanner.Models;

public class AllPortsScanner : PortScanner
{
    private HttpEnumScript _httpScript;
    private SemaphoreSlim _semaphore;

    public AllPortsScanner()
    {
        _httpScript = new HttpEnumScript();
        _semaphore = new SemaphoreSlim(100);
    }

    public override async Task ScanAsync(string host)
    {
        var tasks = new List<Task>();

        for (int port = 1; port <= 65535; port++)
        {
            await _semaphore.WaitAsync();

            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    using (TcpClient tcpClient = new TcpClient())
                    {
                        await tcpClient.ConnectAsync(host, port);

                        string service = PortScannerService(port);
                        Console.WriteLine($"Port {port} is open. Service: {service}");
                        SaveToFile.WriteToFile($"Port {port} is open. Service: {service}");

                        await _httpScript.ExecuteAsync(host, port);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Port {port} is closed");
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