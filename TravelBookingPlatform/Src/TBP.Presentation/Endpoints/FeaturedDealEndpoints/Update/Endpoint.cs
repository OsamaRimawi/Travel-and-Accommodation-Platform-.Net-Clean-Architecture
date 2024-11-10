using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.FeaturedDealCommands;

namespace TBP.Presentation.Endpoints.FeaturedDealEndpoints.Update
{
    internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Put("/api/featured-deals/{id}");
            Policies("AdminOnly");
            Validator<Request.Validator>(); // Apply the validator
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new UpdateFeaturedDeal.Command
                {
                    Id = req.Id,
                    FeaturedDealDto = req.FeaturedDeal
                }.ExecuteAsync(ct);

                if (cmdResult.ErrorMessage != null)
                {
                    await SendAsync(new Response
                    {
                        ErrorMessage = cmdResult.ErrorMessage
                    }, (int)HttpStatusCode.BadRequest);
                    return;
                }

                var response = Map.FromEntity(cmdResult.FeaturedDeal);
                await SendAsync(response, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error updating featured deal: {exception}", ex);
                await SendErrorsAsync((int)HttpStatusCode.InternalServerError, ct);
            }
        }
    }
}