namespace bookcollection.Models
{
    public class GoogleBooksResponse
    {
        public List<BookItem> items { get; set; }
    }

    public class BookItem
    {
        public VolumeInfo volumeInfo { get; set; }
    }
    public class VolumeInfo
    {
        public string title { get; set; }
        public string[] authors { get; set; }
        public string description { get; set; }
        public ImageLinks imageLinks { get; set; }
    }

    public class ImageLinks
    {
        public string smallThumbnail { get; set; }
        public string thumbnail { get; set; }
    }
}
