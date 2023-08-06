using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Console1
    {
        public static async Task<string> TestAPI()
        {
            string URL = "https://localhost:7093/TestXMLAPI/Prices/";

            Console.WriteLine("Insert file name");
            string fileName = "cd.xml";

            while (fileName != "END" || fileName == string.Empty)
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(URL + fileName);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonResponse ?? "No file found!");

                Console.WriteLine("Insert file name");
                fileName = Console.ReadLine();
            }
            return "Exit program";
        }
    }
}
