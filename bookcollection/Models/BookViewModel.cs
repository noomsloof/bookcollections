namespace bookcollection.Models
{
    public class BookViewModel
    {
        public List<Book> Books { get; set; } = new List<Book>(); // รายการหนังสือ
        public Book NewBook { get; set; } = new Book(); // หนังสือใหม่สำหรับฟอร์ม
    }
}
