using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.DTO
{
    public class UnclaimedOrdersDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }


        public string MedicationName { get; set; }


        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }


        public int UnclaimedOrdersCount { get; set; }

        public int TotalCustomersWithUnclaimedOrders { get; set; }
        public int TotalUnclaimedOrders { get; set; }
    }
}
