using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.ViewModels
{
    public class DeleteItemsViewModel
    {
        [BindProperty]
        public List<DeleteItemViewModel> DeleteItems { get; set; } = new List<DeleteItemViewModel>();
    }
}
