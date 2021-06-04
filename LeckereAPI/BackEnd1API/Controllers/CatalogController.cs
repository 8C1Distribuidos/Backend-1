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

        [HttpPost("GetAll")]
        public ActionResult<IEnumerable<Catalog>> GetAll(Data data)
        {
            Query query = new Query("Get all catalogs");
            query.user = data.usuario;
            try
            {
                IEnumerable<Catalog> c = DatabaseConsumer<Catalog>.GetAll(url);
                if(c!=null)
                {
                    query.status = "Correcto";
                    query.date = Query.DateNow();
                    HistoryLog.AddQuery(query);
                    return Ok(c);
                }
                query.status = "Incorrecto";
                query.date = Query.DateNow();
                HistoryLog.AddQuery(query);
                return NotFound(c);
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = Query.DateNow();
                HistoryLog.AddQuery(query);
                return NotFound(ex.Message);
            }
        }

        [HttpPost("GetId")]
        public ActionResult<Catalog> GetCatalogByID(Data data)
        {
            int id;
            // puede fallar
            try{
                 id = Int32.Parse(data.informacion);
            }catch(FormatException ex){
                return NotFound(ex.Message);
            }catch(Exception ex){
                return NotFound(ex.Message);
            }
            
            // data

            Query query = new Query("Get catalog by id: " + id);
            query.user = data.usuario;
            try
            {
                Catalog c = DatabaseConsumer<Catalog>.Get(url + $"/find?id={id}");
                if(c!=null)
                {
                    query.status = "Correcto";
                    query.date = Query.DateNow();
                    HistoryLog.AddQuery(query);
                    return Ok(c);
                }
                query.status = "Incorrecto";
                query.date = Query.DateNow();
                HistoryLog.AddQuery(query);
                return NotFound(c);
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = Query.DateNow();
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