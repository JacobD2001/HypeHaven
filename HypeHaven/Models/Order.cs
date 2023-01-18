using System;
using System.Collections.Generic;

namespace HypeHaven.models;

public partial class Order
{
    public int OrderId { get; set; }

    public string Id { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public decimal Total { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public string OrderStatus { get; set; } = null!;

    public string? TrackingNumber { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string ShippingMethod { get; set; } = null!;

    public string? PromoCode { get; set; }

    public int ProductId { get; set; }

    public int PaymentsId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual Payment Payments { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
