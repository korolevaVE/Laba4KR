using System;
using System.Collections.Generic;

namespace ShopApp.Models;

public partial class Roles
{
    public int Id { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}
