using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.FeaturedDealCommands;

namespace TBP.Presentation.Endpoints.FeaturedDealEndpoints.Get
{
    internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Get("/api/featured-deals/{id}");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new GetFeaturedDeal.Command { Id = req.Id }
                    .ExecuteAsync(ct);

                if (cmdResult.ErrorMessage != null)
                {
                    await SendAsync(new Response
                    {
                        ErrorMessage = cmdResult.ErrorMessage
                    }, (int)HttpStatusCode.NotFound);
                    return;
                }

                var response = Map.FromEntity(cmdResult.FeaturedDeal);
                await SendAsync(response, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error fetching featured deal: {exception}", ex);
                await SendErrorsAsync((int)HttpStatusCode.InternalServerError, ct);
            }
        }
    }
}