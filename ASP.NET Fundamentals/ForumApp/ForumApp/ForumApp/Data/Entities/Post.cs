using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static ForumApp.Data.DataConstants.Post;

namespace ForumApp.Data.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }
    }
}
