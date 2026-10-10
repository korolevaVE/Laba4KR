using System;
using System.Collections.Generic;

namespace ShopApp.Models;

public partial class Users
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public int? Role { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();

    public virtual Roles? RoleNavigation { get; set; }
}
