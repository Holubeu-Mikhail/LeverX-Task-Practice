namespace BusinessLogicLayer.UnitTests
{
    public class TownDataProviderTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAll_ValidCall_ReturnsList()
        {
            // Arrange
            var entities = GetTestTowns();

            var mock = new Mock<IRepository<Town>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);

            var validator = new TownValidator(mock.Object);
            var service = new DataProvider<Town>(mock.Object, validator);

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
            var expectedEntity = GetTestTowns()[id];

            var mock = new Mock<IRepository<Town>>();
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(expectedEntity);

            var validator = new TownValidator(mock.Object);
            var service = new DataProvider<Town>(mock.Object, validator);

            // Act
            var result = service.Get(id);

            // Assert
            result.Should().NotBeNull().And.BeOfType(typeof(Town)).And.BeEquivalentTo(expectedEntity);
        }

        [Test]
        [TestCase(-1)]
        public void Get_InvalidCall_ReturnsNull(int id)
        {
            // Arrange
            var mock = new Mock<IRepository<Town>>();
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns((Town)null);

            var validator = new TownValidator(mock.Object);
            var service = new DataProvider<Town>(mock.Object, validator);

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
            var entity = new Town
            {
                Id = 6,
                Name = "London",
                Code = 100
            };
            var entities = GetTestTowns();

            var mock = new Mock<IRepository<Town>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Create(It.IsAny<Town>()))
                .Callback(() => t = true);

            var validator = new TownValidator(mock.Object);
            var service = new DataProvider<Town>(mock.Object, validator);

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
            var entity = new Town
            {
                Id = 6,
                Name = "Minsk",
                Code = 200
            };
            var entities = GetTestTowns();

            var mock = new Mock<IRepository<Town>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Create(It.IsAny<Town>()))
                .Callback(() => t = true);

            var validator = new TownValidator(mock.Object);
            var service = new DataProvider<Town>(mock.Object, validator);

            // Act
            service.Create(entity);

            // Assert
            t.Should().BeFalse();
        }

        [Test]
        public void Create_InvalidCall_ThrowsException()
        {
            // Arrange
            var entity = new Town
            {
                Id = -1,
                Name = null,
                Code = -1
            };

            var mock = new Mock<IRepository<Town>>();

            var validator = new TownValidator(mock.Object);
            var service = new DataProvider<Town>(mock.Object, validator);

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
            var entity = new Town
            {
                Id = 1,
                Name = "New York",
                Code = 21
            };
            var entities = GetTestTowns();
            var expectedEntity = GetTestTowns()[entity.Id];

            var mock = new Mock<IRepository<Town>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(expectedEntity);
            mock.Setup(r => r.Update(It.IsAny<Town>()))
                .Callback(() => t = true);

            var validator = new TownValidator(mock.Object);
            var service = new DataProvider<Town>(mock.Object, validator);

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
            var entity = new Town
            {
                Id = 1,
                Name = "Minsk",
                Code = 22
            };
            var entities = GetTestTowns();
            var expectedEntity = GetTestTowns()[entity.Id];

            var mock = new Mock<IRepository<Town>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(expectedEntity);
            mock.Setup(r => r.Update(It.IsAny<Town>()))
                .Callback(() => t = true);

            var validator = new TownValidator(mock.Object);
            var service = new DataProvider<Town>(mock.Object, validator);

            // Act
            service.Update(entity);

            // Assert
            t.Should().BeFalse();
        }

        [Test]
        public void Update_InvalidCall_ThrowsException()
        {
            // Arrange
            var entity = new Town
            {
                Id = -1,
                Name = null,
                Code = -1
            };

            var mock = new Mock<IRepository<Town>>();

            var validator = new TownValidator(mock.Object);
            var service = new DataProvider<Town>(mock.Object, validator);

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
            var entities = GetTestTowns();

            var mock = new Mock<IRepository<Town>>();
            mock.Setup(r => r.GetAll())
                .Returns(entities);
            mock.Setup(r => r.Delete(It.IsAny<int>()))
                .Callback(() => t = true);

            var validator = new TownValidator(mock.Object);
            var service = new DataProvider<Town>(mock.Object, validator);

            // Act
            service.Delete(id);

            // Assert
            t.Should().BeTrue();
        }


        private static List<Town> GetTestTowns()
        {
            var output = new List<Town>
            {
                new Town
                {
                    Id = 1,
                    Name = "Moscow",
                    Code = 100
                },
                new Town
                {
                    Id = 2,
                    Name = "Brest",
                    Code = 200
                },
                new Town
                {
                    Id = 3,
                    Name = "Minsk",
                    Code = 300
                }
            };

            return output;
        }
    }
}
