using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.FeaturedDealEndpoints.GetAll
{
    internal sealed class Response
    {
        public IEnumerable<FeaturedDealDto> FeaturedDeals { get; set; }
        public string ErrorMessage { get; set; }
    }
}