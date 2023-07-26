using System.Globalization;
using System.Xml.Linq;
using testAPI.Contracts;
using testAPI.Controllers;

namespace testAPI.Services
{
    public class testService : Itest
    {
        private readonly ILogger<TestXMLAPI> _logger;

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
    }
}
