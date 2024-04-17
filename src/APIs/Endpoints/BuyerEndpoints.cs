using Application.Buyers.Queries.GetMaxBuyer;
using Application.Buyers.Queries.GetMaxBuyerInDate;
using MediatR;

namespace APIs.Endpoints;

public static class BuyerEndpoints
{
    public static void MapBuyerEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("GetMaxBuyer", (
            ISender sender,
            CancellationToken ct) => sender.Send(new GetMaxBuyerQuery(), ct));

        builder.MapPost("GetMaxBuyerInDate", (
            GetMaxBuyerInDateQuery query,
            ISender sender,
            CancellationToken ct) => sender.Send(query, ct));
    }
}
