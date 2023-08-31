//using SqlKata.Compilers;
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Xml.Linq;
//using TestContainers.Container.Database.MsSql;
//using Xunit;

//namespace YourNamespace.Tests
//{
//    public class YourTestClassTests : IClassFixture<MsSqlContainerFixture>
//    {
//        private readonly MsSqlContainerFixture _fixture;

//        public YourTestClassTests(MsSqlContainerFixture fixture)
//        {
//            _fixture = fixture;
//        }

//        [Fact]
//        public void Sqlkata_ReturnsCorrectXml()
//        {
//            // Arrange
//            var compiler = new SqlServerCompiler();

//            using (var connection = new SqlConnection(_fixture.ConnectionString))
//            {
//                connection.Open();

//                var yourTestClassInstance = new YourTestClass(connection, compiler);

//                // Act
//                var result = yourTestClassInstance.Sqlkata();

//                // Assert
//                Assert.NotNull(result);

//                var playElements = result.Elements("Play");
//                Assert.Equal(2, playElements.Count());

//                var firstPlay = playElements.ElementAt(0);
//                Assert.Equal("Play 1", firstPlay.Element("Title").Value);
//                Assert.Equal("7200", firstPlay.Element("Duration").Value); // Total seconds
//                Assert.Equal("Description 1", firstPlay.Element("Description").Value);
//                Assert.Equal("Writer 1", firstPlay.Element("Screenwriter").Value);
//            }
//        }
//    }

//    public class MsSqlContainerFixture : IDisposable
//    {
//        public MsSqlContainerFixture()
//        {
//            Container = new ContainerBuilder<MsSqlContainer>()
//                .ConfigureDatabaseConfiguration("sa", "YourStrong!Passw0rd")
//                .Build();
//            Container.Start();
//        }

//        public IDbConnection CreateConnection()
//        {
//            var connectionString = Container.GetConnectionString();
//            return new SqlConnection(connectionString);
//        }

//        public void Dispose()
//        {
//            Container.Stop();
//            Container.Dispose();
//        }

//        private IDatabaseContainer Container { get; }
//    }
//}
