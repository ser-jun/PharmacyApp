using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.DTO
{
    public class StockItems
    {
        public int StockId { get; set; }
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal Amount { get; set; }
        public decimal CriticalNorm { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int ShelfLife { get; set; }
    }
}
