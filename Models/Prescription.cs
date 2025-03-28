using System;
using System.Collections.Generic;

namespace PharmacyApp.Models;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public int CustomerId { get; set; }

    public int DoctorId { get; set; }

    public DateOnly IssueDate { get; set; }

    public string? Diagnosis { get; set; }

    public string? Dosage { get; set; }

    public string? Duration { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
