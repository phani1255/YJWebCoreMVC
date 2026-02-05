namespace YJWebCoreMVC.Models
{
    public class ThumbnailModel
    {
        public int ImageId { get; set; }
        public string FileName { get; set; }
        public string EntityId { get; set; }
        public string EntityType { get; set; }
        public string FileUrl { get; set; } // URL to access the file
        public bool IsPdf { get; set; }
    }
}
