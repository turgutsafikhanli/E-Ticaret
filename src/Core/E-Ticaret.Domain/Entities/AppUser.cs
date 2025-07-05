using Microsoft.AspNetCore.Identity;

namespace E_Ticaret.Domain.Entities;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; } = null!;
    public string? RefreshToken { get; set; }
    public DateTime ExpiryDate { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<Favourite> Favourites { get; set; }
    public ICollection<Order> Orders { get; set; }
}
