using System.Diagnostics;

var appDirectory = AppDomain.CurrentDomain.BaseDirectory;

var directory = Directory.GetDirectories(appDirectory)
    .Select(path => new DirectoryInfo(path))
    .Where(d => d.Name.StartsWith("CoolieMint_"))
    .OrderByDescending(d => d.Name)
    .FirstOrDefault();

if(directory == null)
{
    Console.WriteLine("No directory found.");
    return;
}

var targetFile = directory.GetFiles().FirstOrDefault(f => f.Name.Equals("WebControlCenter"));
if(targetFile == null)
{
    Console.WriteLine("Target file not found.");
    return;
}

var processStartInfo = new ProcessStartInfo
{
    FileName = targetFile.FullName,
    WorkingDirectory = targetFile.DirectoryName
};

Console.WriteLine($"Starting file: {processStartInfo.FileName}");
var process = Process.Start(processStartInfo);
process?.WaitForExit();

Console.WriteLine($"{processStartInfo.FileName} exited with code: {process.ExitCode}");
Environment.Exit(process?.ExitCode ?? 0);
