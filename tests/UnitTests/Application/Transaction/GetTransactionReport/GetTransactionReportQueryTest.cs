using Application.Common.Interfaces;
using Application.Transactions.Queries.GetTransactionReport;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace UnitTests.Application.Transaction.GetTransactionReport
{
    public class GetTransactionReportQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsTransactionReportList()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var mockDbSet = new Mock<DbSet<Person>>();

            var persons = new List<Person>
            {
                new Person { Name = "Mike", Family = "Copper", Transactions = new List<Domain.Entities.Transaction> { new Domain.Entities.Transaction { Price = 100, TransactionDate = new DateTime(2022, 4, 1) } } },
                new Person { Name = "Jane", Family = "Parker", Transactions = new List<Domain.Entities.Transaction> { new Domain.Entities.Transaction { Price = 200, TransactionDate = new DateTime(2022, 4, 3) } } }
            }.AsQueryable();

            mockDbSet = persons.BuildMockDbSet();
            mockContext.Setup(c => c.Persons).Returns(mockDbSet.Object);

            var handler = new GetTransactionReportQueryHandler(mockContext.Object);

            // Act
            var result = await handler.Handle(new GetTransactionReportQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Mike", result[0].Name);
            Assert.Equal("Copper", result[0].Family);
        }
    }
}
