using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstants;

namespace HouseRentingSystem.Data.Entities
{
    public class Category
    {
        public Category()
        {
            Houses = new List<House>();
        }

        [Key]
        public int Id { get; init; }
        
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }

        public List<House> Houses { get; init; }
    }
}
