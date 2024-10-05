using MediatR;
using Modules.Training.Application.Workouts.AddExercises;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Workouts;

public sealed class AddExercises : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("workouts/{workoutId}/exercises", async (
            Guid workoutId,
            List<ExerciseRequest> exercises,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new AddExercisesCommand(workoutId, exercises);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Workouts);
    }
}
