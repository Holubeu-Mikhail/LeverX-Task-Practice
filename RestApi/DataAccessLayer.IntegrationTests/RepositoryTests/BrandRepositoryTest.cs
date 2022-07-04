using DataAccessLayer.IntegrationTests.Common;
using DataAccessLayer.IntegrationTests.Helpers;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.IntegrationTests.RepositoryTests
{
    internal class BrandRepositoryTest
    {
        private IRepository<Brand> _repository;

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(ConnectionService.GetConnectionString());
            var dbContext = new AppDbContext(optionsBuilder.Options);

            _repository = new EntityRepository<Brand>(dbContext);
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
            var expectedEntities = new List<Brand> { new Brand { Id = 1, Name = "Apple", Description = "American company", TownId = 1 } };

            //Act
            var entities = _repository.GetAll().ToList();

            //Assert
            entities.Should().BeEquivalentTo(expectedEntities, options => options.Excluding(x => x.Id));
        }

        [Test]
        public void Get_ValidCall_GetItemFromDatabase()
        {
            //Arrange
            var expectedEntity = new Brand { Id = 1, Name = "Apple", Description = "American company", TownId = 1 };

            //Act
            var entity = _repository.Get(1);

            //Assert
            entity.Should().BeEquivalentTo(expectedEntity);
        }

        [Test]
        public void Create_ValidCall_InsertItemInDatabase()
        {
            //Arrange
            var entities = new List<Brand> { new Brand { Id = 1, Name = "Apple", Description = "American company", TownId = 1 },
                new Brand { Id = 2, Name = "Samsung", Description = "European company", TownId = 1 } };
            var entity = new Brand { Id = 2, Name = "Samsung", Description = "European company", TownId = 1 };

            //Act
            _repository.Create(entity);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }

        [Test]
        public void Update_ValidCall_UpdateItemInDatabase()
        {
            //Arrange
            var entity = new Brand { Id = 1, Name = "Xiaomi", Description = "Chinese company", TownId = 1 };
            var entities = new List<Brand> { entity };

            //Act
            _repository.Update(entity);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }

        [Test]
        public void Delete_ValidCall_UpdateItemInDatabase()
        {
            //Arrange
            var entities = new List<Brand> { new Brand { Id = 1, Name = "Apple", Description = "American company", TownId = 1 } };
            var entity = new Brand { Id = 2, Name = "Samsung", Description = "European company", TownId = 1 };
            _repository.Create(entity);
            var id = 2;

            //Act
            _repository.Delete(id);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }
    }
}
