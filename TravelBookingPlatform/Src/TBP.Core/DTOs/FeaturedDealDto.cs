using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBP.Domain.Entites;

namespace TBP.Core.DTOs
{
    public class FeaturedDealDto
    {
        public int RoomId { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal DiscountedPrice { get; set; }

    }
}
