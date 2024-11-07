using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.CityCommands;
using TBP.Infrastructure.Database.Repositories;
using TBP.Infrastructure.Database;

namespace Update
{
    internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Put("/api/cities/{id}");
            Policies("AdminOnly");
            Validator<Request.Validator>(); // Apply the validator
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new UpdateCity.Command {
                    Id = req.Id,
                    cityDto = req.city
                }.ExecuteAsync(ct);

                if (cmdResult.ErrorMessage != null)
                {
                    await SendAsync(new Response
                    {
                        ErrorMessage = cmdResult.ErrorMessage
                    }, (int)HttpStatusCode.BadRequest);
                    return;
                }

                var response = Map.FromEntity(cmdResult.City);
                await SendAsync(response, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error updating city: {exception}", ex);
                await SendErrorsAsync((int)HttpStatusCode.InternalServerError, ct);
            }
        }
    }
}