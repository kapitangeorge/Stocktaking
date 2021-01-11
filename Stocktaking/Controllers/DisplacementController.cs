using Microsoft.AspNetCore.Mvc;
using Stocktaking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Controllers
{
    public class DisplacementController : Controller
    {
        private ApplicationContext database;

        public DisplacementController (ApplicationContext context)
        {
            database = context;
        }

        
    }
}
