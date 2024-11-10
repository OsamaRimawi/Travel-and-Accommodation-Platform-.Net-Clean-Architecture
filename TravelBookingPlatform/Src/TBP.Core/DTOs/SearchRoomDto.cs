using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBP.Core.DTOs
{
    public class SearchRoomDto
    {
        public decimal? Price { get; set; }
        public int AdultCapacity { get; set; } = 2;
        public int ChildCapacity { get; set; } = 0;
        public bool Availability { get; set; } = true;
    }
}
