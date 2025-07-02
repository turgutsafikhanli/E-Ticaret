using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;

    public ICollection<Product> Products { get; set; }
}

