using Library.Data.Entities;
using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants.Book;

namespace Library.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public decimal Rating { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string ImageUrl { get; set; }
    }
}
