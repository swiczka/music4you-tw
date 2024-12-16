using music4you.Models;

namespace music4you.ViewModels
{
    public class AccountDetailsViewModel
    {
        public AppUser AppUser { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
