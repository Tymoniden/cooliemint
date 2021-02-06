namespace WebControlCenter.Services.FileSystem
{
    public class FolderContentStrategy
    {
        public string Path { get; set; }

        public DetectionStrategy DetectionStrategy { get; set; }

        public CleanUpStrategy CleanUpStrategy { get; set; }

        public long Size { get; set; }

        public int Count { get; set; }
    }
}
