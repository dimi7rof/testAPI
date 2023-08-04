using System.Xml.Linq;

namespace testAPI.Contracts
{
    public interface Itest
    {
        public XDocument Get(string fileName);

        public XDocument Price(string fileName);

        public XDocument SqlToXml(string fileName);
    }
}
