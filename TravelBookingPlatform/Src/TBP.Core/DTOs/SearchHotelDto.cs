using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBP.Core.DTOs
{
    public class SearchHotelDto
    {
        public string? Name { get; set; }

        public int? StarRating { get; set; }

        public string? Location { get; set; }

        public string? CityName { get; set; }

        public string? Owner { get; set; }
    }
}
