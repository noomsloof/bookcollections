using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace bookcollection.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? BookID { get; set; }

        [DisplayName("โน๊ต")]
        public string? Content { get; set; }

        [DisplayName("รูป")]
        public string? Image64 { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
