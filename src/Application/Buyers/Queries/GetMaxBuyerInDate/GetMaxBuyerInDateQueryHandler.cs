using Application.Buyers.Queries.GetMaxBuyer;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Buyers.Queries.GetMaxBuyerInDate;
public class GetMaxBuyerInDateQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetMaxBuyerInDateQuery, GetMaxBuyerDto?>
{
    public async Task<GetMaxBuyerDto?> Handle(
        GetMaxBuyerInDateQuery request,
        CancellationToken cancellationToken)
        => await context.Persons
            .Include(p => p.Transactions)
            .OrderByDescending(x => x.Transactions
                .Where(t => t.TransactionDate >= request.StartDate && t.TransactionDate <= request.EndDate)
                .Sum(t => t.Price))
            .Select(x => new GetMaxBuyerDto(
                x.Name,
                x.Family))
            .FirstOrDefaultAsync(cancellationToken);

}
