using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using EVideoGameStoreApp.Data.Base;

namespace EVideoGameStoreApp.Models
{
    public class Publisher : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Publisher name is required")]
        [Display(Name = "Publisher Name")]
        public string Name { get; set; }

        [Display(Name = "Headquarters")]
        public string Headquarters { get; set; }

        [Display(Name = "Logo URL")]
        public string LogoUrl { get; set; }

        [ValidateNever]
        public ICollection<VideoGame> VideoGames { get; set; }

        [ValidateNever]
        public ICollection<DeveloperPublisher> DeveloperPublishers { get; set; } = new List<DeveloperPublisher>();
    }
}
