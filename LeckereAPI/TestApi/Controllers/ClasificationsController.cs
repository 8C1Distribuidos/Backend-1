using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/Clasification")]
    [ApiController]
    public class ClasificationsController : ControllerBase {
        FakeForJSON fake = Startup.fake;
        [HttpGet("Test")]
        public IActionResult Test(){
            return Ok("Funcionando");
        }

        [HttpGet("GetAll")]
        public IEnumerable<Clasification> GetAll(){
            return fake.clasifications;
        }

        [HttpGet("GetId")]
        public string GetClasificationgByID(int id){
            foreach(Clasification c in fake.clasifications){
                if(c.id == id){
                    return JsonHandler<Clasification>.Serialize(c);
                }
            }
            return null;
        }

        
        [HttpPost("Post")]
        public bool PostClasification(Clasification newClasification){
            //Catalog c = JsonHandler<Catalog>.Deserialize(jsonString);
            foreach (Clasification c in fake.clasifications)
            {
                if(c.id == newClasification.id){
                    return false;
                }
            }
            fake.clasifications.Add(newClasification);
            return true;
        }
        /*USELESS ... maybe
        [HttpPost("PostJson")]
        public bool PostClasificationJson(string jsonString ){
            Clasification newClasification = JsonHandler<Clasification>.Deserialize(jsonString);
            if(newClasification == null)return false;
            foreach (Clasification c in fake.clasifications)
            {
                if(c.id == newClasification.id){
                    return false;
                }
            }
            fake.clasifications.Add(newClasification);
            return true;
        }
        */
        
        [HttpPut("Put")]
        public bool PutClasification(Clasification updatedClasification){
            //Catalog updatedCatalog = JsonHandler<Catalog>.Deserialize(jsonString);
            foreach(Clasification c in fake.clasifications){
                if(c.id == updatedClasification.id){
                    c.name = updatedClasification.name;
                    c.catalog = updatedClasification.catalog;
                    return true;
                }
            }
            return false;
        }

        [HttpDelete("Delete")]
        public bool DeleteClasification(int id){
            foreach(Clasification c in fake.clasifications){
                if(c.id == id){
                    fake.clasifications.Remove(c);
                    return true;
                }
            }
            return false;
        }
    }
}