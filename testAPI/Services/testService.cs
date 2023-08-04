using SqlKata.Compilers;
using SqlKata;
using System.Globalization;
using System.Xml.Linq;
using testAPI.Contracts;
using testAPI.Controllers;
using testAPI.Models;
using System.Data.SqlClient;
using Dapper;

namespace testAPI.Services
{
    public class testService : Itest
    {
        private readonly ILogger<TestXMLAPI> _logger;
        private readonly string _connectionString = "Server=DESKTOP-8IRVI79;Database=Theatre;Integrated Security=true;";

        public testService(ILogger<TestXMLAPI> logger)
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
                _logger.Log(LogLevel.Information, $"{DateTime.Now} - {fileName} file not found");
            }

            XDocument document = XDocument.Load($"E:\\xml\\{fileName}");
            _logger.Log(LogLevel.Information, $"{DateTime.Now} - {fileName} file found");

            XElement element = new XElement("CD");
            XDocument resultDoc = new XDocument(element);
            List<double> prices = new List<double>();

            foreach (var cd in document.Root.Elements())
            {
                string price = cd.Element("PRICE").Value;
                prices.Add(double.Parse(price, CultureInfo.InvariantCulture));
                _logger.Log(LogLevel.Information, $"{DateTime.Now} - Price of {price} added");
            }

            foreach (var price in prices.OrderBy(x => x))
            {
                element.Add(new XElement("PRICE", price));
            }
            return resultDoc;
        }

        public XDocument SqlToXml(string fileName)
        {
            IEnumerable<Play> plays = new List<Play>();
            var compiler = new SqlServerCompiler();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = new Query(fileName);
                SqlResult result = compiler.Compile(query);
                plays = connection.Query<Play>(result.Sql).ToList();
            }

            XElement root = new XElement(fileName);
            XDocument resultDoc = new XDocument(root);

            foreach (var play in plays)
            {
                XElement element = new XElement(("Play"),
                    new XElement("Title", play.Title),
                    new XElement("Duration", play.Duration.TotalSeconds),
                    new XElement("Rating", play.Rating),
                    new XElement("Description", play.Description),
                    new XElement("Screenwriter", play.Screenwriter));
                root.Add(element);
                _logger.Log(LogLevel.Information, $"{DateTime.Now} - Play {play.Title} added to document");
            }
            return resultDoc;
        }
    }
}
