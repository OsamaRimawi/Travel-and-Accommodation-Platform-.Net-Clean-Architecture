using FastEndpoints;
using FluentValidation;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.FeaturedDealEndpoints.Create
{
    internal sealed class Mapper : Mapper<Request, Response, Room>
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
            return new Response { FeaturedDeal = featuredDeal };
        }
    }
}
