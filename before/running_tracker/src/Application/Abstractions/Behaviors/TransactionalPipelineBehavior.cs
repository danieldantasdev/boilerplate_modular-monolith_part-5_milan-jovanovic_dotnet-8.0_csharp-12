using System.Data;
using Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Abstractions.Behaviors;

internal sealed class TransactionalPipelineBehavior<TRequest, TResponse>(
    // TODO: Fix this later - IUnitOfWork unitOfWork,
    ILogger<TransactionalPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactionalCommand
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Beginning transaction for {RequestName}", typeof(TRequest).Name);

        using IDbTransaction transaction = default!;

        TResponse response = await next();

        transaction.Commit();

        logger.LogInformation("Committed transaction for {RequestName}", typeof(TRequest).Name);

        return response;
    }
}
