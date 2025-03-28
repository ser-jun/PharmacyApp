using System;
using System.Collections.Generic;

namespace PharmacyApp.Models;

public partial class MedicationType
{
    public int TypeId { get; set; }

    public string Name { get; set; } = null!;

    public string ApplicationMethod { get; set; } = null!;

    public virtual ICollection<Medication> Medications { get; set; } = new List<Medication>();
}
