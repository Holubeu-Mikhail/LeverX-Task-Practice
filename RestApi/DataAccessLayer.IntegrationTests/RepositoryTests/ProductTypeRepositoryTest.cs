using DataAccessLayer.DbContexts;
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
            var optionsBuilder = new DbContextOptionsBuilder<EntityDbContext>();
            optionsBuilder.UseSqlServer(ConnectionService.GetConnectionString());
            var dbContext = new EntityDbContext(optionsBuilder.Options);

            _repository = new EntityRepository<ProductType>(dbContext);
            BackupService.CreateDatabaseBackup();
            DataHelper.DeleteAllFromDatabase();
            CityDataHelper.FillTable();
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
            var expectedEntities = new List<ProductType> { new ProductType { Id = DataHelper.ProductTypeId, Name = "Food" } };

            //Act
            var entities = _repository.GetAll().ToList();

            //Assert
            entities.Should().BeEquivalentTo(expectedEntities, options => options.Excluding(x => x.Id));
        }

        [Test]
        public void Get_ValidCall_GetItemFromDatabase()
        {
            //Arrange
            var expectedEntity = new ProductType { Id = DataHelper.ProductTypeId, Name = "Food" };

            //Act
            var entity = _repository.Get(DataHelper.ProductTypeId);

            //Assert
            entity.Should().BeEquivalentTo(expectedEntity);
        }

        [Test]
        public void Create_ValidCall_InsertItemInDatabase()
        {
            //Arrange
            var guid = new Guid("dc3f8b75-5414-4775-9f3b-dbeaef579df6");
            var entities = new List<ProductType> { new ProductType { Id = DataHelper.ProductTypeId, Name = "Food" },
                new ProductType { Id = guid, Name = "Drinks" } };
            var entity = new ProductType { Id = guid, Name = "Drinks" };

            //Act
            _repository.Create(entity);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }

        [Test]
        public void Update_ValidCall_UpdateItemInDatabase()
        {
            //Arrange
            var entity = new ProductType { Id = DataHelper.ProductTypeId, Name = "Drinks" };
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
            var entities = new List<ProductType> { new ProductType { Id = DataHelper.ProductTypeId, Name = "Food" } };
            var entity = new ProductType { Id = Guid.NewGuid(), Name = "Drinks" };
            _repository.Create(entity);
            var id = entity.Id;

            //Act
            _repository.Delete(id);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }
    }
}
