using System;
using System.Collections.Generic;

namespace PharmacyApp.Models;

public partial class PendingOrder
{
    public int PendingId { get; set; }

    public int OrderId { get; set; }

    public int? ComponentId { get; set; }

    public decimal? RequiredAmount { get; set; }

    public virtual Component Component { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
