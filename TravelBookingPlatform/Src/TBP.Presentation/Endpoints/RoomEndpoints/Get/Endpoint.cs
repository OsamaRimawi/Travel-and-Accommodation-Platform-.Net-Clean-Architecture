﻿using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.RoomCommands;

namespace TBP.Presentation.Endpoints.RoomEndpoints.Get
{
    internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Get("/api/rooms/{id}");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new GetRoom.Command { Id = req.Id }
                    .ExecuteAsync(ct);

                if (cmdResult.ErrorMessage != null)
                {
                    await SendAsync(new Response
                    {
                        ErrorMessage = cmdResult.ErrorMessage
                    }, (int)HttpStatusCode.NotFound);
                    return;
                }

                var response = Map.FromEntity(cmdResult.Room);
                await SendAsync(response, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error fetching room: {exception}", ex);
                await SendErrorsAsync((int)HttpStatusCode.InternalServerError, ct);
            }
        }
    }
}