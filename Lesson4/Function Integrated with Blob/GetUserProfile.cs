using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Function;

public class GetUserProfile(ILogger<GetUserProfile> logger)
{
    [Function("GetUserProfile")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        logger.LogInformation($"Azure funbction call from {req.Headers.UserAgent}");

        return new OkObjectResult(new
        {
            Id = 1,
            Name = "Andrii Panochyshyn",
            Email = "andrewpn92@gmail.com"
        });
    }
}


