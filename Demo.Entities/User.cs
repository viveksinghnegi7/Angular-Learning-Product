using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Demo.Entities
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; } 
    }
}
