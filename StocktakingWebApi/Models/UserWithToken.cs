using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocktakingWebApi.Models
{
    public class UserWithToken
    {
        public UserWithToken(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Position = user.Position;
            OrganizationId = user.OrganizationId;
            Username = user.Username;
            Password = user.Password;
            Role = user.Role;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public int OrganizationId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }
    }
}
