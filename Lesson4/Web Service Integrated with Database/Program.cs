
using System.Data.SqlClient;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/database", () =>
{
        var connectionString = "Server=tcp:omarfut-server.database.windows.net,1433;Initial Catalog=test;Persist Security Info=False;User ID=omarfut;Password=81Osuzov;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        
        using var connection = new SqlConnection(connectionString);
        
        connection.Open();

        using var command = new SqlCommand("SELECT [Name] FROM [Users]", connection);
        using var reader = command.ExecuteReader();

        Object data = 0;

        while (reader.Read())
        {
            data = reader.GetString(0);
        }

    return data;
})
.WithName("Database")
.WithOpenApi();

app.MapGet("/blob", async () =>
{
    const string connectionString = "DefaultEndpointsProtocol=https;AccountName=testomarfut123;AccountKey=AgIjuQSMaw6iq9gKDP0hJIGe0ephPoHnjil9Q/1JQXGJhIbROGPZiI/cz6WPtLBUXZsCglJSBxB++AStQMi1yA==;EndpointSuffix=core.windows.net";
    const string containerName = "cont";
    const string blobName = "data.txt";

    var blobServiceClient = new BlobServiceClient(connectionString);
    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
    var blobClient = containerClient.GetBlobClient(blobName);

    var localFilePath = Path.Combine(Path.GetTempPath(), blobName);
    await blobClient.DownloadToAsync(localFilePath);

    string[] lines = File.ReadAllLines(localFilePath);
    return lines;
})
.WithName("Blob")
.WithOpenApi();

app.Run();

