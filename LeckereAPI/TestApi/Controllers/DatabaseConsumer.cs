using TestApi.Models;
using System.Net;
namespace TestApi.Controllers
{
    public class DatabaseConsumer
    {
        private const string URL = "http://localhost:9081/";
        HttpWebRequest request;
        public Product GetProduct(int id)
        {
            string url = URL + "products/find?id=" + id;
            
            request = (HttpWebRequest) WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    
                }
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}