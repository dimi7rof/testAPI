using Microsoft.Data.SqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Xml.Linq;

namespace MinimalAPItest
{
    public class CarViewer
    {
        public void CarViewer(IServiceCollection services)
        {
            services.AddDbContext<BloggingContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("desktop")));
        }
        public static string CarViewer1()
        {
            var connection = new SqlConnection("Server=DESKTOP-2UM0VJE\\\\SQLEXPRESS;Database=MyCarStatistics;Integrated Security=true;TrustServerCertificate=true");
            var tableName = "Cars";
            var compiler = new SqlServerCompiler();
            var root = new XElement(tableName);

            using (connection)
            {
                connection.Open();
                MyLogger("Connection open");
                var db = new QueryFactory(connection, compiler, 15);
                var cars = db.Query(tableName).Where("IsDeleted", false).Get();

                foreach (var car in cars)
                {
                    var element = new XElement(("Car"),
                        new XAttribute("Id", car.Id),
                        new XElement("Brand", car.Brand),
                        new XElement("Model", car.CarModel),
                        new XElement("Mileage", car.Mileage));
                    root.Add(element);
                }
                MyLogger("Car found");
            }

            return root.ToString();
        }
        private static void MyLogger(string message)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger(string.Empty);
            logger.LogInformation("Test Logger for CarViewer class: {message}", message);
        }

    }
}
