﻿using System;

namespace CoolieMint.WebApp.Services.FileSystem
{
    public class UpgradeService
    {
        private readonly IDirectoryProvider _directoryProvider;
        private readonly IFileSystemService _fileSystemService;

        public UpgradeService(IDirectoryProvider directoryProvider, IFileSystemService fileSystemService)
        {
            _directoryProvider = directoryProvider ?? throw new ArgumentNullException(nameof(directoryProvider));
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
        }

        public void SavePackage(string version, byte[] content)
        {
            var destinationFolder = _directoryProvider.RelativeWebRoot("..");
            var destinationFilename = _fileSystemService.CombinePath(destinationFolder, $"Cooliemint_{version}");

            _fileSystemService.WriteAllBytes(destinationFilename, content);
        }
    }
}