using System.ComponentModel.DataAnnotations;

namespace EVideoGameStoreApp.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        [Required]

        [Display(Name = "Publisher Name")]
        public string Name { get; set; }

        [Display(Name = "Headquarters")]
        public string Headquarters { get; set; }

        [Display (Name = "Logo URL")]
        public string LogoUrl { get; set; }

        public ICollection<VideoGame> VideoGames { get; set; }

        //relatioships
        public ICollection<DeveloperPublisher> DeveloperPublishers { get; set; } = new List<DeveloperPublisher>();

    }
}
