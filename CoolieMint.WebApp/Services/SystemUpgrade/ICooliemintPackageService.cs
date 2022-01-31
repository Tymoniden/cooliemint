using System;

namespace CoolieMint.WebApp.Services.SystemUpgrade
{
    public interface ICooliemintPackageService
    {
        void SavePackage(byte[] content);
        bool TryInspectVersion(byte[] package, out Version version);
    }
}