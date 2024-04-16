using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class PersonConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.Id)
            .HasName("PersonId");

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Family)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(x => x.Transactions)
            .WithOne(z => z.Person)
            .HasForeignKey(x => x.PersonId)
            .HasPrincipalKey(x => x.Id);
    }
}
