using AutoMapper;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace MappingProfiles
{
    public class FeaturedDealProfile : Profile
    {
        public FeaturedDealProfile()
        {
            CreateMap<FeaturedDeal, FeaturedDealDto>();
            CreateMap<FeaturedDealDto, FeaturedDeal>();
        }
    }
}
