using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.FeaturedDealCommands;

namespace TBP.Presentation.Endpoints.FeaturedDealEndpoints.GetAll
{
    internal sealed class Endpoint : Endpoint<EmptyRequest, Response, Mapper>
    {
        public override void Configure()
        {
            Get("/api/featured-deals");
        }

        public override async Task HandleAsync(EmptyRequest r, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new GetFeaturedDeals.Command()
                .ExecuteAsync(ct);

                if (cmdResult.ErrorMessage != null)
                {
                    await SendAsync(new Response
                    {
                        ErrorMessage = cmdResult.ErrorMessage
                    }, (int)HttpStatusCode.NotImplemented);
                    return;
                }

                var response = Map.FromEntity(cmdResult.FeaturedDeals);
                await SendAsync(response, (int)HttpStatusCode.OK); // Send data with 200 OK
            }
            catch (Exception ex)
            {
                Logger.LogError("Error fetching featured deals: {exception}", ex);
                await SendErrorsAsync((int)HttpStatusCode.InternalServerError, ct); // Send 500 on error
            }
        }
    }
}