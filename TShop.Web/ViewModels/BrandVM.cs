using System.ComponentModel.DataAnnotations;
namespace TShop.Web.ViewModels
{
    public class BrandVM
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        [Display(Name = "Brand Name")]
        [RegularExpression(@"^[a-zA-Z0-9\s_-]+$", ErrorMessage = "Name can only contain letters, numbers, and spaces.")]
        public string Name { get; set; }
    }
}