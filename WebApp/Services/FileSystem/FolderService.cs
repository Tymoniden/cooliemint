using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebControlCenter.Services.FileSystem
{
    public class FolderService : IFolderService
    {
        private readonly IFileSystemService _fileSystemService;

        public FolderService(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
        }

        public void EnsureFolderContent(FolderContentStrategy folderContentStrategy)
        {
            if (folderContentStrategy is null)
            {
                throw new ArgumentNullException(nameof(folderContentStrategy));
            }

            if (!_fileSystemService.DirectoryExists(folderContentStrategy.Path))
            {
                throw new FileNotFoundException();
            }

            var files = new List<FileInfo>();

            if (CleanupNecessary(folderContentStrategy))
            {
                files = GetFilesToRemove(folderContentStrategy);
            }

            RemoveFiles(files);
        }

        void RemoveFiles(List<FileInfo> files)
        {
            if (!files.Any())
            {
                return;
            }

            foreach(var file in files)
            {
                file.Delete();
            }
        }

        bool CleanupNecessary(FolderContentStrategy folderContentStrategy)
        {
            if (folderContentStrategy.DetectionStrategy == DetectionStrategy.BySize)
            {
                if (folderContentStrategy.Size < _fileSystemService.GetFolderContentSize(folderContentStrategy.Path))
                {
                    return true;
                }
            }

            if (folderContentStrategy.DetectionStrategy == DetectionStrategy.ByCount)
            {
                if (folderContentStrategy.Count < _fileSystemService.GetFilesInFolder(folderContentStrategy.Path).Count)
                {
                    return true;
                }
            }

            return false;
        }

        List<FileInfo> GetFilesToRemove(FolderContentStrategy folderContentStrategy)
        {
            var files = _fileSystemService.GetFilesInFolder(folderContentStrategy.Path);

            switch (folderContentStrategy.CleanUpStrategy)
            {
                case CleanUpStrategy.ByDateAsc:
                    return RemoveFilesByDateAsc(folderContentStrategy, files);

                case CleanUpStrategy.ByDateDsc:
                    return RemoveFilesByDateDsc(folderContentStrategy, files);

                case CleanUpStrategy.BySize:
                    return RemoveFilesBySize(folderContentStrategy, files);
            }

            return new List<FileInfo>();
        }

        bool CleanupSufficient(FolderContentStrategy folderContentStrategy, List<FileInfo> files, List<FileInfo> removedFiles)
        {
            switch (folderContentStrategy.DetectionStrategy)
            {
                case DetectionStrategy.ByCount:
                    return files.Count - removedFiles.Count <= folderContentStrategy.Count;
                case DetectionStrategy.BySize:
                    return files.Sum(file => file.Length) - removedFiles.Sum(f => f.Length) <= folderContentStrategy.Size;
                default:
                    throw new ArgumentException(nameof(folderContentStrategy.DetectionStrategy));
            }
        }

        List<FileInfo> RemoveFilesBySize(FolderContentStrategy folderContentStrategy, List<FileInfo> files)
        {
            var filesToRemove = new List<FileInfo>();

            foreach (var file in files.OrderByDescending(f => f.Length))
            {
                filesToRemove.Add(file);

                if (CleanupSufficient(folderContentStrategy, files, filesToRemove))
                {
                    break;
                }
            }

            return filesToRemove;
        }

        List<FileInfo> RemoveFilesByDateAsc(FolderContentStrategy folderContentStrategy, List<FileInfo> files)
        {
            var filesToRemove = new List<FileInfo>();

            foreach (var file in files.OrderBy(f => f.CreationTimeUtc))
            {
                filesToRemove.Add(file);

                if (CleanupSufficient(folderContentStrategy, files, filesToRemove))
                {
                    break;
                }
            }

            return filesToRemove;
        }

        List<FileInfo> RemoveFilesByDateDsc(FolderContentStrategy folderContentStrategy, List<FileInfo> files)
        {
            var filesToRemove = new List<FileInfo>();

            foreach (var file in files.OrderByDescending(f => f.CreationTimeUtc))
            {
                filesToRemove.Add(file);

                if (CleanupSufficient(folderContentStrategy, files, filesToRemove))
                {
                    break;
                }
            }

            return filesToRemove;
        }
    }
}
