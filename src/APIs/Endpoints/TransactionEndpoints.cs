using Application.Transactions.Queries.GetTransactionReport;
using Application.Transactions.Queries.GetTransactionReportByUserId;
using MediatR;

namespace APIs.Endpoints;

public static class TransactionEndpoints
{
    public static void MapTransactionEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("GetTransactionReport", (
            ISender sender,
            CancellationToken ct) => sender.Send(new GetTransactionReportQuery(), ct));

        builder.MapGet("GetTransactionReport/{userId:int}", (
            int userId,
            ISender sender,
            CancellationToken ct) => sender.Send(new GetTransactionReportByUserIdQuery(userId), ct));
    }
}
