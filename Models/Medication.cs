using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyApp.Models;

public partial class Medication
{
    [Column("medication_id")]
    public int MedicationId { get; set; }

    public string Name { get; set; } = null!;

    public int? TypeId { get; set; }

    public int? CategoryId { get; set; }

    public bool? IsReadyMade { get; set; }

    public decimal? Price { get; set; }

    public virtual MedicationCategory Category { get; set; } = null!;

    public virtual ICollection<MedicationComponent> MedicationComponents { get; set; } = new List<MedicationComponent>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual MedicationType Type { get; set; } = null!;
}
