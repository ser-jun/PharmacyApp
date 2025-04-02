using System;
using System.Collections.Generic;

namespace PharmacyApp.Models;

public partial class Stock
{
    public int StockId { get; set; }

    public int ComponentId { get; set; }

    public decimal Amount { get; set; }

    public DateTime ArrivalDate { get; set; }

    public virtual Component Component { get; set; } = null!;
}
