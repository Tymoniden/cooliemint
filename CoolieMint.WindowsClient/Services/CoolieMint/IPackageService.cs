using System;

namespace CoolieMint.WindowsClient.Services.CoolieMint
{
    public interface IPackageService
    {
        Version InspectPackage(string path);
    }
}