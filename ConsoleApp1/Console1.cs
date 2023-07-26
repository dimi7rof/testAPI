using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Console1
    {
        public static string TestAPI()
        {
            string URL = "https://localhost:7093/TestXMLAPI/Prices/";

            Console.WriteLine("Insert file name");
            string fileName = Console.ReadLine();

            while (fileName != "END" || fileName == string.Empty)
            {
                var request = (HttpWebRequest)WebRequest.Create(URL + fileName);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                Console.WriteLine(responseString ?? "No file found!");
                Console.WriteLine("Insert file name");
                fileName = Console.ReadLine();
            }
            return "Exit program";
        }
    }
}
