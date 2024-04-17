using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Transactions.Queries.GetTransactionReportByUserId;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace UnitTests.Application.Transaction.GetTransactionReport
{
    public class GetTransactionReportByUserIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidUserId_ReturnsTransactionReportList()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var mockDbSet = new Mock<DbSet<Person>>();

            var persons = new List<Person>{
                new Person
                {
                    Id = 1,
                    Name = "Mike", Family = "Copper",
                    Transactions = new List<Domain.Entities.Transaction>
                    {
                        new() { Price = 100, TransactionDate = new DateTime(2022, 4, 1) },
                        new() { Price = 200, TransactionDate = new DateTime(2022, 4, 3) }
                    }
                }};

            mockDbSet = persons.BuildMockDbSet();

            mockContext.Setup(c => c.Persons).Returns(mockDbSet.Object);

            var handler = new GetTransactionReportByUserIdQueryHandler(mockContext.Object);
            var query = new GetTransactionReportByUserIdQuery(1);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Mike", result[0].Name);
            Assert.Equal("Copper", result[0].Family);
        }

        [Fact]
        public void Handle_WithInvalidUserId_ThrowsUserNotFoundException()
        {
            // Arrange
            var userId = 1;
            var mockContext = new Mock<IApplicationDbContext>();
            var mockDbSet = new Mock<DbSet<Person>>();

            mockDbSet.Setup(x => x.FindAsync(userId)).ReturnsAsync((Person)null);

            mockContext.Setup(c => c.Persons).Returns(mockDbSet.Object);

            var handler = new GetTransactionReportByUserIdQueryHandler(mockContext.Object);
            var query = new GetTransactionReportByUserIdQuery(userId);

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(() => handler.Handle(query, CancellationToken.None));
        }
    }
}
