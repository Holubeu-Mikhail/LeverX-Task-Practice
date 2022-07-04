using DataAccessLayer.IntegrationTests.Common;
using DataAccessLayer.IntegrationTests.Helpers;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.IntegrationTests.RepositoryTests
{
    internal class ProductTypeRepositoryTest
    {
        private IRepository<ProductType> _repository;

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(ConnectionService.GetConnectionString());
            var dbContext = new AppDbContext(optionsBuilder.Options);

            _repository = new EntityRepository<ProductType>(dbContext);
            BackupService.CreateDatabaseBackup();
            DataHelper.DeleteAllFromDatabase();
            TownDataHelper.FillTable();
            BrandDataHelper.FillTable();
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
            var expectedEntities = new List<ProductType> { new ProductType { Id = 1, Name = "Food" } };

            //Act
            var entities = _repository.GetAll().ToList();

            //Assert
            entities.Should().BeEquivalentTo(expectedEntities, options => options.Excluding(x => x.Id));
        }

        [Test]
        public void Get_ValidCall_GetItemFromDatabase()
        {
            //Arrange
            var expectedEntity = new ProductType { Id = 1, Name = "Food" };

            //Act
            var entity = _repository.Get(1);

            //Assert
            entity.Should().BeEquivalentTo(expectedEntity);
        }

        [Test]
        public void Create_ValidCall_InsertItemInDatabase()
        {
            //Arrange
            var entities = new List<ProductType> { new ProductType { Id = 1, Name = "Food" },
                new ProductType { Id = 2, Name = "Drinks" } };
            var entity = new ProductType { Id = 2, Name = "Drinks" };

            //Act
            _repository.Create(entity);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }

        [Test]
        public void Update_ValidCall_UpdateItemInDatabase()
        {
            //Arrange
            var entity = new ProductType { Id = 1, Name = "Drinks" };
            var entities = new List<ProductType> { entity };

            //Act
            _repository.Update(entity);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }

        [Test]
        public void Delete_ValidCall_UpdateItemInDatabase()
        {
            //Arrange
            var entities = new List<ProductType> { new ProductType { Id = 1, Name = "Food" } };
            var entity = new ProductType { Id = 2, Name = "Drinks" };
            _repository.Create(entity);
            var id = 2;

            //Act
            _repository.Delete(id);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }
    }
}
