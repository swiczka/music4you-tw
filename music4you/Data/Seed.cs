using Microsoft.AspNetCore.Identity;
using music4you.Models;
using System.Drawing;
using System.Net;
using System.Reflection;

namespace music4you.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Albums.Any() || true)
                {
                    context.Albums.AddRange(new List<Album>()
                    {
                        new Album()
                        {
                            Author = "Death",
                            Genre = Genre.DeathMetal,
                            Name = "Symbolic",
                            Year = 1995,
                            ImageUrl = "https://i1.sndcdn.com/artworks-000094909900-s517hf-t500x500.jpg"
                        },
                        new Album()
                        {
                            Author = "Death",
                            Genre = Genre.DeathMetal,
                            Name = "Human",
                            Year = 1991,
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/a/a9/Human_Album.jpg"
                        },
                        new Album()
                        {
                            Author = "Metallica",
                            Genre = Genre.HeavyMetal,
                            Name = "Master of Puppets",
                            Year = 1986,
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/b2/Metallica_-_Master_of_Puppets_cover.jpg"
                        },
                        new Album()
                        {
                            Author = "Kult",
                            Genre = Genre.Rock,
                            Name = "Spokojnie",
                            Year = 1988,
                            ImageUrl = "https://ecsmedia.pl/c/spokojnie-b-iext160095559.jpg"
                        },
                        new Album()
                        {
                            Author = "Kazik na Żywo",
                            Genre = Genre.HardRock,
                            Name = "Porozumienie ponad podziałami",
                            Year = 1995,
                            ImageUrl = "https://ecsmedia.pl/c/porozumienie-ponad-podzialami-b-iext148316630.jpg"
                        },
                        new Album()
                        {
                            Author = "Kanye West",
                            Genre = Genre.HipHop,
                            Name = "My Beautiful Dark Twisted Fantasy",
                            Year = 2010,
                            ImageUrl = "https://m.media-amazon.com/images/I/71EYmWxcmdL._UF894,1000_QL80_.jpg"
                        },
                        new Album()
                        {
                            Author = "Coma",
                            Genre = Genre.Rock,
                            Name = "Pierwsze Wyjście z Mroku",
                            Year = 2004,
                            ImageUrl = "https://ecsmedia.pl/c/pierwsze-wyjscie-z-mroku-b-iext148218643.jpg"
                        },
                        new Album()
                        {
                            Author = "Slayer",
                            Genre = Genre.ThrashMetal,
                            Name = "Reign in Blood",
                            Year = 1986,
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/8/8e/Reign_in_blood.jpg"
                        },
                        new Album()
                        {
                            Author = "Led Zeppelin",
                            Genre = Genre.Rock,
                            Name = "Led Zeppelin IV",
                            Year = 1971,
                            ImageUrl = "https://voiceshop.pl/userdata/public/gfx/43942/led_zeppelin_iv.jpg"
                        },
                        new Album()
                        {
                            Author = "Nirvana",
                            Genre = Genre.Grunge,
                            Name = "Nevermind",
                            Year = 1991,
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/b7/NirvanaNevermindalbumcover.jpg"
                        },
                        new Album()
                        {
                            Author = "Iron Maiden",
                            Genre = Genre.HeavyMetal,
                            Name = "The Number of the Beast",
                            Year = 1982,
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/tr/thumb/3/30/TheNumberOfTheBeast.jpg/640px-TheNumberOfTheBeast.jpg"
                        },
                        new Album()
                        {
                            Author = "Pink Floyd",
                            Genre = Genre.Rock,
                            Name = "The Dark Side of the Moon",
                            Year = 1973,
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/3/3b/Dark_Side_of_the_Moon.png"
                        },
                        new Album()
                        {
                            Author = "Queen",
                            Genre = Genre.Rock,
                            Name = "A Night at the Opera",
                            Year = 1975,
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/4/4d/Queen_A_Night_At_The_Opera.png"
                        },
                        new Album()
                        {
                            Author = "Black Sabbath",
                            Genre = Genre.HeavyMetal,
                            Name = "Paranoid",
                            Year = 1970,
                            ImageUrl = "https://ecsmedia.pl/c/paranoid-b-iext156937938.jpg"
                        },
                        new Album()
                        {
                            Author = "Tool",
                            Genre = Genre.Metal,
                            Name = "Lateralus",
                            Year = 2001,
                            ImageUrl = "https://lastfm.freetls.fastly.net/i/u/ar0/b5a5721a08264207c2df36bf07454005.jpg"
                        },
                        new Album()
                        {
                            Author = "Kult",
                            Genre = Genre.Alternative,
                            Name = "Tata Kazika",
                            Year = 1993,
                            ImageUrl = "https://ecsmedia.pl/cdn-cgi/image/width=270,height=270,/c/tata-kazika-b-iext125376568.jpg"
                        },
                        new Album()
                        {
                            Author = "Kazik Staszewski",
                            Genre = Genre.Rock,
                            Name = "Spalam się",
                            Year = 1991,
                            ImageUrl = "https://polskiemc.wordpress.com/wp-content/uploads/2017/01/kazo.jpg"
                        },
                        new Album()
                        {
                            Author = "Guns N' Roses",
                            Genre = Genre.HardRock,
                            Name = "Appetite for Destruction",
                            Year = 1987,
                            ImageUrl = "https://ecsmedia.pl/c/appetite-for-destruction-reedycja-b-iext124064410.jpg"
                        }
                    // Add more albums similarly
                });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "173726@stud.prz.edu.pl";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = adminUserEmail.Split('@')[0],
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Abc-1234");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = appUserEmail.Split('@')[0],
                        Email = appUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAppUser, "Abc-1234");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
