using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoriesController : ControllerBase {
        string url = "http://localhost:9081/categories";
        //FakeForJSON fake = Startup.fake;
        [HttpGet("Test")]
        public IActionResult Test(){
            return Ok("Funcionando");
        }

        [HttpGet("GetAll")]
        public IEnumerable<Category> GetAll(){
            IEnumerable<Category> c = DatabaseConsumer<Category>.GetAll(url);
            if(c!=null)return c;
            return null;
        }

        [HttpGet("GetId")]
        public ActionResult<Category> GetClasificationgByID(int id){
            Category c = DatabaseConsumer<Category>.Get(url +$"/find?id={id}");
            if(c!=null){
                return Ok(c);
            }
            return NotFound();
        }

        /*
        [HttpPost("Post")]
        public bool PostClasification(Category newClasification){
            //Catalog c = JsonHandler<Catalog>.Deserialize(jsonString);
            
        }
        USELESS ... maybe
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
        */

        [HttpPost("Post")]
        public ActionResult<Category> PostCategory(Category newCategory){
            Category c = DatabaseConsumer<Category>.Post(url,JsonHandler<Category>.Serialize(newCategory));
            if(c!=null) return Ok(c);
            return NotFound();
        }
        
        [HttpPut("Put")]
        public ActionResult<Category> PutCategory(Category updatedCategory){
            Category c = DatabaseConsumer<Category>.Put(url,JsonHandler<Category>.Serialize(updatedCategory));
            if(c==null) return Ok(c);
            return NotFound();
        }
        

        [HttpDelete("Delete")]
        public ActionResult<Category> DeleteCategory(int id){
            bool c = DatabaseConsumer<Category>.Delete(url + $"?id={id}");
            if(c) return Ok("Category eliminated");
            return NotFound();
        }
    }
}