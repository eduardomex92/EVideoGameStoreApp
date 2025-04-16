using System.ComponentModel.DataAnnotations;

namespace EVideoGameStoreApp.Data.Enums
{
    public enum VideoGameCategory
    {
        [Display(Name = "Action")]
        Action = 1,

        [Display(Name = "Adventure")]
        Adventure,

        [Display(Name = "RPG")]
        RPG,

        [Display(Name = "Sports")]
        Sports,

        [Display(Name = "Racing")]
        Racing,

        [Display(Name = "Shooter")]
        Shooter,

        [Display(Name = "Puzzle")]
        Puzzle,

        [Display(Name = "Strategy")]
        Strategy,

        [Display(Name = "Simulation")]
        Simulation,

        [Display(Name = "Horror")]
        Horror,

        [Display(Name = "Indie")]
        Indie
    }
}
