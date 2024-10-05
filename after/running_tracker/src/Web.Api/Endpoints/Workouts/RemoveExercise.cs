using MediatR;
using Modules.Training.Application.Workouts.RemoveExercise;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Workouts;

public sealed class RemoveExercise : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("workouts/{workoutId}/exercises/{exerciseId}", async (
            Guid workoutId,
            Guid exerciseId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new RemoveExerciseCommand(workoutId, exerciseId);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Workouts);
    }
}
