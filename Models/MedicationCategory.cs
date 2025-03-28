using System;
using System.Collections.Generic;

namespace PharmacyApp.Models;

public partial class MedicationCategory
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Medication> Medications { get; set; } = new List<Medication>();
}
