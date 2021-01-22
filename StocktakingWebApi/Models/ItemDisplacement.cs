using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocktakingWebApi.Models
{
    public class ItemDisplacement
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public int DisplacementId { get; set; }
    }
}
