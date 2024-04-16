namespace APIs.Endpoints;

public static class TransactionEndpoints
{
    public static void MapTransactionEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("GetTransactionReport/{userId:int}", (
            int? userId,
            CancellationToken ct) =>
        {

        });
    }
}
