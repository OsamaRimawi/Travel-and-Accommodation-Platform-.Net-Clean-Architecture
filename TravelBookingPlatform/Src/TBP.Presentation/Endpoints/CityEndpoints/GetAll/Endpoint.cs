using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.CityCommands;

namespace GetAll
{
    internal sealed class Endpoint : Endpoint<EmptyRequest, Response, Mapper>
    {
        public override void Configure()
        {
            Get("/api/cities");
        }

        public override async Task HandleAsync(EmptyRequest r, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new GetCities.Command()
                .ExecuteAsync(ct);

                if (cmdResult.ErrorMessage != null)
                {
                    await SendAsync(new Response
                    {
                        ErrorMessage = cmdResult.ErrorMessage
                    }, (int)HttpStatusCode.NotImplemented); 
                    return;
                }

                var response = Map.FromEntity(cmdResult.Cities);
                await SendAsync(response, (int)HttpStatusCode.OK); // Send data with 200 OK
            }
            catch (Exception ex)
            {
                Logger.LogError("Error fetching customer points: {exception}", ex);
                await SendErrorsAsync((int)HttpStatusCode.InternalServerError, ct); // Send 500 on error
            }
        }
    }
}