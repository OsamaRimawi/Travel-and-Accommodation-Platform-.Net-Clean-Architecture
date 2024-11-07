using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.UserCommands;
using TBP.Core.DTOs;

namespace TBP.Presentation.Endpoints.UserEndpoints.Book
{
    internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Post("/api/users/{id}/book");
            Validator<Request.Validator>(); // Apply the validator
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new CreateBooking.Command
                {
                    BookingDto = new BookingDto
                    {
                        UserId = req.id,
                        RoomId = req.RoomId,
                        CheckInDate = req.CheckInDate,
                        CheckOutDate = req.CheckOutDate
                    }
                }.ExecuteAsync(ct);

                if (cmdResult.ErrorMessage != null)
                {
                    await SendAsync(new Response
                    {
                        ErrorMessage = cmdResult.ErrorMessage
                    }, (int)HttpStatusCode.BadRequest);
                    return;
                }

                var response = Map.FromEntity(cmdResult.Booking);
                await SendAsync(response, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error creating booking: {exception}", ex);
                await SendErrorsAsync((int)HttpStatusCode.InternalServerError, ct);
            }
        }
    }
}