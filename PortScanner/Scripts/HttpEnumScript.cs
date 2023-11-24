using System.Net;

namespace PortScanner.Scripts;

public class HttpEnumScript : Script
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private static readonly string[] _commonPaths = { "admin", "login", "test", "backup", "index.html" };

    public override async Task ExecuteAsync(string host, int port)
    {
        foreach (var path in _commonPaths)
        {
            await CheckPathAsync($"http://{host}:{port}", path);
        }
    }

    private async Task CheckPathAsync(string baseUrl, string path)
    {
        var fullPath = $"{baseUrl}/{path}";

        try
        {
            var response = await _httpClient.GetAsync(fullPath, HttpCompletionOption.ResponseHeadersRead);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Found path: {fullPath}");

                if (path.EndsWith("/"))
                {
                    foreach (var commonPath in _commonPaths)
                    {
                        await CheckPathAsync(fullPath, commonPath);
                    }
                }
            }
            else if (response.StatusCode == HttpStatusCode.Redirect ||
                     response.StatusCode == HttpStatusCode.MovedPermanently)
            {
                var redirectUrl = response.Headers.Location.ToString();
                await CheckPathAsync(redirectUrl, "");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error when sending request to {fullPath}: {ex.Message}");
        }
    }
}