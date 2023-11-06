using System;
using System.Collections.Generic;

namespace HypeHaven.models;

/// <summary>
/// Represents a payment.
/// </summary>
public partial class Payment
{
    public int PaymentId { get; set; }

    public string Id { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public decimal PaidAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
