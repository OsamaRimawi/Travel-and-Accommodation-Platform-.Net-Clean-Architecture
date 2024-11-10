using FastEndpoints;
using FluentValidation;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.HotelEndpoints.Get
{
    internal sealed class Request
    {
        public int Id { get; set; }

        internal sealed class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id must be provided.");
            }
        }
    }

    internal sealed class Response
    {
        public HotelDto Hotel { get; set; }
        public string ErrorMessage { get; set; }
    }
}
