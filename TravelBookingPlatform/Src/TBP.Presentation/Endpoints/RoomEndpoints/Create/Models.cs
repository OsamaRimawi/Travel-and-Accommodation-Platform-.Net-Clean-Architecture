using FastEndpoints;
using FluentValidation;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.RoomEndpoints.Create
{
    internal sealed class Request
    {
        public RoomDto room { get; set; }

        internal sealed class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.room.Number)
                    .GreaterThan(0)
                    .WithMessage("Room number must be greater than zero.");
                RuleFor(x => x.room.Price)
                    .GreaterThan(0)
                    .WithMessage("Price must be greater than zero.");
                RuleFor(x => x.room.Availability)
                    .NotNull()
                    .WithMessage("Availability must be provided.");
            }
        }
    }

    internal sealed class Response
    {
        public Room Room { get; set; }
        public string ErrorMessage { get; set; }
    }
}