using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyApp.Models;

public partial class Component
{
    public int ComponentId { get; set; }

    public string Name { get; set; } = null!;

    public string UnitOfMeasure { get; set; } = null!;

    public decimal CriticalNorm { get; set; }

    public int? ShelfLife { get; set; }
    [NotMapped] 
    public bool IsSelected { get; set; }

    public virtual ICollection<MedicationComponent> MedicationComponents { get; set; } = new List<MedicationComponent>();

    public virtual ICollection<PendingOrder> PendingOrders { get; set; } = new List<PendingOrder>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual ICollection<SupplierComponent> SupplierComponents { get; set; } = new List<SupplierComponent>();

    public virtual ICollection<SupplyRequest> SupplyRequests { get; set; } = new List<SupplyRequest>();
}
