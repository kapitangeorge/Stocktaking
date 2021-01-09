using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public int OrganizationId { get; set; }
        
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
