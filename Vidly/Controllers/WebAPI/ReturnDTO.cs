using System.Collections.Generic;

namespace Vidly.Controllers.WebAPI
{
    public class ReturnDTO
    {
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}