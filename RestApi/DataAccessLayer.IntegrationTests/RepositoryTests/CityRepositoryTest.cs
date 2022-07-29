using DataAccessLayer.DbContexts;
using DataAccessLayer.IntegrationTests.Common;
using DataAccessLayer.IntegrationTests.Helpers;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.IntegrationTests.RepositoryTests
{
    internal class CityRepositoryTest
    {
        private IRepository<City> _repository;

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EntityDbContext>();
            optionsBuilder.UseSqlServer(ConnectionService.GetConnectionString());
            var dbContext = new EntityDbContext(optionsBuilder.Options);

            _repository = new EntityRepository<City>(dbContext);
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
            var expectedEntities = new List<City> { new City { Id = DataHelper.CityId, Name = "Zhlobin", Code = 201 } };

            //Act
            var entities = _repository.GetAll().ToList();

            //Assert
            entities.Should().BeEquivalentTo(expectedEntities, options => options.Excluding(x => x.Id));
        }

        [Test]
        public void Get_ValidCall_GetItemFromDatabase()
        {
            //Arrange
            var expectedEntity = new City { Id = DataHelper.CityId, Name = "Zhlobin", Code = 201 };

            //Act
            var entity = _repository.Get(DataHelper.CityId);

            //Assert
            entity.Should().BeEquivalentTo(expectedEntity);
        }

        [Test]
        public void Create_ValidCall_InsertItemInDatabase()
        {
            //Arrange
            var guid = new Guid("dc3f8b75-5414-4775-9f3b-dbeaef579df6");
            var entities = new List<City> { new City { Id = DataHelper.CityId, Name = "Zhlobin", Code = 201 },
                new City { Id = guid, Name = "Homel", Code = 242 } };
            var entity = new City { Id = guid, Name = "Homel", Code = 242 };

            //Act
            _repository.Create(entity);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }

        [Test]
        public void Update_ValidCall_UpdateItemInDatabase()
        {
            //Arrange
            var entity = new City { Id = DataHelper.CityId, Name = "Minsk", Code = 232 };
            var entities = new List<City> { entity };

            //Act
            _repository.Update(entity);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }

        [Test]
        public void Delete_ValidCall_UpdateItemInDatabase()
        {
            //Arrange
            var entities = new List<City> { new City { Id = DataHelper.CityId, Name = "Zhlobin", Code = 201 } };
            var entity = new City { Id = Guid.NewGuid(), Name = "Minsk", Code = 232 };
            _repository.Create(entity);
            var id = entity.Id;

            //Act
            _repository.Delete(id);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }
    }
}
