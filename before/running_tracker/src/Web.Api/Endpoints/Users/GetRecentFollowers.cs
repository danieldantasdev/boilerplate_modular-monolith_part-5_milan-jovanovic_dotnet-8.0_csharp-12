using MediatR;
using Modules.Users.Application.Followers.GetRecentFollowers;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

public class GetRecentFollowers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{userId}/followers/recent", async (
            Guid userId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetRecentFollowersQuery(userId);

            Result<List<FollowerResponse>> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
