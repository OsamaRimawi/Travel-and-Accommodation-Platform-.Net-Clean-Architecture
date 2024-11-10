using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.RoomCommands;
using TBP.Core.DTOs;
using static FastEndpoints.Ep;

namespace TBP.Presentation.Endpoints.RoomEndpoints.Search
{
    internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Get("/api/rooms/search");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new SearchRoom.Command
                {
                    SearchRoomDto = new SearchRoomDto
                    {
                        Price = req.Price,
                        AdultCapacity = req.AdultCapacity,
                        ChildCapacity = req.ChildCapacity,
                        Availability = req.Availability
                    }
                }.ExecuteAsync(ct);

                if (cmdResult.ErrorMessage != null)
                {
                    await SendAsync(new Response
                    {
                        ErrorMessage = cmdResult.ErrorMessage
                    }, (int)HttpStatusCode.NotImplemented); 
                    return;
                }

                var response = Map.FromEntity(cmdResult.Rooms);
                await SendAsync(response, (int)HttpStatusCode.OK); // Send data with 200 OK
            }
            catch (Exception ex)
            {
                Logger.LogError("Error fetching  {exception}", ex);
                await SendErrorsAsync((int)HttpStatusCode.InternalServerError, ct); // Send 500 on error
            }
        }
    }
}