using System.ComponentModel.DataAnnotations;

namespace WebApplicationDotNet.Models
{
    public class Car
    {
        [Key] 
        public int Id { get; set; }
        [Required(ErrorMessage = "Brand And Model is required")]
        public string BrandAndModel { get; set; }
        [Required(ErrorMessage = "Manufacture Date is required")]
        public DateTime ManufactureDate { get; set; }
        public bool IsAvailable { get; set; } = true;
        [Required(ErrorMessage = "Rental Price is required")]
        public decimal RentalPrice { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
