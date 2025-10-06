using System;
using Newtonsoft.Json;

namespace Protech.Models
{
    public class ErroMessage
    {
        [JsonProperty("Message")]
        public string Message { get; set; }

    }
}
