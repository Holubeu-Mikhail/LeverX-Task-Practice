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
            var entities = GetTestProducts().AsQueryable();

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);

            var validator = new ProductValidator(mock.Object);
            var service = new DataProvider<Product>(mock.Object, validator);

            // Act
            var result = service.GetAll();

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(5).And.BeEquivalentTo(entities);
        }

        [Test]
        public void Get_ValidCall_ReturnsEntity()
        {
            // Arrange
            var expectedEntity = GetTestProducts()[0];

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns(expectedEntity);

            var validator = new ProductValidator(mock.Object);
            var service = new DataProvider<Product>(mock.Object, validator);

            // Act
            var result = service.Get(GetTestProducts()[0].Id);

            // Assert
            result.Should().NotBeNull().And.BeOfType(typeof(Product)).And.BeEquivalentTo(expectedEntity);
        }

        [Test]
        public void Get_InvalidCall_ReturnsNull()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns((Product)null);

            var validator = new ProductValidator(mock.Object);
            var service = new DataProvider<Product>(mock.Object, validator);

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
            var entity = new Product
            {
                Name = "Fish",
                Quantity = 7,
                TypeId = Guid.NewGuid(),
                BrandId = Guid.NewGuid()
            };
            var entities = GetTestProducts().AsQueryable();

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
            var entities = GetTestProducts().AsQueryable();
            var entity = new Product
            {
                Name = "Cheese",
                Quantity = 7,
                TypeId = entities.ToList()[1].TypeId, 
                BrandId = Guid.NewGuid()
            };

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
                Name = null,
                Quantity = -10,
                TypeId = Guid.Empty
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
                Id = GetTestProducts()[0].Id,
                Name = "Juice",
                Quantity = 21,
                TypeId = Guid.NewGuid(), 
                BrandId = Guid.NewGuid()
            };
            var entities = GetTestProducts().AsQueryable();
            var expectedEntity = GetTestProducts()[0];

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
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
            var entities = GetTestProducts().AsQueryable();
            var entity = new Product
            {
                Id = GetTestProducts()[0].Id,
                Name = "Cake",
                Quantity = 21,
                TypeId = entities.ToList()[4].TypeId,
                BrandId = Guid.NewGuid()
            };
            var expectedEntity = GetTestProducts()[0];

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Get(It.IsAny<Guid>()))
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
                Id = Guid.Empty,
                Name = null,
                Quantity = -1,
                TypeId = Guid.Empty,
                BrandId = Guid.Empty
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
        public void Delete_ValidCall_RepositoryMethodInvokes()
        {
            // Arrange
            var t = false;
            var entities = GetTestProducts().AsQueryable();

            var mock = new Mock<IRepository<Product>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Delete(It.IsAny<Guid>()))
                .Callback(() => t = true);

            var validator = new ProductValidator(mock.Object);
            var service = new DataProvider<Product>(mock.Object, validator);

            // Act
            service.Delete(GetTestProducts()[0].Id);

            // Assert
            t.Should().BeTrue();
        }


        private static List<Product> GetTestProducts()
        {
            var output = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Salad",
                    Quantity = 25,
                    TypeId = Guid.NewGuid(),
                    BrandId = Guid.NewGuid()
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Cheese",
                    Quantity = 19,
                    TypeId = Guid.NewGuid(),
                    BrandId = Guid.NewGuid()
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Coffee",
                    Quantity = 36,
                    TypeId = Guid.NewGuid(),
                    BrandId = Guid.NewGuid()
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Tea",
                    Quantity = 14,
                    TypeId = Guid.NewGuid(),
                    BrandId = Guid.NewGuid()
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Cake",
                    Quantity = 1,
                    TypeId = Guid.NewGuid(),
                    BrandId = Guid.NewGuid()
                },
            };

            return output;
        }
    }
}