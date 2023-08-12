using System.Xml.Linq;

namespace testAPI.Contracts
{
    public interface Itest
    {
        public XDocument Get(string fileName);

        public XDocument Price(string fileName);

        public XDocument SqlToXml(string fileName,string _configuration);

        public XElement Sqlkata(string _configuration);

        public XElement Dapper(string _configuration);

        public XElement ViewCars(string _connectionString);
    }
}
