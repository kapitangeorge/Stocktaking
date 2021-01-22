using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocktakingWebApi.Models
{
    public class Displacement
    {
        public int Id { get; set; }

        public string WhereTo { get; set; }

        public string FromWhere { get; set; }

        public string WhoAdd { get; set; }

        public DateTime When { get; set; }

        public string Status { get; set; }

        public int OrganizationId { get; set; }
    }
}
