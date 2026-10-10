using System;
using System.Collections.Generic;

namespace ShopApp.Models;

public partial class Orders
{
    public int Id { get; set; }

    public double? Number { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public int? PickupPointId { get; set; }

    public int? UserId { get; set; }

    public double? Code { get; set; }

    public int? StatusId { get; set; }

    public virtual ICollection<OrderArticle> OrderArticle { get; set; } = new List<OrderArticle>();

    public virtual PickupPoint? PickupPoint { get; set; }

    public virtual Statuses? Status { get; set; }

    public virtual Users? User { get; set; }
}
