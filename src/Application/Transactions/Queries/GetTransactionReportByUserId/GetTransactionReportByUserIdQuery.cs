using Application.Transactions.Queries.GetTransactionReport;
using MediatR;

namespace Application.Transactions.Queries.GetTransactionReportByUserId;
public record GetTransactionReportByUserIdQuery(int UserId) : IRequest<List<GetTransactionReportDto>>;
