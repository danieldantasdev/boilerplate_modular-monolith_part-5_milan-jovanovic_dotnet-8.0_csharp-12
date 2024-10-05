using System.Data;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using SharedKernel;

namespace Modules.Users.Application.Followers.GetRecentFollowers;

internal sealed class GetRecentFollowersQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetRecentFollowersQuery, List<FollowerResponse>>
{
    public async Task<Result<List<FollowerResponse>>> Handle(
        GetRecentFollowersQuery request,
        CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                u.id AS Id,
                u.name AS Name
            FROM followers f
            JOIN users u ON u.id = f.user_id
            WHERE f.followed_id = @UserId
            ORDER BY created_on_utc DESC
            LIMIT @Limit
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        IEnumerable<FollowerResponse> followers = await connection.QueryAsync<FollowerResponse>(
            sql,
            new
            {
                request.UserId,
                Limit = 10
            });

        return followers.ToList();
    }
}
