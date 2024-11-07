using Microsoft.EntityFrameworkCore;
using TBP.Domain.Entites;
using TBP.Core.Interfaces;

namespace TBP.Infrastructure.Database.Repositories
{
    public class FeaturedDealRepository : IFeaturedDealRepository
    {
        private readonly TBPDbContext _context;

        public FeaturedDealRepository(TBPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeaturedDeal>> GetFeaturedDealsAsync()
        {
            return await _context.Set<FeaturedDeal>()
                                 .Include(fd => fd.Room)
                                 .ToListAsync();
        }

        public async Task<FeaturedDeal> GetFeaturedDealByIdAsync(int id)
        {
            return await _context.Set<FeaturedDeal>()
                                 .Include(fd => fd.Room)
                                 .FirstOrDefaultAsync(fd => fd.Id == id);
        }

        public async Task<FeaturedDeal> AddFeaturedDealAsync(FeaturedDeal featuredDeal)
        {
            _context.Set<FeaturedDeal>().Add(featuredDeal);
            await _context.SaveChangesAsync();
            return featuredDeal;
        }

        public async Task<FeaturedDeal> UpdateFeaturedDealAsync(int id, FeaturedDeal featuredDeal)
        {
            var existingFeaturedDeal = await _context.Set<FeaturedDeal>()
                                                     .Include(fd => fd.Room)
                                                     .FirstOrDefaultAsync(fd => fd.Id == id);
            if (existingFeaturedDeal == null)
            {
                return null;
            }

            _context.Entry(existingFeaturedDeal).CurrentValues.SetValues(featuredDeal);

            await _context.SaveChangesAsync();
            return existingFeaturedDeal;
        }

        public async Task DeleteFeaturedDealAsync(int id)
        {
            var featuredDeal = await _context.Set<FeaturedDeal>()
                                             .Include(fd => fd.Room)
                                             .FirstOrDefaultAsync(fd => fd.Id == id);
            if (featuredDeal != null)
            {
                _context.Set<FeaturedDeal>().Remove(featuredDeal);
                await _context.SaveChangesAsync();
            }
        }
    }
}