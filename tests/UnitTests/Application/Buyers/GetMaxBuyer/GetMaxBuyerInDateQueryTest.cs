using Application.Buyers.Queries.GetMaxBuyerInDate;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace UnitTests.Application.Buyers.GetMaxBuyer
{
    public class GetMaxBuyerInDateQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsMaxBuyerInDateRange()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var mockDbSet = new Mock<DbSet<Person>>();

            var persons = new List<Person>
            {
                new Person { Name = "Jane", Family = "Parker", Transactions = new List<Domain.Entities.Transaction> { new Domain.Entities.Transaction { Price = 100 } } },
                new Person
                {
                    Name = "Mike", Family = "Copper",
                    Transactions = new List<Domain.Entities.Transaction> { new Domain.Entities.Transaction { Price = 200 } }
                }
            }.AsQueryable();

            mockDbSet = persons.BuildMockDbSet();
            mockContext.Setup(c => c.Persons).Returns(mockDbSet.Object);

            var handler = new GetMaxBuyerInDateQueryHandler(mockContext.Object);
            var query = new GetMaxBuyerInDateQuery { StartDate = new DateTime(2022, 4, 1), EndDate = new DateTime(2022, 4, 2) };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Jane", result.Name);
            Assert.Equal("Parker", result.Family);
        }
    }
}
