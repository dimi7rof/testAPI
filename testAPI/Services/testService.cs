using Dapper;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml.Linq;
using testAPI.Contracts;
using testAPI.Models;

namespace testAPI.Services;

public class testService : Itest
{
    private readonly ILogger<testService> _logger;

    public testService(ILogger<testService> logger)
    {
        _logger = logger;
    }

    public XDocument Get(string fileName)
    {
        _logger.LogInformation("Search for {fileName} file", fileName);
        if (fileName == null || fileName == string.Empty)
        {
            _logger.LogInformation("{fileName} file not found", fileName);
        }
        return XDocument.Load($"E:\\xml\\{fileName}");
    }


    public XDocument Price(string fileName)
    {
        _logger.Log(LogLevel.Information, $"{DateTime.Now} - search for {fileName} file");
        if (fileName == null || fileName == string.Empty)
        {
            _logger.LogInformation("{fileName} file not found", fileName);
        }

        var document = XDocument.Load($"E:\\xml\\{fileName}");
        _logger.LogInformation("{fileName} file found", fileName);

        var element = new XElement("CD");
        var resultDoc = new XDocument(element);
        var prices = new List<double>();

        foreach (var cd in document.Root.Elements())
        {
            string price = cd.Element("PRICE").Value;
            prices.Add(double.Parse(price, CultureInfo.InvariantCulture));
            _logger.LogInformation("Price of {price} added", price);
        }

        foreach (var price in prices.OrderBy(x => x))
        {
            element.Add(new XElement("PRICE", price));
        }
        return resultDoc;
    }


    public XDocument SqlToXml(string tableName, string _connectionString)
    {
        var plays = new List<Play>();
        var compiler = new SqlServerCompiler();
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var query = new Query(tableName);
            var result = compiler.Compile(query);
            plays = connection.Query<Play>(result.Sql).ToList();
        }

        var root = new XElement(tableName);
        foreach (var play in plays)
        {
            var element = new XElement(("Play"),
                new XElement("Title", play.Title),
                new XElement("Duration", play.Duration.TotalSeconds),
                new XElement("Description", play.Description),
                new XElement("Screenwriter", play.Screenwriter));
            root.Add(element);
            _logger.LogInformation("Play {Title} added to document", play.Title);
        }
        return new XDocument(root);
    }


    public XElement Dapper(string _connectionString)
    {
        var tableName = "Plays";
        var plays = new List<Play>();
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            //string sql = $"SELECT * FROM {tableName} WHERE Screenwriter = 'Merna Menham'";
            string sql = $"SELECT * FROM {tableName} WHERE Rating > 5";
            plays = connection.Query<Play>(sql).ToList();
        }

        var root = new XElement(tableName);
        foreach (var play in plays)
        {
            var element = new XElement(("Play"),
                new XElement("Title", play.Title),
                new XElement("Duration", play.Duration.TotalSeconds),
                new XElement("Description", play.Description),
                new XElement("Rating", play.Rating),
                new XElement("Screenwriter", play.Screenwriter));
            root.Add(element);
            _logger.LogInformation("Play {Title} added to document", play.Title);
        }
        return root;
    }


    public XElement Sqlkata(string _connectionString)
    {
        var tableName = "Play";
        var compiler = new SqlServerCompiler();
        var root = new XElement(tableName);

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            _logger.LogInformation("Connection open!");
            var db = new QueryFactory(connection, compiler);
            var plays = db.Query("Plays").Where("Rating", ">", 6).Get();

            foreach (var play in plays)
            {
                var element = new XElement(("Play"),
                    new XElement("Title", play.Title),
                    new XElement("Duration", play.Duration.TotalSeconds),
                    new XElement("Description", play.Description),
                    new XElement("Screenwriter", play.Screenwriter));
                root.Add(element);
            }
        }
        _logger.LogInformation("Element returned!");

        return root;
    }


    public XElement ViewCars(IDbConnection connection)
    {
        var tableName = "Cars";
        var compiler = new SqlServerCompiler();
        var root = new XElement(tableName);

        using (connection)
        {
            connection.Open();
            _logger.LogInformation("Connection open!");
            //var query = new Query(tableName).Where("Rating", ">", 1);
            var db = new QueryFactory(connection, compiler);
            var cars = db.Query(tableName).Get();

            foreach (var car in cars)
            {
                var element = new XElement(("Car"),
                    new XAttribute("Id", car.Id),
                    new XElement("Brand", car.Brand),
                    new XElement("Model", car.CarModel),
                    new XElement("Mileage", car.Mileage));
                root.Add(element);
            }
            //_logger.LogInformation("{cars} cars returned!", cars.Count);
        }

        return root;
    }
}
