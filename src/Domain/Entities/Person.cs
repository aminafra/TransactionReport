using Domain.Common;

namespace Domain.Entities;
public class Person : BaseEntity
{
    public string Name { get; set; }
    public string Family { get; set; }
}
