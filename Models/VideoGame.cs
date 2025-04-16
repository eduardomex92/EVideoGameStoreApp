using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EVideoGameStoreApp.Data;


using EVideoGameStoreApp.Data.Enums;

namespace EVideoGameStoreApp.Models
{
    public class VideoGame
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public decimal Price { get; set; }

        public string CoverImageUrl { get; set; }

        public string Description { get; set; }

        public VideoGameCategory VideoGameCategory { get; set; }

        // relationships
        //developer
        public int DeveloperId { get; set; }
        [ForeignKey("DeveloperId")]
        public Developer Developer { get; set; }

        //publisher
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }

        //platform
        public ICollection<VideoGamePlatform> VideoGamePlatforms { get; set; }
    }
}
