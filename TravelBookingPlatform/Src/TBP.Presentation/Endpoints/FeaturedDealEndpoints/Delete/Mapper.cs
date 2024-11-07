using FastEndpoints;
using FluentValidation;

namespace TBP.Presentation.Endpoints.FeaturedDealEndpoints.Delete
{
    internal sealed class Mapper : Mapper<Request, Response, object>
    {
        private readonly AutoMapper.IMapper _mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }


    }
}
