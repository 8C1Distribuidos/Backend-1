using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BackEnd1API.Models;
using System.Net;

namespace BackEnd1API.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoriesController : ControllerBase {
        string url = ConnectionTester.getURL() + "/categories";
        
        
        [HttpGet("Test")]
        public IActionResult Test(){
            return Ok("Funcionando");
        }

        /*
        try
        {
            
        }
        catch (WebException ex)
        {
            return NotFound(ex.Message);
        }
        */

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            try
            {
                IEnumerable<Category> c = DatabaseConsumer<Category>.GetAll(url);
                if(c!=null) return Ok(c);
                return NoContent();
            }
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [HttpGet("GetId")]
        public ActionResult<Category> GetByID(int id)
        {
            try
            {
                Category c = DatabaseConsumer<Category>.Get(url +$"/find?id={id}");
                if(c!=null){
                    return Ok(c);
                }
                return NotFound();
                }
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [HttpGet("GetByCatalog")]
        public ActionResult<IEnumerable<Category>> GetByCatalog(int id)
        {
            try
            {
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
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [HttpPost("Post")]
        public ActionResult<Category> PostCategory(Category newCategory)
        {
            try
            {
                Category c = DatabaseConsumer<Category>.Post(url,JsonHandler<Category>.Serialize(newCategory));
                if(c!=null) return Ok(c);
                return NotFound();
            }
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
            
        }
        
        [HttpPut("Put")]
        public ActionResult<Category> PutCategory(Category updatedCategory)
        {
            try
            {
                Category c = DatabaseConsumer<Category>.Put(url,JsonHandler<Category>.Serialize(updatedCategory));
                if(c!=null) return Ok(c);
                return NotFound();
            }
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
        }
        

        [HttpDelete("Delete")]
        public ActionResult<Category> DeleteCategory(int id)
        {
            try
            {
                bool c = DatabaseConsumer<Category>.Delete(url + $"?id={id}");
                if(c) return Ok("Category eliminated");
                return NotFound();
            }
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
            
        }
    }
}