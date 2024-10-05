using Application.Abstractions.Storage;

namespace Web.Api.Endpoints;

public class Files : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("files", async (IFormFile file, IBlobService blobService) =>
        {
            using Stream stream = file.OpenReadStream();

            Guid fileId = await blobService.UploadAsync(stream, file.ContentType);

            return Results.Ok(fileId);
        })
        .WithTags("Files")
        .DisableAntiforgery();

        app.MapGet("files/{fileId}", async (Guid fileId, IBlobService blobService) =>
        {
            FileResponse fileResponse = await blobService.DownloadAsync(fileId);

            return Results.File(fileResponse.Stream, fileResponse.ContentType);
        })
        .WithTags("Files");

        app.MapDelete("files/{fileId}", async (Guid fileId, IBlobService blobService) =>
        {
            await blobService.DeleteAsync(fileId);

            return Results.NoContent();
        })
        .WithTags("Files");
    }
}
