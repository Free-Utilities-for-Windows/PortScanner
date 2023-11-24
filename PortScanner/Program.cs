using PortScanner.Context;
using PortScanner.Models;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Enter the host to scan:");
        string host = Console.ReadLine();

        if (string.IsNullOrEmpty(host) ||
            (!Uri.CheckHostName(host).Equals(UriHostNameType.Dns) &&
             !Uri.CheckHostName(host).Equals(UriHostNameType.IPv4) &&
             !Uri.CheckHostName(host).Equals(UriHostNameType.IPv6)))
        {
            Console.WriteLine("Invalid host. Exiting.");
            return;
        }

        Console.WriteLine("Enter the scanning strategy (1 for single port, 2 for range, 3 for all ports):");
        string strategy = Console.ReadLine();

        PortScanner.Models.PortScanner scanner;
        if (strategy == "1")
        {
            Console.WriteLine("Enter the port to scan:");
            string portInput = Console.ReadLine();

            if (!int.TryParse(portInput, out int port) || port < 1 || port > 65535)
            {
                Console.WriteLine("Invalid port. Exiting.");
                return;
            }

            scanner = new SinglePortScanner(port);
        }
        else if (strategy == "2")
        {
            Console.WriteLine("Enter the start port:");
            string startPortInput = Console.ReadLine();
            Console.WriteLine("Enter the end port:");
            string endPortInput = Console.ReadLine();

            if (!int.TryParse(startPortInput, out int startPort) || startPort < 1 || startPort > 65535 ||
                !int.TryParse(endPortInput, out int endPort) || endPort < 1 || endPort > 65535 ||
                startPort > endPort)
            {
                Console.WriteLine("Invalid ports. Exiting.");
                return;
            }

            scanner = new RangePortScanner(startPort, endPort);
        }
        else if (strategy == "3")
        {
            scanner = new AllPortsScanner();
        }
        else
        {
            Console.WriteLine("Invalid strategy. Exiting.");
            return;
        }

        PortScanContext context = new PortScanContext(scanner);
        await context.ExecuteScanAsync(host);
    }
}