using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BackEnd1API.Models;

namespace BackEnd1API.Controllers
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
        public ActionResult<IEnumerable<Category>> GetAll(){
            IEnumerable<Category> c = DatabaseConsumer<Category>.GetAll(url);
            if(c!=null)return Ok(c);
            return NoContent();
        }

        [HttpGet("GetId")]
        public ActionResult<Category> GetByID(int id){
            Category c = DatabaseConsumer<Category>.Get(url +$"/find?id={id}");
            if(c!=null){
                return Ok(c);
            }
            return NotFound();
        }
        [HttpGet("GetByCatalog")]
        public ActionResult<IEnumerable<Category>> GetByCatalog(int id){
            IEnumerable<Category> c = DatabaseConsumer<Category>.GetAll(url);
            if(c==null) return NoContent();
            List<Category> result = new List<Category>();
            foreach(Category cat in c){
                if(cat!=null){
                    if(cat.catalog.id.Equals(id)){
                        result.Add(cat);
                    }
                }
            }
            if(result.Count==0)return NoContent();
            return Ok(result);
        }

        [HttpPost("Post")]
        public ActionResult<Category> PostCategory(Category newCategory){
            Category c = DatabaseConsumer<Category>.Post(url,JsonHandler<Category>.Serialize(newCategory));
            if(c!=null) return Ok(c);
            return NotFound();
        }
        
        [HttpPut("Put")]
        public ActionResult<Category> PutCategory(Category updatedCategory){
            Category c = DatabaseConsumer<Category>.Put(url,JsonHandler<Category>.Serialize(updatedCategory));
            if(c!=null) return Ok(c);
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