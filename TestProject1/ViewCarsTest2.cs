
using System;
using System.Data;
using System.Data.Common;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using testAPI.Services;  // Replace with the actual namespace of your code

public class CarServiceTests2 : IDisposable
{
    private readonly IDbConnection _connection;

    public CarServiceTests2()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using (var cmd = _connection.CreateCommand())
        {
            cmd.CommandText = "CREATE TABLE Cars (Id INT PRIMARY KEY, Brand TEXT, CarModel TEXT, Mileage INT);";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Cars (Id, Brand, CarModel, Mileage) VALUES (1, 'Toyota', 'Camry', 50000);";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Cars (Id, Brand, CarModel, Mileage) VALUES (2, 'Honda', 'Civic', 60000);";
            cmd.ExecuteNonQuery();
        }
    }

    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }

    [Fact]
    public void ViewCars_ReturnsCorrectXml2()
    {
        // Arrange
        var carService = new testService(new NullLogger<testService>());

        // Act
        var result = carService.ViewCars(_connection);

        // Assert
        var expectedXml = new XElement("Cars",
            new XElement("Car",
                new XAttribute("Id", 1),
                new XElement("Brand", "Toyota"),
                new XElement("Model", "Camry"),
                new XElement("Mileage", 50000)),
            new XElement("Car",
                new XAttribute("Id", 2),
                new XElement("Brand", "Honda"),
                new XElement("Model", "Civic"),
                new XElement("Mileage", 60000)));

        Assert.Equal(expectedXml.ToString(), result.ToString());
    }
}
