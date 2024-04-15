﻿using System.Text.Json.Serialization;
namespace TODO.WebApi.Models
{
    public class User
    {

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public virtual ICollection<Book>? UsersBooks { get; set; }
    }
}