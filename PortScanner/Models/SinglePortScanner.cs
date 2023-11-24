using System.Net.Sockets;
using PortScanner.Context;
using PortScanner.Scripts;

namespace PortScanner.Models;

public class SinglePortScanner : PortScanner
{
    private int _port;
    private HttpEnumScript _httpScript;

    public SinglePortScanner(int port)
    {
        _port = port;
        _httpScript = new HttpEnumScript();
    }

    public override async Task ScanAsync(string host)
    {
        using (TcpClient tcpClient = new TcpClient())
        {
            try
            {
                await tcpClient.ConnectAsync(host, _port);

                string service = PortScannerService(_port);
                Console.WriteLine($"Port {_port} is open. Service: {service}");
                SaveToFile.WriteToFile($"Port {_port} is open. Service: {service}");

                await _httpScript.ExecuteAsync(host, _port);
            }
            catch (Exception)
            {
                Console.WriteLine($"Port {_port} is closed");
            }
        }
    }
}