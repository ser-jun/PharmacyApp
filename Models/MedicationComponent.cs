using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyApp.Models;

public partial class MedicationComponent
{
    [Column("medication_id")]
    public int MedicationId { get; set; }
    [Column("component_id")]
    public int ComponentId { get; set; }

    public decimal Amount { get; set; }

    public virtual Component Component { get; set; } = null!;

    public virtual Medication Medication { get; set; } = null!;
}
