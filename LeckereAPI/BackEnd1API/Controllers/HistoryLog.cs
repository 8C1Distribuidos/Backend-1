using System;
using System.IO;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BackEnd1API.Models;
using Newtonsoft.Json;
using BackEnd1API.Controllers;
using System.Net;

namespace BackEnd1API.Controllers
{
    [Route("api/HistotyLog")]
    [ApiController]
    public class HistoryLog : ControllerBase{
        private static List<Query> queries = new List<Query>();
        [HttpGet]
        public ActionResult<IEnumerable<Query>> SendLog(){
            return Ok(queries);
        }
        public static void SaveFile(){
            string folderPath = Combine((string) GetFolderPath(SpecialFolder.Desktop),"data");
            if(!Directory.Exists(folderPath)){
                CreateDirectory(folderPath);
            }
            string filePath = Combine(folderPath,"HistoyLog");
            if(System.IO.File.Exists(filePath)){
                using(StreamWriter writer  = new StreamWriter(filePath,false)){
                    writer.Write(JsonHandler<List<Query>>.Serialize(queries));
                }
            }else{
                System.IO.File.Create(filePath);
                queries = new List<Query>();
            }
        }
        public static void LoadFile(){
            queries = new List<Query>();
            string folderPath = Combine((string) GetFolderPath(SpecialFolder.Desktop),"data");
            if(!Directory.Exists(folderPath)){
                CreateDirectory(folderPath);
            }
            string filePath = Combine(folderPath,"HistoyLog");
            if(System.IO.File.Exists(filePath)){
                using(StreamReader reader  = System.IO.File.OpenText(filePath)){
                    JsonSerializer serializer = new JsonSerializer();
                    queries = JsonConvert.DeserializeObject<List<Query>>(reader.ReadToEnd());
                }
            }else{
                System.IO.File.Create(filePath);
            }
        }
        public static void AddQuery(Query query){
            if(query!=null){
                System.Console.WriteLine(query.date.ToString());
                System.Console.Write(query.action + " : ");
                System.Console.WriteLine(query.status);
                HistoryLog.queries.Add(query);
                SaveFile();
                return;
            }
            System.Console.WriteLine("Ñooooo");
            
        }
    }
}