using FastEndpoints;
using FluentValidation;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace Create
{
    internal sealed class Request
    {
        public CityDto city { get; set; }

        internal sealed class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.city.Name)
                    .NotEmpty()
                    .WithMessage("Name must be provided.");
                RuleFor(x => x.city.Country)
                    .NotEmpty()
                    .WithMessage("Country must be provided.");
                 RuleFor(x => x.city.PostOffice)
                    .NotEmpty()
                    .WithMessage("PostOffice must be provided.");
            }
        }
    }

    internal sealed class Response
    {
        public City City { get; set; }
        public string ErrorMessage { get; set; }
    }
}
