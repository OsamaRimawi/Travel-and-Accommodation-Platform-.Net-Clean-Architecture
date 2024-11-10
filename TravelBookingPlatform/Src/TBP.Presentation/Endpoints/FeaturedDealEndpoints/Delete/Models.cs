using FastEndpoints;
using FluentValidation;

namespace TBP.Presentation.Endpoints.FeaturedDealEndpoints.Delete
{
    internal sealed class Request
    {
        public int Id { get; set; }

        internal sealed class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0)
                    .WithMessage("Id must be greater than zero.");
            }
        }
    }

    internal sealed class Response
    {
        public string ErrorMessage { get; set; }
    }
}