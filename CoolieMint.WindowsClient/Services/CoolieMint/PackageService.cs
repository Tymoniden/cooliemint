using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoolieMint.WindowsClient.Services.CoolieMint
{
    public class PackageService : IPackageService
    {
        public Version InspectPackage(string path)
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
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
