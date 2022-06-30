using DataAccessLayer.IntegrationTests.Common;
using DataAccessLayer.IntegrationTests.Helpers;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.IntegrationTests.RepositoryTests
{
    public class AreaRepositoryTest
    {
        private IRepository<Product> _repository;

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=LXIBY788;Initial Catalog=ProductsDb;Integrated Security=True");
            var dbContext = new AppDbContext(optionsBuilder.Options);

            _repository = new EntityRepository<Product>(dbContext);
            BackupService.CreateDatabaseBackup();
            DataHelper.DeleteAllFromDatabase();
            ProductTypeDataHelper.FillTable();
            ProductDataHelper.FillTable();
        }

        [TearDown]
        public void TearDown()
        {
            BackupService.RestoreDatabaseBackup();
        }

        [Test]
        public void GetAll_ValidCall_GetAllItemsFromDatabase()
        {
            //Arrange
            var expectedAreas = new List<Product> { new Product { Id = 1, Name = "Fish", Quantity = 1, TypeId = 1 } };

            //Act
            var areas = _repository.GetAll().ToList();

            //Assert
            areas.Should().BeEquivalentTo(expectedAreas, options => options.Excluding(x => x.Id));
        }
    }
}