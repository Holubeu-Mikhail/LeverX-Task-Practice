namespace BusinessLogicLayer.UnitTests
{
    public class ProductTypeServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAll_ValidCall_ReturnsList()
        {
            // Arrange
            var productTypes = GetTestProductTypes();

            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.GetAll())
                .Returns(productTypes);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

            // Act
            var result = service.GetAll();

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(3).And.BeEquivalentTo(productTypes);
        }

        [Test]
        [TestCase(1)]
        public void Get_ValidCall_ReturnsEntity(int id)
        {
            // Arrange
            var expectedProductType = GetTestProductTypes()[id];

            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(expectedProductType);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

            // Act
            var result = service.Get(id);

            // Assert
            result.Should().NotBeNull().And.BeOfType(typeof(ProductType)).And.BeEquivalentTo(expectedProductType);
        }

        [Test]
        [TestCase(-1)]
        public void Get_InvalidCall_ReturnsNull(int id)
        {
            // Arrange
            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns((ProductType)null);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

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
            var productType = new ProductType
            {
                Id = 4,
                Name = "Automobile"
            };
            var productTypes = GetTestProductTypes();

            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.GetAll())
                .Returns(productTypes);
            mock.Setup(r => r.Create(It.IsAny<ProductType>()))
                .Callback((ProductType type) => t = true);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

            // Act
            service.Create(productType);

            // Assert
            t.Should().BeTrue();
        }

        [Test]
        public void Create_InvalidCall_RepositoryMethodDoesNotInvoke()
        {
            // Arrange
            var t = false;
            var productType = new ProductType
            {
                Id = 6,
                Name = "Food"
            };
            var productTypes = GetTestProductTypes();

            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.GetAll())
                .Returns(productTypes);
            mock.Setup(r => r.Create(It.IsAny<ProductType>()))
                .Callback((ProductType type) => t = true);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

            // Act
            service.Create(productType);

            // Assert
            t.Should().BeFalse();
        }

        [Test]
        public void Create_InvalidCall_ThrowsException()
        {
            // Arrange
            var productType = new ProductType
            {
                Id = -1,
                Name = null
            };

            var mock = new Mock<IRepository<ProductType>>();

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

            // Act
            var act = () => service.Create(productType);

            // Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        public void Update_ValidCall_RepositoryMethodInvokes()
        {
            // Arrange
            var t = false;
            var productType = new ProductType
            {
                Id = 1,
                Name = "Automobile"
            };
            var productTypes = GetTestProductTypes();
            var expectedProductType = GetTestProductTypes()[productType.Id];

            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.GetAll())
                .Returns(productTypes);
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(expectedProductType);
            mock.Setup(r => r.Update(It.IsAny<ProductType>()))
                .Callback((ProductType type) => t = true);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

            // Act
            service.Update(productType);

            // Assert
            t.Should().BeTrue();
        }

        [Test]
        public void Update_InvalidCall_RepositoryMethodDoesNotInvoke()
        {
            // Arrange
            var t = false;
            var productType = new ProductType
            {
                Id = 1,
                Name = "Tech"
            };
            var productTypes = GetTestProductTypes();
            var expectedProductType = GetTestProductTypes()[productType.Id];

            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.GetAll())
                .Returns(productTypes);
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(expectedProductType);
            mock.Setup(r => r.Update(It.IsAny<ProductType>()))
                .Callback((ProductType type) => t = true);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

            // Act
            service.Update(productType);

            // Assert
            t.Should().BeFalse();
        }

        [Test]
        public void Update_InvalidCall_ThrowsException()
        {
            // Arrange
            var productType = new ProductType
            {
                Id = -1,
                Name = null
            };

            var mock = new Mock<IRepository<ProductType>>();

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

            // Act
            var act = () => service.Update(productType);

            // Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        [TestCase(1)]
        public void Delete_ValidCall_RepositoryMethodInvokes(int id)
        {
            // Arrange
            var t = false;
            var productTypes = GetTestProductTypes();

            var mock = new Mock<IRepository<ProductType>>();
            mock.Setup(r => r.GetAll())
                .Returns(productTypes);
            mock.Setup(r => r.Delete(It.IsAny<int>()))
                .Callback((int i) => t = true);

            var validator = new ProductTypeValidator(mock.Object);
            var service = new DataProvider<ProductType>(mock.Object, validator);

            // Act
            service.Delete(id);

            // Assert
            t.Should().BeTrue();
        }


        private List<ProductType> GetTestProductTypes()
        {
            var output = new List<ProductType>
            {
                new ProductType
                {
                    Id = 1,
                    Name = "Food"
                },
                new ProductType
                {
                    Id = 2,
                    Name = "Drinks"
                },
                new ProductType
                {
                    Id = 3,
                    Name = "Tech"
                }
            };

            return output;
        }
    }
}