using System;
using System.Collections.Generic;

namespace PharmacyApp.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FullName { get; set; }
    public DateTime? BirthDate { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
