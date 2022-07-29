using DataAccessLayer.DbContexts;
using DataAccessLayer.IntegrationTests.Common;
using DataAccessLayer.IntegrationTests.Helpers;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.IntegrationTests.RepositoryTests
{
    public class ProductRepositoryTest
    {
        private IRepository<Product> _repository;

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EntityDbContext>();
            optionsBuilder.UseSqlServer(ConnectionService.GetConnectionString());
            var dbContext = new EntityDbContext(optionsBuilder.Options);

            _repository = new EntityRepository<Product>(dbContext);
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
            var expectedEntities = new List<Product> { new Product { Id = DataHelper.ProductId, Name = "Fish", Quantity = 1, TypeId = DataHelper.ProductTypeId, BrandId = DataHelper.BrandId } };

            //Act
            var entities = _repository.GetAll().ToList();

            //Assert
            entities.Should().BeEquivalentTo(expectedEntities, options => options.Excluding(x => x.Id));
        }

        [Test]
        public void Get_ValidCall_GetItemFromDatabase()
        {
            //Arrange
            var expectedEntity = new Product { Id = DataHelper.ProductId, Name = "Fish", Quantity = 1, BrandId = DataHelper.BrandId, TypeId = DataHelper.ProductTypeId };

            //Act
            var entity = _repository.Get(DataHelper.ProductId);

            //Assert
            entity.Should().BeEquivalentTo(expectedEntity);
        }

        [Test]
        public void Create_ValidCall_InsertItemInDatabase()
        {
            //Arrange
            var guid = new Guid("dc3f8b75-5414-4775-9f3b-dbeaef579df6");
            var entities = new List<Product> { new Product { Id = DataHelper.ProductId, Name = "Fish", Quantity = 1, BrandId = DataHelper.BrandId, TypeId = DataHelper.ProductTypeId },
                new Product { Id = guid, Name = "Fish2", Quantity = 1, BrandId = DataHelper.BrandId, TypeId = DataHelper.ProductTypeId } };
            var entity = new Product { Id = guid, Name = "Fish2", Quantity = 1, BrandId = DataHelper.BrandId, TypeId = DataHelper.ProductTypeId };

            //Act
            _repository.Create(entity);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }

        [Test]
        public void Update_ValidCall_UpdateItemInDatabase()
        {
            //Arrange
            var entity = new Product { Id = DataHelper.ProductId, Name = "Fish3", Quantity = 1, BrandId = DataHelper.BrandId, TypeId = DataHelper.ProductTypeId };
            var entities = new List<Product> { entity };

            //Act
            _repository.Update(entity);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }

        [Test]
        public void Delete_ValidCall_UpdateItemInDatabase()
        {
            //Arrange
            var entities = new List<Product> { new Product { Id = DataHelper.ProductId, Name = "Fish", Quantity = 1, BrandId = DataHelper.BrandId, TypeId = DataHelper.ProductTypeId } };
            var entity = new Product { Id = Guid.NewGuid(), Name = "Fish2", Quantity = 1, BrandId = DataHelper.BrandId, TypeId = DataHelper.ProductTypeId };
            _repository.Create(entity);
            var id = entity.Id;

            //Act
            _repository.Delete(id);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }
    }
}