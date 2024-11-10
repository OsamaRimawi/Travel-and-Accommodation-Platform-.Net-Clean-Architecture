using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.RoomCommands;

namespace TBP.Presentation.Endpoints.RoomEndpoints.Update
{
    internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Put("/api/rooms/{id}");
            Policies("AdminOnly");
            Validator<Request.Validator>(); // Apply the validator
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new UpdateRoom.Command
                {
                    Id = req.Id,
                    RoomDto = req.room
                }.ExecuteAsync(ct);

                if (cmdResult.ErrorMessage != null)
                {
                    await SendAsync(new Response
                    {
                        ErrorMessage = cmdResult.ErrorMessage
                    }, (int)HttpStatusCode.BadRequest);
                    return;
                }

                var response = Map.FromEntity(cmdResult.Room);
                await SendAsync(response, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error updating room: {exception}", ex);
                await SendErrorsAsync((int)HttpStatusCode.InternalServerError, ct);
            }
        }
    }
}