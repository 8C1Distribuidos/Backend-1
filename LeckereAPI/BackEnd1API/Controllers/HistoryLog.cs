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
        static string folderPath = Combine(GetCurrentDirectory(),"data");
        private static List<Query> queries;
        [HttpGet]
        public ActionResult<IEnumerable<Query>> SendLog(){
            LoadFile();
            if(queries!=null)
            return Ok(queries);
            else
            return NotFound("Path:" + folderPath);
        }
        public static void SaveFile(){
            
            if(!Directory.Exists(folderPath)){
                CreateDirectory(folderPath);
            }
            string filePath = Combine(folderPath,"HistoyLog");
            if(System.IO.File.Exists(filePath)){
                using(StreamWriter writer  = new StreamWriter(filePath,false)){
                    writer.Write(JsonHandler<List<Query>>.Serialize(queries));
                    writer.Close();
                }
            }else{
                System.IO.File.Create(filePath);
                using(StreamWriter writer  = new StreamWriter(filePath,false)){
                    writer.Write(JsonHandler<List<Query>>.Serialize(queries));
                    writer.Close();
                }
            }
        }
        public static void LoadFile(){
            queries = new List<Query>();
            if(!Directory.Exists(folderPath)){
                CreateDirectory(folderPath);
            }
            string filePath = Combine(folderPath,"HistoyLog");
            if(System.IO.File.Exists(filePath)){
                using(StreamReader reader  = System.IO.File.OpenText(filePath)){
                    JsonSerializer serializer = new JsonSerializer();
                    queries = JsonConvert.DeserializeObject<List<Query>>(reader.ReadToEnd());
                    if(queries == null) queries = new List<Query>();
                    reader.Close();
                }
            }else{
                System.IO.File.Create(filePath);
                queries = new List<Query>();
            }
        }
        public static void AddQuery(Query query){
            if(query!=null){
                HistoryLog.queries.Add(query);
                SaveFile();
                return;
            }
            System.Console.WriteLine("Ñooooo");
            
        }
    }
}