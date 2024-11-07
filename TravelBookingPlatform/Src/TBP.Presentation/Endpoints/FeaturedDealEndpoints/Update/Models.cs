using FastEndpoints;
using FluentValidation;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.FeaturedDealEndpoints.Update
{
    internal sealed class Request
    {
        public int Id { get; set; }
        public FeaturedDealDto FeaturedDeal { get; set; }

        internal sealed class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.FeaturedDeal.RoomId)
                    .GreaterThan(0)
                    .WithMessage("Room ID must be greater than zero.");
                RuleFor(x => x.FeaturedDeal.OriginalPrice)
                    .GreaterThan(0)
                    .WithMessage("Original price must be greater than zero.");
                RuleFor(x => x.FeaturedDeal.DiscountedPrice)
                    .GreaterThan(0)
                    .WithMessage("Discounted price must be greater than zero.");
            }
        }
    }

    internal sealed class Response
    {
        public FeaturedDealDto FeaturedDeal { get; set; }
        public string ErrorMessage { get; set; }
    }
}