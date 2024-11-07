using TBP.Core.DTOs;

namespace TBP.Presentation.Endpoints.FeaturedDealEndpoints.Get
{
    internal sealed class Request
    {
        public int Id { get; set; }
    }

    internal sealed class Response
    {
        public FeaturedDealDto FeaturedDeal { get; set; }
        public string ErrorMessage { get; set; }
    }
}