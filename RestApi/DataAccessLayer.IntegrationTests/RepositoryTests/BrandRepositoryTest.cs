using DataAccessLayer.DbContexts;
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
            var optionsBuilder = new DbContextOptionsBuilder<EntityDbContext>();
            optionsBuilder.UseSqlServer(ConnectionService.GetConnectionString());
            var dbContext = new EntityDbContext(optionsBuilder.Options);

            _repository = new EntityRepository<Brand>(dbContext);
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
            var expectedEntities = new List<Brand> { new Brand { Name = "Apple", Description = "American company", CityId = DataHelper.CityId } };

            //Act
            var entities = _repository.GetAll().ToList();

            //Assert
            entities.Should().BeEquivalentTo(expectedEntities, options => options.Excluding(x => x.Id));
        }

        [Test]
        public void Get_ValidCall_GetItemFromDatabase()
        {
            //Arrange
            var expectedEntity = new Brand { Id = DataHelper.BrandId, Name = "Apple", Description = "American company", CityId = DataHelper.CityId };

            //Act
            var entity = _repository.Get(DataHelper.BrandId);

            //Assert
            entity.Should().BeEquivalentTo(expectedEntity);
        }

        [Test]
        public void Create_ValidCall_InsertItemInDatabase()
        {
            //Arrange
            var guid = new Guid("dc3f8b75-5414-4775-9f3b-dbeaef579df6");
            var entities = new List<Brand> { new Brand { Id = DataHelper.BrandId, Name = "Apple", Description = "American company", CityId = DataHelper.CityId },
                new Brand { Id = guid, Name = "Samsung", Description = "European company", CityId = DataHelper.CityId } };
            var entity = new Brand { Id = guid, Name = "Samsung", Description = "European company", CityId = DataHelper.CityId };

            //Act
            _repository.Create(entity);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }

        [Test]
        public void Update_ValidCall_UpdateItemInDatabase()
        {
            //Arrange
            var entity = new Brand { Id = DataHelper.BrandId, Name = "Xiaomi", Description = "Chinese company", CityId = DataHelper.CityId };
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
            var entities = new List<Brand> { new Brand { Id = DataHelper.BrandId, Name = "Apple", Description = "American company", CityId = DataHelper.CityId } };
            var entity = new Brand { Id = Guid.NewGuid(), Name = "Samsung", Description = "European company", CityId = DataHelper.CityId };
            _repository.Create(entity);
            var id = entity.Id;

            //Act
            _repository.Delete(id);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }
    }
}
