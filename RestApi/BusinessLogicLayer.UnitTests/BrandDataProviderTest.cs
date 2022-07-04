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
            var entities = GetTestBrands();

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
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(expectedEntity);

            var validator = new BrandValidator(mock.Object);
            var service = new DataProvider<Brand>(mock.Object, validator);

            // Act
            var result = service.Get(id);

            // Assert
            result.Should().NotBeNull().And.BeOfType(typeof(Brand)).And.BeEquivalentTo(expectedEntity);
        }

        [Test]
        [TestCase(-1)]
        public void Get_InvalidCall_ReturnsNull(int id)
        {
            // Arrange
            var mock = new Mock<IRepository<Brand>>();
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns((Brand)null);

            var validator = new BrandValidator(mock.Object);
            var service = new DataProvider<Brand>(mock.Object, validator);

            // Act
            var result = service.Get(id);

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
                Id = 6,
                Name = "Xiaomi",
                Description = "Chinese electronics company",
                TownId = 10
            };
            var entities = GetTestBrands();

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
            var entity = new Brand
            {
                Id = 6,
                Name = "Apple",
                Description = "Chinese electronics company",
                TownId = 1
            };
            var entities = GetTestBrands();

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
                Id = -1,
                Name = null,
                Description = null,
                TownId = -1
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
                Id = 1,
                Name = "ZTE",
                Description = "Asian electronics company",
                TownId = 20
            };
            var entities = GetTestBrands();
            var expectedEntity = GetTestBrands()[entity.Id];

            var mock = new Mock<IRepository<Brand>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Get(It.IsAny<int>()))
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
            var entity = new Brand
            {
                Id = 1,
                Name = "Apple",
                Description = "American electronics company",
                TownId = 1
            };
            var entities = GetTestBrands();
            var expectedEntity = GetTestBrands()[entity.Id];

            var mock = new Mock<IRepository<Brand>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Get(It.IsAny<int>()))
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
                Id = -1,
                Name = null,
                Description = null,
                TownId = -1
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
        [TestCase(1)]
        public void Delete_ValidCall_RepositoryMethodInvokes(int id)
        {
            // Arrange
            var t = false;
            var entities = GetTestBrands();

            var mock = new Mock<IRepository<Brand>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Delete(It.IsAny<int>()))
                .Callback(() => t = true);

            var validator = new BrandValidator(mock.Object);
            var service = new DataProvider<Brand>(mock.Object, validator);

            // Act
            service.Delete(id);

            // Assert
            t.Should().BeTrue();
        }


        private static List<Brand> GetTestBrands()
        {
            var output = new List<Brand>
            {
                new Brand
                {
                    Id = 1,
                    Name = "Apple",
                    Description = "Electronics manufacture",
                    TownId = 1
                },
                new Brand
                {
                    Id = 2,
                    Name = "Milkavita",
                    Description = "Milk products",
                    TownId = 2
                },
                new Brand
                {
                    Id = 3,
                    Name = "Samsung",
                    Description = "Electronics manufacture",
                    TownId = 3
                }
            };

            return output;
        }
    }
}
