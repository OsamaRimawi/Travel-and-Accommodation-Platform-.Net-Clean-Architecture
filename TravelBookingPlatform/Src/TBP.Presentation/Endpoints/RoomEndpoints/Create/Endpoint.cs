using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.RoomCommands;

namespace TBP.Presentation.Endpoints.RoomEndpoints.Create
{
    internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Post("/api/rooms");
            Policies("AdminOnly");
            Validator<Request.Validator>(); // Apply the validator
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new CreateRoom.Command
                {
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
                await SendAsync(response, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error creating room: {exception}", ex);
                await SendErrorsAsync((int)HttpStatusCode.InternalServerError, ct);
            }
        }
    }
}