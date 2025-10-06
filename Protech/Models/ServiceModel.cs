using System;
using Newtonsoft.Json;

namespace Protech.Models
{
    public class ServiceModel
    {

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("ServiceName")]
        public string ServiceName { get; set; }

        [JsonProperty("Descriptions")]
        public string Descriptions { get; set; }

        [JsonProperty("ServiceFee")]
        public string ServiceFee { get; set; }

        [JsonProperty("PaymentMode")]
        public string PaymentMode { get; set; }

    }
}