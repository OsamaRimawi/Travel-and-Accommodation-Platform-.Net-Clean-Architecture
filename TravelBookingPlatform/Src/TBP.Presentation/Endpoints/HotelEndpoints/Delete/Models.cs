using FastEndpoints;
using FluentValidation;

namespace TBP.Presentation.Endpoints.HotelEndpoints.Delete
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
        public string ErrorMessage { get; set; }
    }
}
