using System;
using static System.Console;
using System.IO;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BackEnd1API.Models;
using BackEnd1API.Controllers;
using System.Net;

namespace BackEnd1API.Controllers
{
    [Route("api/HistotyLog")]
    [ApiController]
    public class HistoryLog{
        [HttpGet]
        public IActionResult SendLog(){
            return Ok("Yes");
        }
        public void SaveFile(){
            string path = Combine((string) GetFolderPath(SpecialFolder.Desktop),"CosasDeXml");
        }
        public void LoadFile(){
            
        }
    }
}