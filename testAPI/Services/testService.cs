using SqlKata.Compilers;
using SqlKata;
using System.Globalization;
using System.Xml.Linq;
using testAPI.Contracts;
using testAPI.Controllers;
using testAPI.Models;
using System.Data.SqlClient;
using Dapper;

namespace testAPI.Services;

public class testService : Itest
{
    private readonly ILogger<testService> _logger;
    private readonly string _connectionString = "Server=DESKTOP-8IRVI79;Database=Theatre;Integrated Security=true;";

    public testService(ILogger<testService> logger)
    {
        _logger = logger;
    }


    public XDocument Get(string fileName)
    {
        _logger.Log(LogLevel.Information, $"search for {fileName} file");
        if (fileName == null || fileName == string.Empty)
        {
            _logger.Log(LogLevel.Information, $"{fileName} file not found");
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

    

    public XDocument SqlToXml(string tableName)
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


    public XElement Dapper()
    {
        throw new NotImplementedException();

    }

    public XElement Sqlkata()
    {
        throw new NotImplementedException();
    }
}
