using Domain.Common;

namespace Domain.Entities;
public class Transaction : BaseEntity
{
    public DateTime TransactionDate { get; set; }
    public decimal Price { get; set; }

    public int PersonId { get; set; }
    public virtual Person Person { get; set; }
}
