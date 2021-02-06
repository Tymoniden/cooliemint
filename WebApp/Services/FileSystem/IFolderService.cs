namespace WebControlCenter.Services.FileSystem
{
    public interface IFolderService
    {
        void EnsureFolderContent(FolderContentStrategy folderContentStrategy);
    }
}