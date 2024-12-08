// using DriveSalez.Domain.Entities;
// using DriveSalez.Persistence.DbContext;
// using DriveSalez.Persistence.Repositories;
// using Microsoft.EntityFrameworkCore;
// using Moq;
//
// namespace DriveSalez.Tests.Repositories.RepositoryTests;
//
// public class OneTimePurchaseRepositoryTests
// {
//     [Fact]
//     public async Task GetByIdAsync_ReturnsCorrectEntity_WhenEntityExists()
//     {
//         // Arrange
//         var mockSet = new Mock<DbSet<OneTimePurchase>>();
//         var data = new List<OneTimePurchase>
//         {
//             new OneTimePurchase { Id = 1, Name = "Purchase No.1" },
//             new OneTimePurchase { Id = 2, Name = "Purchase No.2" }
//         }.AsQueryable();
//             
//         mockSet.As<IQueryable<OneTimePurchase>>().Setup(m => m.Provider).Returns(data.Provider);
//         mockSet.As<IQueryable<OneTimePurchase>>().Setup(m => m.Expression).Returns(data.Expression);
//         mockSet.As<IQueryable<OneTimePurchase>>().Setup(m => m.ElementType).Returns(data.ElementType);
//         mockSet.As<IQueryable<OneTimePurchase>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
//
//         
//         var mockContext = new Mock<ApplicationDbContext>();
//         mockContext.Setup(c => c.Set<OneTimePurchase>()).Returns(mockSet.Object);
//
//         var repository = new OneTimePurchaseRepository(mockContext.Object);
//
//         // Act
//         var result = await repository.GetByIdAsync(1);
//
//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(1, result.Id);
//         Assert.Equal("Purchase No.1", result.Name);
//     }
//
//     // [Fact]
//     // public async Task GetByIdAsync_ReturnsNull_WhenEntityDoesNotExist()
//     // {
//     //     // Arrange
//     //     var mockSet = CreateMockDbSet(new List<OneTimePurchase>
//     //     {
//     //         new OneTimePurchase { Id = 1, Name = "Purchase No.1" }
//     //     });
//     //
//     //     var mockContext = new Mock<ApplicationDbContext>();
//     //     mockContext.Setup(c => c.Set<OneTimePurchase>()).Returns(mockSet.Object);
//     //
//     //     var repository = new OneTimePurchaseRepository(mockContext.Object);
//     //
//     //     // Act
//     //     var result = await repository.GetByIdAsync(99);
//     //
//     //     // Assert
//     //     Assert.Null(result);
//     // }
//     //
//     // private Mock<DbSet<T>> CreateMockDbSet<T>(IEnumerable<T> data) where T : class
//     // {
//     //     var queryableData = data.AsQueryable();
//     //     var mockSet = new Mock<DbSet<T>>();
//     //
//     //     mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
//     //     mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
//     //     mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
//     //     mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());
//     //
//     //     mockSet.Setup(x => x).Returns(mockSet.Object);
//     //     return mockSet;
//     // }
// }