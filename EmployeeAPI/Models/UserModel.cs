using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeAPI.Models
{
    public class UserModel
    {
        [Required]
        public string UserName { get; set; }
        [MinLength(5)]
        public string Password { get; set; }

        public IdentityUser BindModel()
        {
            IdentityUser usr = new();
            usr.UserName = UserName;
            return usr;
        }

        [JsonIgnore]
        public string Token { get; set; }
        [JsonIgnore]
        public List<string> Errors { get; set; } = new List<string>();
    }
}
