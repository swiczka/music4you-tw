using Microsoft.AspNetCore.Identity;

namespace music4you.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
