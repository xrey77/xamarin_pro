using System;
using Newtonsoft.Json;

namespace Protech.Models
{
    public class UserLogin
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Firstname")]
        public string Firstname { get; set; }

        [JsonProperty("Lastname")]
        public string Lastname { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Profilepic")]
        public string Profilepic { get; set; }

        [JsonProperty("Role")]
        public string Role { get; set; }

        [JsonProperty("IsActivated")]
        public string Isactivated { get; set; }

        [JsonProperty("Token")]
        public string Token { get; set; }
    }
}
/*
public class User
{
    public string id;
    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    public string firstname;
    public string Firstname
    {
        get { return firstname; }
        set { firstname = value; }
    }

    public string lastname;
    public string Lastname
    {
        get { return lastname; }
        set { lastname = value; }
    }

    public string username;
    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    public string profilepic;
    public string Profilepic
    {
        get { return profilepic; }
        set { profilepic = value; }
    }

    public string role;
    public string Role
    {
        get { return role; }
        set { role = value; }
    }

    public string isactivated;
    public string IsActivated
    {
        get { return isactivated; }
        set { isactivated = value; }
    }

    public string token;
    public string Token
    {
        get { return token; }
        set { token = value; }
    }
}
*/