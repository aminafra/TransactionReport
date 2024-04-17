namespace Application.Transactions.Queries.GetTransactionReport;

public record GetTransactionReportDto(
    string Name,
    string Family,
    string StartDate,
    string? EndDate,
    decimal Sum,
    decimal Total);
