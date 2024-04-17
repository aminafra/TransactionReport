using Application.Buyers.Queries.GetMaxBuyer;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace UnitTests.Application.Buyers.GetMaxBuyer
{
    public class GetMaxBuyerQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsMaxBuyer()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var mockDbSet = new Mock<DbSet<Person>>();

            var persons = new List<Person>
            {
                new Person
                {
                    Id = 1, Name = "Jane", Family = "Parker",
                    Transactions = new List<Domain.Entities.Transaction> { new Domain.Entities.Transaction
                        {
                            Id = 1,
                            TransactionDate = DateTime.Now,
                            Price = 100,
                            PersonId = 1,
                        }
                    }
                },
                new Person
                {
                    Id = 2,
                    Name = "Mike", Family = "Copper",
                    Transactions = new List<Domain.Entities.Transaction> { new Domain.Entities.Transaction
                        {
                            Id = 2,
                            TransactionDate = DateTime.Today,
                            Price = 200,
                            PersonId = 2,
                        }
                    }
                }
            }.AsQueryable();
            mockDbSet = persons.BuildMockDbSet();

            mockContext.Setup(c => c.Persons).Returns(mockDbSet.Object);

            var handler = new GetMaxBuyerQueryHandler(mockContext.Object);

            // Act
            var result = await handler.Handle(new GetMaxBuyerQuery(), It.IsAny<CancellationToken>());

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Mike", result.Name);
            Assert.Equal("Copper", result.Family);
        }
    }
}