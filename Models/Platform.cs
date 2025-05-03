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
        [Display(Name = "Platform Name")]
        public string Name { get; set; }

        [Display(Name = "Platform Type")]
        public string Description { get; set; }

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
