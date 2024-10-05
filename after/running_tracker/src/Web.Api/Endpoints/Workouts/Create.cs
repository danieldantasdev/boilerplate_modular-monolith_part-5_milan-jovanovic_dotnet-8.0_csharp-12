using MediatR;
using Modules.Training.Application.Workouts.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Workouts;

public sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("workouts", async (
            CreateWorkoutRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateWorkoutCommand(request.UserId, request.Name);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Workouts);
    }
}
