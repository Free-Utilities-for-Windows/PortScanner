namespace PortScanner.Context;

public static class SaveToFile
{
    private static object lockObject = new object();
    private static string defaultOutput;

    static SaveToFile()
    {
        string defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PortScanner");
        Directory.CreateDirectory(defaultPath);
        string defaultFilename = $"Scan_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
        defaultOutput = Path.Combine(defaultPath, defaultFilename);
    }

    public static void WriteToFile(string message)
    {
        lock (lockObject)
        {
            using (StreamWriter writer = new StreamWriter(defaultOutput, append: true))
            {
                writer.WriteLine(message);
            }
        }
    }
}