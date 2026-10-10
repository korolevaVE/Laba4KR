using System;
using System.Collections.Generic;

namespace ShopApp.Models;

public partial class OrderArticle
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public double? Count { get; set; }

    public int? ProductId { get; set; }

    public virtual Orders? Order { get; set; }

    public virtual Products? Product { get; set; }
}
