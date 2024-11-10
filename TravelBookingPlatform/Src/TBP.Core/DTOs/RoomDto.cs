using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBP.Domain.Entites;

namespace TBP.Core.DTOs
{
    public class RoomDto
    {
        public int Number { get; set; }
        public decimal Price { get; set; }
        public int AdultCapacity { get; set; }
        public int ChildCapacity { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public bool Availability { get; set; }
        public int? HotelId { get; set; }
        public FeaturedDealDto FeaturedDeal { get; set; }

    }
}
