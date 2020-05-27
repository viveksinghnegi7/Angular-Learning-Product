using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Demo.Entities
{
    public partial class User
    {
        [Required]
        public int UserId { get; set; }

        [Required] 
        public string Email { get; set; }

        [Required] 
        public string FirstName { get; set; } 

        [Required] 
        public string LastName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public byte[] PasswordHash { get; set; }

        [JsonIgnore]
        public byte[] PasswordSalt { get; set; } 
    }
}
