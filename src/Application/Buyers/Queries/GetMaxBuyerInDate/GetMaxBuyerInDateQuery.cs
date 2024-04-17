using Application.Buyers.Queries.GetMaxBuyer;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Buyers.Queries.GetMaxBuyerInDate;
public class GetMaxBuyerInDateQuery : IRequest<GetMaxBuyerDto>
{
    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public DateTime EndDate { get; set; }
}
