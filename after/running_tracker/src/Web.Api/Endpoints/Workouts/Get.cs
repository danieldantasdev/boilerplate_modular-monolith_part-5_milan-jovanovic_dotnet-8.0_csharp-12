using MediatR;
using Modules.Training.Application.Workouts.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Workouts;

public sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("workouts/{workoutId}", async (
            Guid workoutId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetWorkoutByIdQuery(workoutId);

            Result<WorkoutResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Workouts);
    }
}
