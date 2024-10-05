using MediatR;
using Modules.Training.Application.Workouts.Remove;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Workouts;

public sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("workouts/{workoutId}", async (
            Guid workoutId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new RemoveWorkoutCommand(workoutId);

            Result result = await sender.Send(query, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Workouts);
    }
}
