namespace E_Ticaret.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public Guid? MainCategoryId { get; set; }
    public Category? MainCategory { get; set; }
    public ICollection<Category> SubCategories { get; set; }

    public ICollection<Product> Products { get; set; }
}

