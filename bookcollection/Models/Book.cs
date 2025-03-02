using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookcollection.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("ชื่อหนังสือ")]
        public string? BookName { get; set; }

        [Required]
        [DisplayName("ผู้เขียน")]
        public string? Author { get; set; }

        [Required]
        [DisplayName("คำอธิบาย")]
        public string? Description { get; set; }

        [Required]
        [DisplayName("ประเภท")]
        public string? Category { get; set; }

        [Required]
        [DisplayName("รูปหนังสือ")]
        public string? Image64 { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
