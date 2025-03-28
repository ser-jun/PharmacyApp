using System;
using System.Collections.Generic;

namespace PharmacyApp.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int PrescriptionId { get; set; }

    public int MedicationId { get; set; }

    public decimal Amount { get; set; }

    public int RegistrarId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public decimal? Price { get; set; }

    public virtual Medication Medication { get; set; } = null!;

    public virtual ICollection<PendingOrder> PendingOrders { get; set; } = new List<PendingOrder>();

    public virtual Prescription Prescription { get; set; } = null!;

    public virtual User Registrar { get; set; } = null!;
}
