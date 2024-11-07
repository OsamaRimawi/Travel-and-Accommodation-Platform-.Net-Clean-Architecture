using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.FeaturedDealEndpoints.Get
{
    internal sealed class Mapper : Mapper<Request, Response, object>
    {
        private readonly AutoMapper.IMapper _mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public Response FromEntity(FeaturedDeal featuredDeal)
        {
            if (featuredDeal == null)
            {
                return new Response {}; // Return an empty
            }
            var featuredDealDto = _mapper.Map<FeaturedDealDto>(featuredDeal);
            return new Response { FeaturedDeal = featuredDealDto };
        }
    }
}
