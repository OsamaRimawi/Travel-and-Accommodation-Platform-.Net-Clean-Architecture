using FastEndpoints;
using FluentValidation;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.RoomEndpoints.Update
{
    internal sealed class Request
    {
        public int Id { get; set; }
        public RoomDto room { get; set; }


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
        public RoomDto Room { get; set; }
        public string ErrorMessage { get; set; }
    }
}
