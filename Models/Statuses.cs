using System;
using System.Collections.Generic;

namespace ShopApp.Models;

public partial class Statuses
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
