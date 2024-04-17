using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Buyers.Queries.GetMaxBuyer;
public class GetMaxBuyerQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetMaxBuyerQuery, GetMaxBuyerDto?>
{
    public async Task<GetMaxBuyerDto?> Handle(
        GetMaxBuyerQuery request,
        CancellationToken cancellationToken)
        => await context.Persons
            .Include(p => p.Transactions)
            .OrderByDescending(x => x.Transactions.Sum(t => t.Price))
            .Select(x => new GetMaxBuyerDto(
                x.Name,
                x.Family)
            ).FirstOrDefaultAsync(cancellationToken);
}
