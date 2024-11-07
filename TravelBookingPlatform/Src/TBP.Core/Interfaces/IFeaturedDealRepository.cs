using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBP.Domain.Entites;

namespace TBP.Core.Interfaces
{
    public interface IFeaturedDealRepository
    {
        Task<IEnumerable<FeaturedDeal>> GetFeaturedDealsAsync();
        Task<FeaturedDeal> GetFeaturedDealByIdAsync(int id);
        Task<FeaturedDeal> AddFeaturedDealAsync(FeaturedDeal featuredDeal);
        Task<FeaturedDeal> UpdateFeaturedDealAsync(int id, FeaturedDeal featuredDeal);
        Task DeleteFeaturedDealAsync(int id);
    }
}
