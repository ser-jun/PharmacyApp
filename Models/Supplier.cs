using System;
using System.Collections.Generic;

namespace PharmacyApp.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string Name { get; set; } = null!;

    public string? ContactPerson { get; set; }

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public sbyte? Rating { get; set; }

    public virtual ICollection<SupplierComponent> SupplierComponents { get; set; } = new List<SupplierComponent>();
}
