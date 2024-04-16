using Domain.Common;

namespace Domain.Entities;
public class Person : BaseEntity
{
    public string Name { get; set; }
    public string Family { get; set; }

    public ICollection<Transaction> Transactions { get; set; }
}
