using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;



namespace EVideoGameStoreApp.Models
{
    public class Developer
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }


        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required")]
        [StringLength(100, ErrorMessage = "Country cannot be longer than 100 characters.")]
        public string Country { get; set; }


        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }


        [Display(Name = "Logo")]
        [Required(ErrorMessage = "Logo is required")]
        public string LogoUrl { get; set; }

        [ValidateNever]
        public ICollection<VideoGame> VideoGames { get; set; }

        //relationships
        [ValidateNever]
        public ICollection<DeveloperPublisher> DeveloperPublishers { get; set; } = new List<DeveloperPublisher>();  
    }
}
