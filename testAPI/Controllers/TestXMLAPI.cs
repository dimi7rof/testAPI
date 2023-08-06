using Microsoft.AspNetCore.Mvc;
using testAPI.Contracts;

namespace testAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestXMLAPI : ControllerBase
    {
        private readonly Itest _service;

        public TestXMLAPI( Itest service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Get/{fileName}")]
        public string Get(string fileName)
            => _service.Get(fileName).ToString();
        
        [HttpGet]
        [Route("Prices/{fileName}")]
        public string Prices(string fileName)
            => _service.Price(fileName).ToString();

        [HttpGet]
        [Route("SqlToXml/{fileName}")]
        public string SqlToXml(string fileName) =>
            _service.SqlToXml(fileName).ToString();

    }
}
