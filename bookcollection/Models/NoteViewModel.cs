namespace bookcollection.Models
{
    public class NoteViewModel
    {
        public Book ThisBook { get; set; } = new Book();
        public Note NewNote { get; set; } = new Note();
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
