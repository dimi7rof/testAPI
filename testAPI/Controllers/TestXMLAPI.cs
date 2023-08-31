using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using testAPI.Contracts;

namespace testAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestXMLAPI : ControllerBase
    {
        private readonly Itest _service;

        private readonly string _configuration;

        private readonly IDbConnection _connection;

        public TestXMLAPI(Itest service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration.GetValue<string>("ConnectionStrings:desktop");
            _connection = new SqlConnection(_configuration);
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
            _service.SqlToXml(fileName, _configuration).ToString();

        [HttpGet]
        [Route("Dapper")]
        public string Dapper() =>
            _service.Dapper(_configuration).ToString();

        [HttpGet]
        [Route("Sqlkata")]
        public string Sqlkata() =>
           _service.Sqlkata(_configuration).ToString();

        [HttpGet]
        [Route("ViewCars")]
        public string ViewCars() =>
            _service.ViewCars(_connection).ToString();

    }
}
