namespace BusinessLogicLayer.UnitTests
{
    public class BrandDataProviderTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAll_ValidCall_ReturnsList()
        {
            // Arrange
            var entities = GetTestBrands().AsQueryable();

            var mock = new Mock<IRepository<Brand>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);

            var validator = new BrandValidator(mock.Object);
            var service = new DataProvider<Brand>(mock.Object, validator);

            // Act
            var result = service.GetAll();

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(3).And.BeEquivalentTo(entities);
        }

        [Test]
        [TestCase(1)]
        public void Get_ValidCall_ReturnsEntity(int id)
        {
            // Arrange
            var expectedEntity = GetTestBrands()[id];

            var mock = new Mock<IRepository<Brand>>();
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns(expectedEntity);

            var validator = new BrandValidator(mock.Object);
            var service = new DataProvider<Brand>(mock.Object, validator);

            // Act
            var result = service.Get(GetTestBrands()[id].Id);

            // Assert
            result.Should().NotBeNull().And.BeOfType(typeof(Brand)).And.BeEquivalentTo(expectedEntity);
        }

        [Test]
        public void Get_InvalidCall_ReturnsNull()
        {
            // Arrange
            var mock = new Mock<IRepository<Brand>>();
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns((Brand)null);

            var validator = new BrandValidator(mock.Object);
            var service = new DataProvider<Brand>(mock.Object, validator);

            // Act
            var result = service.Get(Guid.Empty);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public void Create_ValidCall_RepositoryMethodInvokes()
        {
            // Arrange
            var t = false;
            var entity = new Brand
            {
                Name = "Xiaomi",
                Description = "Chinese electronics company",
                CityId = Guid.NewGuid()
            };
            var entities = GetTestBrands().AsQueryable();

            var mock = new Mock<IRepository<Brand>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Create(It.IsAny<Brand>()))
                .Callback(() => t = true);

            var validator = new BrandValidator(mock.Object);
            var service = new DataProvider<Brand>(mock.Object, validator);

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
            var entities = GetTestBrands().AsQueryable();
            var entity = new Brand
            {
                Name = "Apple",
                Description = "Chinese electronics company",
                CityId = entities.ToList()[0].CityId
            };

            var mock = new Mock<IRepository<Brand>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Create(It.IsAny<Brand>()))
                .Callback(() => t = true);

            var validator = new BrandValidator(mock.Object);
            var service = new DataProvider<Brand>(mock.Object, validator);

            // Act
            service.Create(entity);

            // Assert
            t.Should().BeFalse();
        }

        [Test]
        public void Create_InvalidCall_ThrowsException()
        {
            // Arrange
            var entity = new Brand
            {
                Name = null,
                Description = null,
                CityId = Guid.Empty
            };

            var mock = new Mock<IRepository<Brand>>();

            var validator = new BrandValidator(mock.Object);
            var service = new DataProvider<Brand>(mock.Object, validator);

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
            var entity = new Brand
            {
                Id = GetTestBrands()[0].Id,
                Name = "ZTE",
                Description = "Asian electronics company",
                CityId = Guid.NewGuid()
            };
            var entities = GetTestBrands().AsQueryable();
            var expectedEntity = GetTestBrands()[0];

            var mock = new Mock<IRepository<Brand>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns(expectedEntity);
            mock.Setup(r => r.Update(It.IsAny<Brand>()))
                .Callback(() => t = true);

            var validator = new BrandValidator(mock.Object);
            var service = new DataProvider<Brand>(mock.Object, validator);

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
            var entities = GetTestBrands().AsQueryable();
            var entity = new Brand
            {
                Id = GetTestBrands()[0].Id,
                Name = "Apple",
                Description = "American electronics company",
                CityId = entities.ToList()[0].CityId
            };
            var expectedEntity = GetTestBrands()[0];

            var mock = new Mock<IRepository<Brand>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns(expectedEntity);
            mock.Setup(r => r.Update(It.IsAny<Brand>()))
                .Callback(() => t = true);

            var validator = new BrandValidator(mock.Object);
            var service = new DataProvider<Brand>(mock.Object, validator);

            // Act
            service.Update(entity);

            // Assert
            t.Should().BeFalse();
        }

        [Test]
        public void Update_InvalidCall_ThrowsException()
        {
            // Arrange
            var entity = new Brand
            {
                Id = Guid.Empty,
                Name = null,
                Description = null,
                CityId = Guid.Empty
            };

            var mock = new Mock<IRepository<Brand>>();

            var validator = new BrandValidator(mock.Object);
            var service = new DataProvider<Brand>(mock.Object, validator);

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
            var entities = GetTestBrands().AsQueryable();

            var mock = new Mock<IRepository<Brand>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Delete(It.IsAny<Guid>()))
                .Callback(() => t = true);

            var validator = new BrandValidator(mock.Object);
            var service = new DataProvider<Brand>(mock.Object, validator);

            // Act
            service.Delete(GetTestBrands()[0].Id);

            // Assert
            t.Should().BeTrue();
        }

        private static List<Brand> GetTestBrands()
        {
            var output = new List<Brand>
            {
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "Apple",
                    Description = "Electronics manufacture",
                    CityId = Guid.NewGuid()
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "Milkavita",
                    Description = "Milk products",
                    CityId = Guid.NewGuid()
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "Samsung",
                    Description = "Electronics manufacture",
                    CityId = Guid.NewGuid()
                }
            };

            return output;
        }
    }
}
