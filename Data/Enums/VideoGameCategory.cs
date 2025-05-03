using System;
using System.ComponentModel.DataAnnotations;

namespace EVideoGameStoreApp.Data.Enums
{
    [Flags]
    public enum VideoGameCategory
    {
        None = 0,

        [Display(Name = "Action")]
        Action = 1,

        [Display(Name = "Adventure")]
        Adventure = 2,

        [Display(Name = "RPG")]
        RPG = 4,

        [Display(Name = "Sports")]
        Sports = 8,

        [Display(Name = "Racing")]
        Racing = 16,

        [Display(Name = "Shooter")]
        Shooter = 32,

        [Display(Name = "Puzzle")]
        Puzzle = 64,

        [Display(Name = "Strategy")]
        Strategy = 128,

        [Display(Name = "Simulation")]
        Simulation = 256,

        [Display(Name = "Horror")]
        Horror = 512,

        [Display(Name = "Indie")]
        Indie = 1024,

        [Display(Name = "Multiplayer")]
        Multiplayer = 2048,

        [Display(Name = "Singleplayer")]
        Singleplayer = 4096,

        [Display(Name = "Fighting")]
        Fighting = 8192,
    }
}
