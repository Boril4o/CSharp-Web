using Library.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Library.Data.DataConstants.Book;

namespace Library.Models
{
    public class AddBookViewModel
    {
        [Required]
        [MaxLength(MaxTitleLength)]
        [MinLength(MinTitleLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(MaxAuthorLength)]
        [MinLength(MinAuthorLength)]
        public string Author { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        [MinLength(MinDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(type: typeof(decimal), minimum: MinRating, maximum: MaxRating)]
        public decimal Rating { get; set; }

        public int CategoryId { get; set; }
        

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
