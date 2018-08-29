using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.Controllers.WebAPI
{
    public class NewRentalDTO
    {
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}