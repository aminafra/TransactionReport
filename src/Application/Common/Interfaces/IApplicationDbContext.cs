using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;
public interface IApplicationDbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
