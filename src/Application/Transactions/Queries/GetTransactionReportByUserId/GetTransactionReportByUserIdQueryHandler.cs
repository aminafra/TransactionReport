using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Transactions.Queries.GetTransactionReport;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetTransactionReportByUserId;
public class GetTransactionReportByUserIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetTransactionReportByUserIdQuery, List<GetTransactionReportDto>>
{
    public async Task<List<GetTransactionReportDto>> Handle(
        GetTransactionReportByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var person = await context.Persons
            .Include(x => x.Transactions)
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (person is null)
        {
            throw new UserNotFoundException();
        }

        var transactionReportList = new List<GetTransactionReportDto>();

        decimal total = 0;
        var timeGroup = person.Transactions.GroupBy(x => x.TransactionDate.Date);
        foreach (var transactionReport in timeGroup)
        {
            var sum = transactionReport.Sum(x => x.Price);
            total += sum;
            transactionReportList.Add(new GetTransactionReportDto(
                person.Name,
                person.Family,
                StartDate: transactionReport.Key.ToShortDateString(),
                EndDate: timeGroup
                    .FirstOrDefault(x => x.Key > transactionReport.Key)?
                    .Key.ToShortDateString(),
                sum,
                total
            ));
        }
        return transactionReportList;
    }
}
