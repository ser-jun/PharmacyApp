using System;
using System.Collections.Generic;

namespace PharmacyApp.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? ContactPhone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
