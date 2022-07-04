namespace BusinessLogicLayer.UnitTests
{
    public class ProductDataProviderTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAll_ValidCall_ReturnsList()
        {
            // Arrange
            var entities = GetTestProducts();

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities.AsQueryable());

            var validator = new ProductValidator(mock.Object);
            var service = new DataProvider<Product>(mock.Object, validator);

            // Act
            var result = service.GetAll();

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(5).And.BeEquivalentTo(entities);
        }

        [Test]
        [TestCase(1)]
        public void Get_ValidCall_ReturnsEntity(int id)
        {
            // Arrange
            var expectedEntity = GetTestProducts()[id];

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(expectedEntity);

            var validator = new ProductValidator(mock.Object);
            var service = new DataProvider<Product>(mock.Object, validator);

            // Act
            var result = service.Get(id);

            // Assert
            result.Should().NotBeNull().And.BeOfType(typeof(Product)).And.BeEquivalentTo(expectedEntity);
        }

        [Test]
        [TestCase(-1)]
        public void Get_InvalidCall_ReturnsNull(int id)
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns((Product)null);

            var validator = new ProductValidator(mock.Object);
            var service = new DataProvider<Product>(mock.Object, validator);

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
            var entity = new Product
            {
                Id = 6,
                Name = "Fish",
                Quantity = 7,
                TypeId = 1
            };
            var entities = GetTestProducts();

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Create(It.IsAny<Product>()))
                .Callback(() => t = true);

            var validator = new ProductValidator(mock.Object);
            var service = new DataProvider<Product>(mock.Object, validator);

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
            var entity = new Product
            {
                Id = 6,
                Name = "Cheese",
                Quantity = 7,
                TypeId = 1, 
                BrandId = 1
            };
            var entities = GetTestProducts();

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Create(It.IsAny<Product>()))
                .Callback((Product prod) => t = true);

            var validator = new ProductValidator(mock.Object);
            var service = new DataProvider<Product>(mock.Object, validator);

            // Act
            service.Create(entity);

            // Assert
            t.Should().BeFalse();
        }

        [Test]
        public void Create_InvalidCall_ThrowsException()
        {
            // Arrange
            var entity = new Product
            {
                Id = -1,
                Name = null,
                Quantity = -1,
                TypeId = -1
            };

            var mock = new Mock<IRepository<Product>>();

            var validator = new ProductValidator(mock.Object);
            var service = new DataProvider<Product>(mock.Object, validator);

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
            var entity = new Product
            {
                Id = 1,
                Name = "Juice",
                Quantity = 21,
                TypeId = 2, 
                BrandId = 1
            };
            var entities = GetTestProducts();
            var expectedEntity = GetTestProducts()[entity.Id];

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(expectedEntity);
            mock.Setup(r => r.Update(It.IsAny<Product>()))
                .Callback(() => t = true);

            var validator = new ProductValidator(mock.Object);
            var service = new DataProvider<Product>(mock.Object, validator);

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
            var entity = new Product
            {
                Id = 1,
                Name = "Cake",
                Quantity = 21,
                TypeId = 1,
                BrandId = 2
            };
            var entities = GetTestProducts();
            var expectedEntity = GetTestProducts()[entity.Id];

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(expectedEntity);
            mock.Setup(r => r.Update(It.IsAny<Product>()))
                .Callback(() => t = true);

            var validator = new ProductValidator(mock.Object);
            var service = new DataProvider<Product>(mock.Object, validator);

            // Act
            service.Update(entity);

            // Assert
            t.Should().BeFalse();
        }

        [Test]
        public void Update_InvalidCall_ThrowsException()
        {
            // Arrange
            var entity = new Product
            {
                Id = -1,
                Name = null,
                Quantity = -1,
                TypeId = -1,
                BrandId = -1
            };

            var mock = new Mock<IRepository<Product>>();

            var validator = new ProductValidator(mock.Object);
            var service = new DataProvider<Product>(mock.Object, validator);

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
            var entities = GetTestProducts();

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Delete(It.IsAny<int>()))
                .Callback(() => t = true);

            var validator = new ProductValidator(mock.Object);
            var service = new DataProvider<Product>(mock.Object, validator);

            // Act
            service.Delete(id);

            // Assert
            t.Should().BeTrue();
        }


        private static List<Product> GetTestProducts()
        {
            var output = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Salad",
                    Quantity = 25,
                    TypeId = 1,
                    BrandId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Cheese",
                    Quantity = 19,
                    TypeId = 1,
                    BrandId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Coffee",
                    Quantity = 36,
                    TypeId = 2,
                    BrandId = 1
                },
                new Product
                {
                    Id = 4,
                    Name = "Tea",
                    Quantity = 14,
                    TypeId = 2,
                    BrandId = 1
                },
                new Product
                {
                    Id = 5,
                    Name = "Cake",
                    Quantity = 1,
                    TypeId = 1,
                    BrandId = 1
                },
            };

            return output;
        }
    }
}