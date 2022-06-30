namespace BusinessLogicLayer.UnitTests
{
    public class ProductServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAll_ValidCall_ReturnsList()
        {
            // Arrange
            var products = GetTestProducts();

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(products.AsQueryable());

            var validator = new ProductValidator(mock.Object);
            var service = new Service<Product>(mock.Object, validator);

            // Act
            var result = service.GetAll();

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(5).And.BeEquivalentTo(products);
        }

        [Test]
        [TestCase(1)]
        public void Get_ValidCall_ReturnsEntity(int id)
        {
            // Arrange
            var expectedProduct = GetTestProducts()[id];

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(expectedProduct);

            var validator = new ProductValidator(mock.Object);
            var service = new Service<Product>(mock.Object, validator);

            // Act
            var result = service.Get(id);

            // Assert
            result.Should().NotBeNull().And.BeOfType(typeof(Product)).And.BeEquivalentTo(expectedProduct);
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
            var service = new Service<Product>(mock.Object, validator);

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
            var product = new Product
            {
                Id = 6,
                Name = "Fish",
                Quantity = 7,
                TypeId = 1
            };
            var products = GetTestProducts();

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(products);
            mock.Setup(r => r.Create(It.IsAny<Product>()))
                .Callback((Product prod) => t = true);

            var validator = new ProductValidator(mock.Object);
            var service = new Service<Product>(mock.Object, validator);

            // Act
            service.Create(product);

            // Assert
            t.Should().BeTrue();
        }

        [Test]
        public void Create_InvalidCall_RepositoryMethodDoesNotInvoke()
        {
            // Arrange
            var t = false;
            var product = new Product
            {
                Id = 6,
                Name = "Cheese",
                Quantity = 7,
                TypeId = 1
            };
            var products = GetTestProducts();

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(products);
            mock.Setup(r => r.Create(It.IsAny<Product>()))
                .Callback((Product prod) => t = true);

            var validator = new ProductValidator(mock.Object);
            var service = new Service<Product>(mock.Object, validator);

            // Act
            service.Create(product);

            // Assert
            t.Should().BeFalse();
        }

        [Test]
        public void Create_InvalidCall_ThrowsException()
        {
            // Arrange
            var product = new Product
            {
                Id = -1,
                Name = null,
                Quantity = -1,
                TypeId = -1
            };

            var mock = new Mock<IRepository<Product>>();

            var validator = new ProductValidator(mock.Object);
            var service = new Service<Product>(mock.Object, validator);

            // Act
            var act = () => service.Create(product);

            // Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        public void Update_ValidCall_RepositoryMethodInvokes()
        {
            // Arrange
            var t = false;
            var product = new Product
            {
                Id = 1,
                Name = "Juice",
                Quantity = 21,
                TypeId = 2
            };
            var products = GetTestProducts();
            var expectedProduct = GetTestProducts()[product.Id];

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(products);
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(expectedProduct);
            mock.Setup(r => r.Update(It.IsAny<Product>()))
                .Callback((Product prod) => t = true);

            var validator = new ProductValidator(mock.Object);
            var service = new Service<Product>(mock.Object, validator);

            // Act
            service.Update(product);

            // Assert
            t.Should().BeTrue();
        }

        [Test]
        public void Update_InvalidCall_RepositoryMethodDoesNotInvoke()
        {
            // Arrange
            var t = false;
            var product = new Product
            {
                Id = 1,
                Name = "Cake",
                Quantity = 21,
                TypeId = 1
            };
            var products = GetTestProducts();
            var expectedProduct = GetTestProducts()[product.Id];

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(products);
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(expectedProduct);
            mock.Setup(r => r.Update(It.IsAny<Product>()))
                .Callback((Product prod) => t = true);

            var validator = new ProductValidator(mock.Object);
            var service = new Service<Product>(mock.Object, validator);

            // Act
            service.Update(product);

            // Assert
            t.Should().BeFalse();
        }

        [Test]
        public void Update_InvalidCall_ThrowsException()
        {
            // Arrange
            var product = new Product
            {
                Id = -1,
                Name = null,
                Quantity = -1,
                TypeId = -1
            };

            var mock = new Mock<IRepository<Product>>();

            var validator = new ProductValidator(mock.Object);
            var service = new Service<Product>(mock.Object, validator);

            // Act
            var act = () => service.Update(product);

            // Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        [TestCase(1)]
        public void Delete_ValidCall_RepositoryMethodInvokes(int id)
        {
            // Arrange
            var t = false;
            var products = GetTestProducts();

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(products);
            mock.Setup(r => r.Delete(It.IsAny<int>()))
                .Callback((int i) => t = true);

            var validator = new ProductValidator(mock.Object);
            var service = new Service<Product>(mock.Object, validator);

            // Act
            service.Delete(id);

            // Assert
            t.Should().BeTrue();
        }


        private List<Product> GetTestProducts()
        {
            var output = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Salad",
                    Quantity = 25,
                    TypeId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Cheese",
                    Quantity = 19,
                    TypeId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Coffee",
                    Quantity = 36,
                    TypeId = 2
                },
                new Product
                {
                    Id = 4,
                    Name = "Tea",
                    Quantity = 14,
                    TypeId = 2
                },
                new Product
                {
                    Id = 5,
                    Name = "Cake",
                    Quantity = 1,
                    TypeId = 1
                },
            };

            return output;
        }
    }
}