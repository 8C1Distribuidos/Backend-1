using TestApi.Models;
using System.Net;
using System.IO;
using System.Collections.Generic;
namespace TestApi.Controllers
{
    public class DatabaseConsumer<T>
    {
        static public T Get(string url)
        {
            HttpWebRequest request;
            
            request = (HttpWebRequest) WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return default(T);
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                        string responseBody = objReader.ReadToEnd();
                        T resp = JsonHandler<T>.Deserialize(responseBody);
                        return resp;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                System.Console.WriteLine($"URL{url}:");
                System.Console.WriteLine(ex.Message);
                return default(T);
            }
        }
        
        static public IEnumerable<T> GetAll(string url)
        {
            HttpWebRequest request;
            
            request = (HttpWebRequest) WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                IEnumerable<T> resp = JsonHandler<IEnumerable<T>>.Deserialize(responseBody);
                                return resp;
                            }
                    }
                }
            }
            catch (WebException ex)
            {
                System.Console.WriteLine($"URL{url}:");
                System.Console.WriteLine(ex.Message);
                return null;
            }
        }

        static public T Put(string url)
        {
            HttpWebRequest request;
            
            request = (HttpWebRequest) WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return default(T);
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                T resp = JsonHandler<T>.Deserialize(responseBody);
                                return resp;
                            }
                    }
                }
            }
            catch (WebException ex)
            {
                System.Console.WriteLine($"URL{url}:");
                System.Console.WriteLine(ex.Message);
                return default(T);
            }
        }

        static public T Post(string url)
        {
            HttpWebRequest request;
            
            request = (HttpWebRequest) WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return default(T);
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                T resp = JsonHandler<T>.Deserialize(responseBody);
                                return resp;
                            }
                    }
                }
            }
            catch (WebException ex)
            {
                System.Console.WriteLine($"URL{url}:");
                System.Console.WriteLine(ex.Message);
                return default(T);
            }
        }
        static public T Delete(string url)
        {
            HttpWebRequest request;
            
            request = (HttpWebRequest) WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return default(T);
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                T resp = JsonHandler<T>.Deserialize(responseBody);
                                return resp;
                            }
                    }
                }
            }
            catch (WebException ex)
            {
                System.Console.WriteLine($"URL{url}:");
                System.Console.WriteLine(ex.Message);
                return default(T);
            }
        }
    }
}