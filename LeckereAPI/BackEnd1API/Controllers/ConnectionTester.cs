using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace BackEnd1API.Controllers
{
    public class ConnectionTester{
        private static string[] direcciones = 
        {
            "http://25.16.129.2:9081" ,//luigi
            "http://localhost:9081",
            "http://25.4.107.19:9081"
                 
        };
        static string url;
        public static void TestConnections(){
            bool isGood = false;
            while(!isGood){
                for(int i=0; i<direcciones.Length&&!isGood;i++){
                    try{
                        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(direcciones[i] + "/catalogs");
                        request.Timeout = 5000;
                        using(HttpWebResponse response = request.GetResponse() as HttpWebResponse){
                            if(response.StatusCode == HttpStatusCode.OK){
                                url = direcciones[i];
                                isGood=true;
                                System.Console.WriteLine("Connection succesful with: " + direcciones[i]);
                                return;
                            }
                        }
                        
                    }catch(WebException ex){
                        System.Console.WriteLine("Unable to connect with: " + direcciones[i]);
                        if(ex.Status == WebExceptionStatus.ProtocolError){
                            var response = ex.Response as HttpWebResponse;
                            System.Console.WriteLine("StatusCode: " + (int)response.StatusCode);
                            if(((int)response.StatusCode) == 500){
                                url = direcciones[i];
                                isGood=true;
                                System.Console.WriteLine("Connection succesful with: " + direcciones[i]);
                            }

                        }
                        System.Console.WriteLine(ex.Message);
                    }
                }
                if(!isGood){
                    System.Console.WriteLine("Unable to connect with any know server");
                    System.Console.WriteLine("Trying again...");
                }
            }
            
        }
        public static string getURL(){
                return url;
        }
    }
}