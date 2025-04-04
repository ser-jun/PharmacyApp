using System;
using System.Collections.Generic;

namespace PharmacyApp.Models;

public partial class SupplierComponent
{
    public int SupplierId { get; set; }

    public int ComponentId { get; set; }

    public int? DeliveryTime { get; set; }

    public virtual Component Component { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
