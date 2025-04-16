using System.ComponentModel.DataAnnotations;

namespace EVideoGameStoreApp.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }

        [Required]

        [Display(Name = "Platform Name")]
        public string Name { get; set; }
        [Display(Name = "Platform Type")]
        public string Description { get; set; }
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }


        [Display(Name = "Platform Logo")]
        public string LogoUrl { get; set; }

        [Required(ErrorMessage = "Logo URL is required")]



        //relationships
        public ICollection<VideoGamePlatform> VideoGamePlatforms { get; set; }

    }
}
