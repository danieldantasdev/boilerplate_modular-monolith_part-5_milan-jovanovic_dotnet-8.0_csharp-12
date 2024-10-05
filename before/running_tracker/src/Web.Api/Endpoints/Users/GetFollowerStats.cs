using MediatR;
using Modules.Users.Application.Followers.GetFollowerStats;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

public class GetFollowerStats : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{userId}/followers/stats", async (
            Guid userId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetFollowerStatsQuery(userId);

            Result<FollowerStatsResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
