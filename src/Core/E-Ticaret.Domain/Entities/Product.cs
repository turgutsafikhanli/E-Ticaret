using static System.Net.Mime.MediaTypeNames;

namespace E_Ticaret.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public string UserId { get; set; } // <-- string tipli UserId
    public AppUser User { get; set; }

    public ICollection<Image> Images { get; set; }
    public ICollection<Favourite> Favourites { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; }
}

