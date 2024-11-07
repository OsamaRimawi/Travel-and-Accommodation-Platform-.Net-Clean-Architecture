using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.HotelCommands;

namespace TBP.Presentation.Endpoints.HotelEndpoints.Create
{
    internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Post("/api/hotels");
            Policies("AdminOnly");
            Validator<Request.Validator>(); // Apply the validator
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new CreateHotel.Command {
                    hotelDto = req.hotel
                }.ExecuteAsync(ct);

                if (cmdResult.ErrorMessage != null)
                {
                    await SendAsync(new Response
                    {
                        ErrorMessage = cmdResult.ErrorMessage
                    }, (int)HttpStatusCode.BadRequest);
                    return;
                }

                var response = Map.FromEntity(cmdResult.Hotel);
                await SendAsync(response, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error creating hotel: {exception}", ex);
                await SendErrorsAsync((int)HttpStatusCode.InternalServerError, ct);
            }
        }
    }
}