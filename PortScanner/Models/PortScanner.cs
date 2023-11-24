namespace PortScanner.Models;

public abstract class PortScanner
{
    public abstract Task ScanAsync(string host);

    public string PortScannerService(int i)
    {
        switch (i)
        {
            case 80:
                return "HTTP protocol proxy service";
            case 135:
                return "DCE endpoint resolutionnetbios-ns";
            case 445:
                return "Security service";
            case 1025:
                return "NetSpy.698(YAI)";
            case 8080:
                return "HTTP protocol proxy service";
            case 8081:
                return "HTTP protocol proxy service";
            case 3128:
                return "HTTP protocol proxy service";
            case 9080:
                return "HTTP protocol proxy service";
            case 1080:
                return "SOCKS protocol proxy service";
            case 21:
                return "FTP (file transfer) protocol proxy service";
            case 23:
                return "Telnet (remote login) protocol proxy service";
            case 443:
                return "HTTPS protocol proxy service";
            case 69:
                return "TFTP protocol proxy service";
            case 22:
                return "SSH, SCP, port redirection protocol proxy service";
            case 25:
                return "SMTP protocol proxy service";
            case 110:
                return "POP3 protocol proxy service";
            default:
                return "Unknown service";
        }
    }
}