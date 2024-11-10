using FastEndpoints;
using System.Net;
using TBP.Core.CommandHandlers.HotelCommands;
using TBP.Core.DTOs;
using static FastEndpoints.Ep;

namespace TBP.Presentation.Endpoints.HotelEndpoints.Search
{
    internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Get("/api/hotels/search");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            try
            {
                var cmdResult = await new SearchHotel.Command
                {
                    searchHotelDto = new SearchHotelDto
                    {
                        Name = req.Name,
                        StarRating = req.StarRating,
                        Location = req.Location,
                        CityName = req.CityName,
                        Owner = req.Owner
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

                var response = Map.FromEntity(cmdResult.Hotels);
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