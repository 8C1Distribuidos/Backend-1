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
        //string url = "http://localhost:9081/catalogs";
        //string url = "http://25.4.107.19:9081/catalogs";
        string url = "http://25.16.129.2:9081/catalogs"; //luigi
        [HttpGet("Test")]
        public IActionResult Test(){
            return Ok("Funcionando");
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Catalog>> GetAll()
        {
            try
            {
                IEnumerable<Catalog> c = DatabaseConsumer<Catalog>.GetAll(url);
                if(c!=null) return Ok(c);
                return NotFound(c);
            }
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetId")]
        public ActionResult<Catalog> GetCatalogByID(int id)
        {
            try
            {
                Catalog c = DatabaseConsumer<Catalog>.Get(url + $"/find?id={id}");
                if(c!=null) return Ok(c);
                return NotFound(c);
            }
            catch (WebException ex)
            {
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