using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystem.Data.DataConstants;

namespace HouseRentingSystem.Data.Entities
{
    public class House
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(HouseTitleMaxLength)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(HouseAddressMaxLength)]
        public string Address { get; set; }

        [Required]
        [MaxLength(HouseDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Range(typeof(decimal), HousePricePerMonthMinLength, HousePricePerMonthMaxLength)]
        public decimal PricePerMonth { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }

        [ForeignKey(nameof(Agent))]
        public int AgentId { get; set; }
        [Required]
        public Agent Agent { get; set; }

        public string? RenterId { get; set; }
    }
}
