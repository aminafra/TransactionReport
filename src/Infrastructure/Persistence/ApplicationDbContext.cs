using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence;
public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}