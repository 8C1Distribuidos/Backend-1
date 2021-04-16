using BackEnd1API.Models;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;



namespace BackEnd1API.Controllers
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
        static public IEnumerable<T> GetAllProducts(string url)
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
                                var read = objReader.ReadToEnd();
                                JObject jsonResponse = JObject.Parse(read);
                                JArray objResponse = (JArray)jsonResponse["content"];
                                T[] respArray = new T[objResponse.Count];
                                for(int i=0;i<respArray.Length-1;i++){
                                    respArray[i] = objResponse[i].ToObject<T>();
                                }
                                IEnumerable<T> resp = respArray;
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

        static public IEnumerable<T> GetList(string url)
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

        static public T Put(string url,string data)
        {
            HttpWebRequest request;
            
            request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }
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
                        // Do something with responseBody
                        return JsonHandler<T>.Deserialize(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                System.Console.WriteLine($"URL: {url}:");
                System.Console.WriteLine($"Data: {data}:");
                System.Console.WriteLine(ex.Message);
                return default(T);
            }
        }

        static public T Post(string url, string data)
        {
            HttpWebRequest request;
            
            request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }
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
                        // Do something with responseBody
                        return JsonHandler<T>.Deserialize(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                System.Console.WriteLine($"URL: {url}");
                System.Console.WriteLine($"Data: {data}");
                System.Console.WriteLine(ex.Message);
                return default(T);
            }
        }
        static public bool Delete(string url)
        {
            HttpWebRequest request;
            
            request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "DELETE";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return false;
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                return true;
                            }
                    }
                }
            }
            catch (WebException ex)
            {
                System.Console.WriteLine($"URL: {url}:");
                System.Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}