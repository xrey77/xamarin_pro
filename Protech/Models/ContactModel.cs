using System;
using Newtonsoft.Json;

namespace Protech.Models
{
    public class ContactModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Contact_Name")]
        public string Contact_Name { get; set; }

        [JsonProperty("Contact_Address")]
        public string Contact_Address { get; set; }

        [JsonProperty("Contact_Email")]
        public string Contact_Email { get; set; }

        [JsonProperty("Contact_Mobileno")]
        public string Contact_Mobileno { get; set; }

        [JsonProperty("IsActive")]
        public string IsActive { get; set; }
    }
}

