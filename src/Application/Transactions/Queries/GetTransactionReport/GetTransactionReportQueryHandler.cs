using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetTransactionReport;

public class GetTransactionReportQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetTransactionReportQuery, List<GetTransactionReportDto>>
{
    public async Task<List<GetTransactionReportDto>> Handle(
        GetTransactionReportQuery request,
        CancellationToken cancellationToken)
    {
        var persons = await context.Persons
            .Include(x => x.Transactions)
            .ToListAsync(cancellationToken);

        var transactionReportList = new List<GetTransactionReportDto>();
        foreach (var person in persons)
        {
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
        }
        return transactionReportList;
    }
}
