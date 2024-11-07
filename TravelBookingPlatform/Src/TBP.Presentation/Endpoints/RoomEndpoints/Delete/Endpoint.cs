using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.RoomCommands;

namespace TBP.Presentation.Endpoints.RoomEndpoints.Delete
{
    internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Delete("/api/rooms/{id}");
            Policies("AdminOnly");
            Validator<Request.Validator>(); // Apply the validator
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new DeleteRoom.Command { Id = req.Id }
                    .ExecuteAsync(ct);

                if (cmdResult.ErrorMessage != null)
                {
                    await SendAsync(new Response
                    {
                        ErrorMessage = cmdResult.ErrorMessage
                    }, (int)HttpStatusCode.NotFound);
                    return;
                }

                await SendNoContentAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error deleting room: {exception}", ex);
                await SendErrorsAsync((int)HttpStatusCode.InternalServerError, ct);
            }
        }
    }
}