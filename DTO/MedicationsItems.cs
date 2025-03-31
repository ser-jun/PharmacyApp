using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.DTO
{
    public class MedicationsItems
    {
        public int MedicationId { get; set; }
        public string MedicationName { get; set; }
        public bool IsReadyMade { get; set; }
        public decimal? Price { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string ApplicationMethod { get; set; } // "Внутрь", "Наружное", "Смешанное"
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string ReadyMadeStatus => IsReadyMade ? "Готовый" : "Требует приготовления";
        public string PriceFormatted => Price?.ToString("C2") ?? "Цена не указана";
    }
}
