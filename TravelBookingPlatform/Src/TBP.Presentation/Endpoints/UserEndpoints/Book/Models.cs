using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.UserEndpoints.Book
{
    internal sealed class Request
    {
        public int id { get; set; }

        public int RoomId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        internal sealed class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.RoomId)
                    .GreaterThan(0)
                    .WithMessage("RoomId must be greater than 0.");

                RuleFor(x => x.CheckInDate)
                    .GreaterThan(DateTime.Now)
                    .WithMessage("Check-in date must be in the future.");

                RuleFor(x => x.CheckOutDate)
                    .GreaterThan(x => x.CheckInDate)
                    .WithMessage("Check-out date must be after the check-in date.");
            }
        }
    }

    internal sealed class Response
    {
        public BookingDto Booking { get; set; }
        public string ErrorMessage { get; set; }
    }
}