using System;
using Newtonsoft.Json;

namespace Protech.Models
{
    public class ProductModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("ProductName")]
        public string ProductName { get; set; }

        [JsonProperty("Descriptions")]
        public string Descriptions { get; set; }

        [JsonProperty("CostPrice")]
        public double CostPrice { get; set; }

        [JsonProperty("SellingPrice")]
        public double SellingPrice { get; set; }

        [JsonProperty("DeliveredQty")]
        public int DeliveredQty { get; set; }

        [JsonProperty("AvailableQty")]
        public int AvailableQty { get; set; }

        [JsonProperty("UnitOfMeasure")]
        public string UnitOfMeasure { get; set; }

    }
}
