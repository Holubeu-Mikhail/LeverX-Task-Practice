namespace BusinessLogicLayer.UnitTests
{
    public class CityDataProviderTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAll_ValidCall_ReturnsList()
        {
            // Arrange
            var entities = GetTestCities().AsQueryable();

            var mock = new Mock<IRepository<City>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);

            var validator = new CityValidator(mock.Object);
            var service = new DataProvider<City>(mock.Object, validator);

            // Act
            var result = service.GetAll();

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(3).And.BeEquivalentTo(entities);
        }

        [Test]
        public void Get_ValidCall_ReturnsEntity()
        {
            // Arrange
            var expectedEntity = GetTestCities()[0];

            var mock = new Mock<IRepository<City>>();
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns(expectedEntity);

            var validator = new CityValidator(mock.Object);
            var service = new DataProvider<City>(mock.Object, validator);

            // Act
            var result = service.Get(GetTestCities()[0].Id);

            // Assert
            result.Should().NotBeNull().And.BeOfType(typeof(City)).And.BeEquivalentTo(expectedEntity);
        }

        [Test]
        public void Get_InvalidCall_ReturnsNull()
        {
            // Arrange
            var mock = new Mock<IRepository<City>>();
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns((City)null);

            var validator = new CityValidator(mock.Object);
            var service = new DataProvider<City>(mock.Object, validator);

            // Act
            var result = service.Get(Guid.NewGuid());

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public void Create_ValidCall_RepositoryMethodInvokes()
        {
            // Arrange
            var t = false;
            var entity = new City
            {
                Name = "London",
                Code = 100
            };
            var entities = GetTestCities().AsQueryable();

            var mock = new Mock<IRepository<City>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Create(It.IsAny<City>()))
                .Callback(() => t = true);

            var validator = new CityValidator(mock.Object);
            var service = new DataProvider<City>(mock.Object, validator);

            // Act
            service.Create(entity);

            // Assert
            t.Should().BeTrue();
        }

        [Test]
        public void Create_InvalidCall_RepositoryMethodDoesNotInvoke()
        {
            // Arrange
            var t = false;
            var entity = new City
            {
                Name = "Minsk",
                Code = 200
            };
            var entities = GetTestCities().AsQueryable();

            var mock = new Mock<IRepository<City>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Create(It.IsAny<City>()))
                .Callback(() => t = true);

            var validator = new CityValidator(mock.Object);
            var service = new DataProvider<City>(mock.Object, validator);

            // Act
            service.Create(entity);

            // Assert
            t.Should().BeFalse();
        }

        [Test]
        public void Create_InvalidCall_ThrowsException()
        {
            // Arrange
            var entity = new City
            {
                Id = Guid.Empty,
                Name = null,
                Code = -1
            };

            var mock = new Mock<IRepository<City>>();

            var validator = new CityValidator(mock.Object);
            var service = new DataProvider<City>(mock.Object, validator);

            // Act
            var act = () => service.Create(entity);

            // Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        public void Update_ValidCall_RepositoryMethodInvokes()
        {
            // Arrange
            var t = false;
            var entity = new City
            {
                Id = GetTestCities()[0].Id,
                Name = "New York",
                Code = 21
            };
            var entities = GetTestCities().AsQueryable();
            var expectedEntity = GetTestCities()[0];

            var mock = new Mock<IRepository<City>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns(expectedEntity);
            mock.Setup(r => r.Update(It.IsAny<City>()))
                .Callback(() => t = true);

            var validator = new CityValidator(mock.Object);
            var service = new DataProvider<City>(mock.Object, validator);

            // Act
            service.Update(entity);

            // Assert
            t.Should().BeTrue();
        }

        [Test]
        public void Update_InvalidCall_RepositoryMethodDoesNotInvoke()
        {
            // Arrange
            var t = false;
            var entity = new City
            {
                Id = GetTestCities()[0].Id,
                Name = "Minsk",
                Code = 22
            };
            var entities = GetTestCities().AsQueryable();
            var expectedEntity = GetTestCities()[0];

            var mock = new Mock<IRepository<City>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns(expectedEntity);
            mock.Setup(r => r.Update(It.IsAny<City>()))
                .Callback(() => t = true);

            var validator = new CityValidator(mock.Object);
            var service = new DataProvider<City>(mock.Object, validator);

            // Act
            service.Update(entity);

            // Assert
            t.Should().BeFalse();
        }

        [Test]
        public void Update_InvalidCall_ThrowsException()
        {
            // Arrange
            var entity = new City
            {
                Id = Guid.Empty,
                Name = null,
                Code = -1
            };

            var mock = new Mock<IRepository<City>>();

            var validator = new CityValidator(mock.Object);
            var service = new DataProvider<City>(mock.Object, validator);

            // Act
            var act = () => service.Update(entity);

            // Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        public void Delete_ValidCall_RepositoryMethodInvokes()
        {
            // Arrange
            var t = false;
            var entities = GetTestCities().AsQueryable();

            var mock = new Mock<IRepository<City>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Delete(It.IsAny<Guid>()))
                .Callback(() => t = true);

            var validator = new CityValidator(mock.Object);
            var service = new DataProvider<City>(mock.Object, validator);

            // Act
            service.Delete(GetTestCities()[0].Id);

            // Assert
            t.Should().BeTrue();
        }


        private static IList<City> GetTestCities()
        {
            var output = new List<City>
            {
                new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Moscow",
                    Code = 100
                },
                new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Brest",
                    Code = 200
                },
                new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Minsk",
                    Code = 300
                }
            };

            return output;
        }
    }
}
