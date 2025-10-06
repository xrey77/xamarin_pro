using System;
using Newtonsoft.Json;

namespace Protech.Models
{
    public class MailtokenModel
    {
        [JsonProperty("MailToken")]
        public string Mailtoken { get; set; }
    }
}
