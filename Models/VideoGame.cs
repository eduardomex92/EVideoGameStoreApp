using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using EVideoGameStoreApp.Data.Base;
using EVideoGameStoreApp.Data.Enums;

namespace EVideoGameStoreApp.Models
{
    public class VideoGame : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000.00, ErrorMessage = "Price must be between $0.01 and $1000")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Display(Name = "Cover Image URL")]
        public string CoverImageUrl { get; set; }

        public string Description { get; set; }

        [Display(Name = "Category")]
        public VideoGameCategory VideoGameCategory { get; set; }

        // Relationships

        [Display(Name = "Developer")]
        public int DeveloperId { get; set; }

        [ForeignKey("DeveloperId")]
        [ValidateNever]
        public Developer Developer { get; set; }

        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        [ValidateNever]
        public Publisher Publisher { get; set; }

        [ValidateNever]
        public ICollection<VideoGamePlatform> VideoGamePlatforms { get; set; }
    }
}
