using System;
using System.Collections.Generic;

namespace PharmacyApp.Models;

public partial class SupplyRequest
{
    public int RequestId { get; set; }

    public int ComponentId { get; set; }

    public decimal? RequestedAmount { get; set; }

    public DateTime? RequestDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Component Component { get; set; } = null!;
}
