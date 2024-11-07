using FastEndpoints;
using FluentValidation;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace Update
{
    internal sealed class Request
    {
        public int Id { get; set; }
        public CityDto city { get; set; }


        internal sealed class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Id).GreaterThan(0);

            }
        }
    }

    internal sealed class Response
    {
        public CityDto City { get; set; }
        public string ErrorMessage { get; set; }
    }
}
