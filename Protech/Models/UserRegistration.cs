using System;
using Newtonsoft.Json;

namespace Protech.Models
{
    public class UserRegistration
    {
        [JsonProperty("Firstname")]
        public string Firstname { get; set; }

        [JsonProperty("Lastname")]
        public string Lastname { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

    }
}
