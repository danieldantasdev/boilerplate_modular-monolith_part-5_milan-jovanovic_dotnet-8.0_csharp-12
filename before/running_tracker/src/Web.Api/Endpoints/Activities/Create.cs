using MediatR;
using Modules.Training.Application.Activities.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Activities;

public sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("activities", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateActivityCommand(
                request.UserId,
                request.WorkoutId,
                request.DistanceInMeters,
                request.DurationInSeconds);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Activities);
    }

    public sealed record Request(Guid UserId, Guid WorkoutId, decimal DistanceInMeters, int DurationInSeconds);
}
