using System.ComponentModel.DataAnnotations;

namespace music4you.Data
{
    public enum Genre
    {
        Rock,
        Metal,
        [Display(Name = "Thrash Metal")]
        ThrashMetal,
        [Display(Name = "Death Metal")]
        DeathMetal,
        [Display(Name = "Heavy Metal")]
        HeavyMetal,
        [Display(Name = "Hard Rock")]
        HardRock,
        Pop,
        [Display(Name = "Hip-hop")]
        HipHop,
        Jazz,
        Blues,
        [Display(Name = "Klasyczna")]
        Classical,
        [Display(Name = "Elektroniczna")]
        Electronic,
        Country,
        Reggae,
        Punk,
        Folk,
        RnB,
        Soul,
        Disco,
        Funk,
        Gospel,
        Latin,
        Indie,
        Grunge,
        [Display(Name = "Synth Pop")]
        SynthPop,
        House,
        Techno,
        [Display(Name = "K-pop")]
        KPop,
        [Display(Name = "Alternatywna")]
        Alternative,
        Ambient,
        [Display(Name = "Eksperymentalna")]
        Experimental
    }
}
