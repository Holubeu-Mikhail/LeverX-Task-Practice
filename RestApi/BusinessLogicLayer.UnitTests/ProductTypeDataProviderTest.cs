namespace BusinessLogicLayer.UnitTests
{
    public class ProductTypeDataProviderTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAll_ValidCall_ReturnsList()
        {
            // Arrange
            var entities = GetTestProductTypes().AsQueryable();

            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

            // Act
            var result = service.GetAll();

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(3).And.BeEquivalentTo(entities);
        }

        [Test]
        public void Get_ValidCall_ReturnsEntity()
        {
            // Arrange
            var expectedEntity = GetTestProductTypes()[0];

            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns(expectedEntity);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

            // Act
            var result = service.Get(GetTestProductTypes()[0].Id);

            // Assert
            result.Should().NotBeNull().And.BeOfType(typeof(ProductType)).And.BeEquivalentTo(expectedEntity);
        }

        [Test]
        public void Get_InvalidCall_ReturnsNull()
        {
            // Arrange
            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns((ProductType)null);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

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
            var entity = new ProductType
            {
                Name = "Automobile"
            };
            var entities = GetTestProductTypes().AsQueryable();

            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Create(It.IsAny<ProductType>()))
                .Callback(() => t = true);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

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
            var entity = new ProductType
            {
                Name = "Food"
            };
            var entities = GetTestProductTypes().AsQueryable();

            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Create(It.IsAny<ProductType>()))
                .Callback((ProductType type) => t = true);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

            // Act
            service.Create(entity);

            // Assert
            t.Should().BeFalse();
        }

        [Test]
        public void Create_InvalidCall_ThrowsException()
        {
            // Arrange
            var entity = new ProductType
            {
                Name = null
            };

            var mock = new Mock<IRepository<ProductType>>();

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

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
            var entity = new ProductType
            {
                Id = GetTestProductTypes()[0].Id,
                Name = "Automobile"
            };
            var entities = GetTestProductTypes().AsQueryable();
            var expectedEntity = GetTestProductTypes()[0];

            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns(expectedEntity);
            mock.Setup(r => r.Update(It.IsAny<ProductType>()))
                .Callback(() => t = true);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

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
            var entity = new ProductType
            {
                Id = GetTestProductTypes()[0].Id,
                Name = "Tech"
            };
            var entities = GetTestProductTypes().AsQueryable();
            var expectedEntity = GetTestProductTypes()[0];

            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns(expectedEntity);
            mock.Setup(r => r.Update(It.IsAny<ProductType>()))
                .Callback(() => t = true);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

            // Act
            service.Update(entity);

            // Assert
            t.Should().BeFalse();
        }

        [Test]
        public void Update_InvalidCall_ThrowsException()
        {
            // Arrange
            var entity = new ProductType
            {
                Id = Guid.Empty,
                Name = null
            };

            var mock = new Mock<IRepository<ProductType>>();

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

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
            var entities = GetTestProductTypes().AsQueryable();

            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Delete(It.IsAny<Guid>()))
                .Callback(() => t = true);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

            // Act
            service.Delete(GetTestProductTypes()[0].Id);

            // Assert
            t.Should().BeTrue();
        }


        private static List<ProductType> GetTestProductTypes()
        {
            var output = new List<ProductType>
            {
                new ProductType
                {
                    Id = Guid.NewGuid(),
                    Name = "Food"
                },
                new ProductType
                {
                    Id = Guid.NewGuid(),
                    Name = "Drinks"
                },
                new ProductType
                {
                    Id = Guid.NewGuid(),
                    Name = "Tech"
                }
            };

            return output;
        }
    }
}