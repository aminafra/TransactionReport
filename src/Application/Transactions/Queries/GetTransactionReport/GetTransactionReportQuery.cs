using MediatR;

namespace Application.Transactions.Queries.GetTransactionReport;

public class GetTransactionReportQuery : IRequest<List<GetTransactionReportDto>>;
