using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class GetData(ILogger<GetData> logger)
    {
        [Function("GetData")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            const string connectionString = "";
            const string containerName = "";
            const string blobName = "";

            var containerClient = new BlobContainerClient(connectionString, containerName);

            var blobCLient = containerClient.GetBlobClient(blobName);

            var ms = new MemoryStream();

            await blobCLient.DownloadToAsync(ms);

            byte[] imageBytes = ms.ToArray();

            return new FileContentResult(imageBytes, "image/png");
        }
    }
}
