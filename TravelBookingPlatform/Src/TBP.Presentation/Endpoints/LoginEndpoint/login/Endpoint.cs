using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.LoginCommands;

namespace TBP.Presentation.Endpoints.LoginEndpoint.login
{
    internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Post("/api/login");
            AllowAnonymous();
            Validator<Request.Validator>(); // Apply the validator
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new Login.Command
                {
                    Username = req.Username,
                    Password = req.PasswordHash
                }.ExecuteAsync(ct);

                if (cmdResult.ErrorMessage != null)
                {
                    await SendAsync(new Response
                    {
                        ErrorMessage = cmdResult.ErrorMessage
                    }, (int)HttpStatusCode.Unauthorized);
                    return;
                }

                var response = new Response { Token = cmdResult.Token };
                await SendAsync(response, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error during login: {exception}", ex);
                await SendErrorsAsync((int)HttpStatusCode.InternalServerError, ct);
            }
        }
    }
}