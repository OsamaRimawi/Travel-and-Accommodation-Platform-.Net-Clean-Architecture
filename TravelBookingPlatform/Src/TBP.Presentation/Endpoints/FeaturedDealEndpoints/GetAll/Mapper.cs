using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.FeaturedDealEndpoints.GetAll
{
    internal sealed class Mapper : Mapper<EmptyRequest, Response, object>
    {
        private readonly AutoMapper.IMapper _mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public Response FromEntity(IEnumerable<FeaturedDeal> featuredDeals)
        {
            if (featuredDeals == null)
            {
                return new Response { FeaturedDeals = new List<FeaturedDealDto>() }; // Return an empty list
            }

            var featuredDealsDtos = _mapper.Map<IEnumerable<FeaturedDealDto>>(featuredDeals); // Direct mapping
            return new Response { FeaturedDeals = featuredDealsDtos };
        }
    }
}
