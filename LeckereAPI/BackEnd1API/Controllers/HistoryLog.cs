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
        public IActionResult SendLog(){
            return Ok("Yes");
        }
        public void SaveFile(){
            string folderPath = Combine((string) GetFolderPath(SpecialFolder.Desktop),"data");
            if(!Directory.Exists(folderPath)){
                CreateDirectory(folderPath);
            }
            string filePath = Combine(folderPath,"HistoyLog");
            if(System.IO.File.Exists(filePath)){
                using(FileStream reader  = System.IO.File.OpenWrite(filePath)){
                    JsonSerializer serializer = new JsonSerializer();
                    queries = JsonConvert.DeserializeObject<List<Query>>(reader.ToString());
                }
            }else{
                System.IO.File.Create(filePath);
                queries = new List<Query>();
            }
        }
        public void LoadFile(){
            string folderPath = Combine((string) GetFolderPath(SpecialFolder.Desktop),"data");
            if(!Directory.Exists(folderPath)){
                CreateDirectory(folderPath);
            }
            string filePath = Combine(folderPath,"HistoyLog");
            if(System.IO.File.Exists(filePath)){
                using(StreamReader reader  = System.IO.File.OpenText(filePath)){
                    JsonSerializer serializer = new JsonSerializer();
                    queries = JsonConvert.DeserializeObject<List<Query>>(reader.ToString());
                }
            }else{
                System.IO.File.Create(filePath);
                queries = new List<Query>();
            }
        }
        public void AddQuery(Query query){
            queries.Add(query);
            SaveFile();
        }
    }
}