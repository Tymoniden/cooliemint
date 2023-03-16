// See https://aka.ms/new-console-template for more information
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;

Console.WriteLine("Hello, World!");

var publishFolder = @"C:\Users\timos\Git\cooliemint\CoolieMint.WebApp\bin\Release\net6.0\publish";
var archivePath = Path.Combine(Path.GetTempPath(), "CoolieMint.zip");

if (File.Exists(archivePath))
{
	Console.WriteLine("Deleting existing archive.");
	File.Delete(archivePath);
}

Console.WriteLine($"Adding folder {publishFolder} to archive {archivePath}.");
using (FileStream zipFile = File.Open(archivePath, FileMode.Create))
{
	using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Create))
	{
		AddDirectoryToArchive(archive, publishFolder, string.Empty);
	}
}


Console.WriteLine("Uploading archive.");
var returnValue = UploadArchive(archivePath).Result;
Console.Write("Service returned:");
Console.WriteLine(returnValue);

Console.WriteLine("Deleting archive.");
File.Delete(archivePath);

void AddDirectoryToArchive(ZipArchive archive, string directory, string targetFolder)
{
	foreach(var file in Directory.GetFiles(directory).Select(filename => new FileInfo(filename)))
    {
		var entryPath = Path.Combine(targetFolder, file.Name);

        archive.CreateEntryFromFile(file.FullName, entryPath);
    }

	foreach(var subDirectory in Directory.GetDirectories(directory).Select(d => new DirectoryInfo(d)))
    {
		AddDirectoryToArchive((ZipArchive)archive, subDirectory.FullName, Path.Combine(targetFolder, subDirectory.Name));
    }
}

async Task<string> UploadArchive(string archivePath){
	HttpClient client = new HttpClient();
	// Update port # in the following line.
	client.BaseAddress = new Uri("http://192.168.2.128/");
	client.DefaultRequestHeaders.Accept.Clear();
	client.DefaultRequestHeaders.Accept.Add(
		new MediaTypeWithQualityHeaderValue("application/json"));

	var upgradeModel = new 
	{
		Version = InspectPackage(archivePath).ToString(),
		Content = File.ReadAllBytes(archivePath)
	};

	HttpResponseMessage response = await client.PostAsJsonAsync($"api/v2/Upgrade", upgradeModel);
	response.EnsureSuccessStatusCode();
	return await response.Content.ReadAsStringAsync();
}

Version InspectPackage(string path)
{
    try
    {
        var archive = ZipFile.OpenRead(path);
        var entry = archive.GetEntry("CoolieMint.WebApp.dll");

        var targetFile = Path.Combine(Path.GetTempPath(), entry.Name);
        if (File.Exists(targetFile))
        {
            File.Delete(targetFile);
        }

        entry.ExtractToFile(targetFile);
        var assemblyName = AssemblyName.GetAssemblyName(targetFile);
        return assemblyName.Version;
    }
    catch (Exception e)
    {
        return null;
    }
}