namespace PortScanner.Context;

public class PortScanContext
{
    private Models.PortScanner _portScanner;

    public PortScanContext(Models.PortScanner portScanner)
    {
        _portScanner = portScanner;
    }

    public async Task ExecuteScanAsync(string host)
    {
        await _portScanner.ScanAsync(host);
    }
}