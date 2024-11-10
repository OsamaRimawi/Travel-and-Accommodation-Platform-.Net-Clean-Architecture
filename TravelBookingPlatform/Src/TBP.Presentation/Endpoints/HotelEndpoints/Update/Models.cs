using FastEndpoints;
using FluentValidation;
using TBP.Core.DTOs;

namespace TBP.Presentation.Endpoints.HotelEndpoints.Update
{
    internal sealed class Request
    {
        public int Id { get; set; }
        public HotelDto hotel { get; set; }


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
        public HotelDto Hotel { get; set; }
        public string ErrorMessage { get; set; }
    }
}
