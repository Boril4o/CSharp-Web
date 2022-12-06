using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants.Category;

namespace Library.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
