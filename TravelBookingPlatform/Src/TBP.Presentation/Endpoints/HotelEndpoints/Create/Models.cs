using FastEndpoints;
using FluentValidation;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.HotelEndpoints.Create
{
    internal sealed class Request
    {
        public HotelDto hotel { get; set; }

        internal sealed class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.hotel.Name)
                    .NotEmpty()
                    .WithMessage("Name must be provided.");
                RuleFor(x => x.hotel.Location)
                    .NotEmpty()
                    .WithMessage("Location must be provided.");
                RuleFor(x => x.hotel.Owner)
                   .NotEmpty()
                   .WithMessage("Owner must be provided.");
                RuleFor(x => x.hotel.StarRating)
                   .NotEmpty()
                   .WithMessage("StarRating must be provided.");
            }
        }
    }

    internal sealed class Response
    {
        public Hotel Hotel { get; set; }
        public string ErrorMessage { get; set; }
    }
}
