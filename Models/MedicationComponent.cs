using System;
using System.Collections.Generic;

namespace PharmacyApp.Models;

public partial class MedicationComponent
{
    public int MedicationId { get; set; }

    public int ComponentId { get; set; }

    public decimal Amount { get; set; }

    public virtual Component Component { get; set; } = null!;

    public virtual Medication Medication { get; set; } = null!;
}
