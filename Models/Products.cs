using System;
using System.Collections.Generic;

namespace ShopApp.Models;

public partial class Products
{
    public int Id { get; set; }

    public string? Article { get; set; }

    public int? NameId { get; set; }

    public string? Unit { get; set; }

    public double? Price { get; set; }

    public int? SupplierId { get; set; }

    public int? ManufacturerId { get; set; }

    public int? CategoryId { get; set; }

    public double? Discount { get; set; }

    public double? Count { get; set; }

    public string? Description { get; set; }

    public string? ImagePath { get; set; }

    public virtual Categories? Category { get; set; }

    public virtual Manufacturers? Manufacturer { get; set; }

    public virtual ProductNames? Name { get; set; }

    public virtual ICollection<OrderArticle> OrderArticle { get; set; } = new List<OrderArticle>();

    public virtual Suppliers? Supplier { get; set; }
}
