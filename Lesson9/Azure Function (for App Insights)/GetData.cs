using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MyProject.Functions
{
    public class GetDataFunction
    {
        private readonly ILogger<GetDataFunction> _logger;

        public GetDataFunction(ILogger<GetDataFunction> logger)
        {
            _logger = logger;
        }

        [Function("GetData")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "GetData/{error}")] HttpRequest req,
            bool error = false)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            if (error)
            {
                throw new Exception("Error in Azure Function.");
            }
            
            return new OkObjectResult("Your data.");
        }
    }
}
