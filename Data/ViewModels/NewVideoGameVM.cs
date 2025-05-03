using System.ComponentModel.DataAnnotations;
using EVideoGameStoreApp.Data.Enums;

namespace EVideoGameStoreApp.Models
{
    public class NewVideoGameVM
    {
        public int Id { get; set; }

        [Display(Name = "Video Game Title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "Release date is required")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Display(Name = "Cover Image URL")]
        [Required(ErrorMessage = "Cover image is required")]
        public string CoverImageUrl { get; set; }

        [Display(Name = "Game Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Select a Category")]
        [Required(ErrorMessage = "Category is required")]
        public List<VideoGameCategory> VideoGameCategories { get; set; }

        // Relationships

        [Display(Name = "Select a Developer")]
        [Required(ErrorMessage = "Developer is required")]
        public int DeveloperId { get; set; }

        [Display(Name = "Select a Publisher")]
        [Required(ErrorMessage = "Publisher is required")]
        public int PublisherId { get; set; }

        [Display(Name = "Select Platform(s)")]
        [Required(ErrorMessage = "At least one platform is required")]
        public List<int> PlatformIds { get; set; } // Instead of full objects
    }
}
