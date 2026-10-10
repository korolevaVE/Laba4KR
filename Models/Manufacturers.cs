using System;
using System.Collections.Generic;

namespace ShopApp.Models;

public partial class Manufacturers
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Products> Products { get; set; } = new List<Products>();
}
