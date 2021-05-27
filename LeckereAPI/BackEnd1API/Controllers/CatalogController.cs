using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BackEnd1API.Models;
using System.Net;


namespace BackEnd1API.Controllers
{
    
    [Route("api/Catalog")]
    [ApiController]
    public class CatalogsController : ControllerBase {
        string url = ConnectionTester.getURL() + "/catalogs";
        [HttpGet("Test")]
        public IActionResult Test(){
            return Ok("Funcionando");
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Catalog>> GetAll()
        {
            Query query = new Query("GET","Get all catalogs");
            try
            {
                IEnumerable<Catalog> c = DatabaseConsumer<Catalog>.GetAll(url);
                if(c!=null)
                {
                    query.status = "Correcto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;
                    
                    HistoryLog.AddQuery(query);
                    return Ok(c);
                }
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;
                
                HistoryLog.AddQuery(query);
                return NotFound(c);
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;
                
                HistoryLog.AddQuery(query);
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetId")]
        public ActionResult<Catalog> GetCatalogByID(int id)
        {
            Query query = new Query("GET", "Get catalog by id: " + id);
            try
            {
                Catalog c = DatabaseConsumer<Catalog>.Get(url + $"/find?id={id}");
                if(c!=null)
                {
                    query.status = "Correcto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;

                    HistoryLog.AddQuery(query);
                    return Ok(c);
                }
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;
                
                HistoryLog.AddQuery(query);
                return NotFound(c);
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;
                
                HistoryLog.AddQuery(query);
                return NotFound(ex.Message);
            }
        }

        /*
        [HttpPost("Post")]
        public bool PostCatalog(Catalog newCatalog){
            //Catalog c = JsonHandler<Catalog>.Deserialize(jsonString);
            foreach (Catalog c in fake.catalogs)
            {
                if(c.id == newCatalog.id){
                    return false;
                }
            }
            fake.catalogs.Add(newCatalog);
            return true;
        }
        [HttpPost("PostJson")]
        public bool PostCatalogJson(string jsonString ){
            Catalog newCatalog = JsonHandler<Catalog>.Deserialize(jsonString);
            if(newCatalog == null)return false;
            foreach (Catalog c in fake.catalogs)
            {
                if(c.id == newCatalog.id){
                    return false;
                }
            }
            fake.catalogs.Add(newCatalog);
            return true;
        }

        
        [HttpPut("Put")]
        public bool PutCatalog(Catalog updatedCatalog){
            //Catalog updatedCatalog = JsonHandler<Catalog>.Deserialize(jsonString);
            foreach(Catalog c in fake.catalogs){
                if(c.id == updatedCatalog.id){
                    c.name = updatedCatalog.name;
                    return true;
                }
            }
            return false;
        }

        [HttpDelete("Delete")]
        public bool DeleteCatalog(int id){
            foreach(Catalog c in fake.catalogs){
                if(c.id == id){
                    fake.catalogs.Remove(c);
                    return true;
                }
            }
            return false;
        }
        */
    }
}