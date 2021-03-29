using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApi.Models;
using TestApi.Controllers;

namespace TestApi.Controllers
{
    
    [Route("api/Catalog")]
    [ApiController]
    public class CatalogsController : ControllerBase {
        FakeForJSON fake = Startup.fake;
        [HttpGet("Test")]
        public IActionResult Test(){
            return Ok("Funcionando");
        }

        [HttpGet("GetAll")]
        public IEnumerable<Catalog> GetAll(){
            return fake.catalogs;
        }

        [HttpGet("GetId")]
        public Catalog GetCatalogByID(int id){
            foreach(Catalog c in fake.catalogs){
                if(c.id == id){
                    //return JsonHandler<Catalog>.Serialize(c);
                    return c;
                }
            }
            return null;
        }

        
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
    }
}