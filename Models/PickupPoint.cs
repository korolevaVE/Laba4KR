using System;
using System.Collections.Generic;

namespace ShopApp.Models;

public partial class PickupPoint
{
    public int Id { get; set; }

    public string? Adress { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
