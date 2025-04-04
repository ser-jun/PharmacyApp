using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.DTO
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte Rating { get; set; }
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }

        public int DeliveryTimeDays { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
