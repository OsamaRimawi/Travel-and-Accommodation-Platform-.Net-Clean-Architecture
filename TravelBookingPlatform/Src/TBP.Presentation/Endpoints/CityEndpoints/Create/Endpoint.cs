using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.CityCommands;

namespace Create
{
    internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Post("/api/cities");
            Policies("AdminOnly");
            Validator<Request.Validator>(); // Apply the validator
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new CreateCity.Command {
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
                await SendAsync(response, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error creating city: {exception}", ex);
                await SendErrorsAsync((int)HttpStatusCode.InternalServerError, ct);
            }
        }
    }
}