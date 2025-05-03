using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using EVideoGameStoreApp.Data.Base;

namespace EVideoGameStoreApp.Models
{
    public class Platform : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Platform name is required")]
        [StringLength(100, ErrorMessage = "Platform name cannot exceed 100 characters.")]
        [Display(Name = "Platform Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Display(Name = "Platform Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Manufacturer is required")]
        [StringLength(100, ErrorMessage = "Manufacturer name cannot exceed 100 characters.")]
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Logo URL is required")]
        [Display(Name = "Platform Logo")]
        public string LogoUrl { get; set; }

        // relationships
        [ValidateNever]
        public ICollection<VideoGamePlatform> VideoGamePlatforms { get; set; }
    }
}
