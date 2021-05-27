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
            Queryable query = new Query("GET", "Get all categories");
            try
            {
                IEnumerable<Category> c = DatabaseConsumer<Category>.GetAll(url);
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
                return NoContent();
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
        public ActionResult<Category> GetByID(int id)
        {
            Query query = new Query("GET", "Get category by id: " + id);
            try
            {
                Category c = DatabaseConsumer<Category>.Get(url +$"/find?id={id}");
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
                return NotFound();
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;
                
                HistoryLog.AddQuery(query);
                return NotFound(ex.Message);
            }
            
        }

        [HttpGet("GetByCatalog")]
        public ActionResult<IEnumerable<Category>> GetByCatalog(int id)
        {
            Query query = new Query("GET", "Get category by cataglog id: " + id);
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
                if(result.Count==0)
                {
                    query.status = "Incorrecto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;
                    
                    HistoryLog.AddQuery(query);
                    return NoContent();
                }
                query.status = "Correcto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;
                
                HistoryLog.AddQuery(query);
                return Ok(result);
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;
                
                HistoryLog.AddQuery(query);
                return NotFound(ex.Message);
            }
            
        }

        [HttpPost("Post")]
        public ActionResult<Category> PostCategory(Category newCategory)
        {
            Query query = new Query("POST", "Post category: " + newCategory.name);
            try
            {
                Category c = DatabaseConsumer<Category>.Post(url,JsonHandler<Category>.Serialize(newCategory));
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
                return NotFound();
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;
                
                HistoryLog.AddQuery(query);
                return NotFound(ex.Message);
            }
            
        }
        
        [HttpPut("Put")]
        public ActionResult<Category> PutCategory(Category updatedCategory)
        {
            Query query = new Query("PUT", "Put category: " + updatedCategory.name);
            try
            {
                Category c = DatabaseConsumer<Category>.Put(url,JsonHandler<Category>.Serialize(updatedCategory));
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
                return NotFound();
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;
                
                HistoryLog.AddQuery(query);
                return NotFound(ex.Message);
            }
        }
        

        [HttpDelete("Delete")]
        public ActionResult<Category> DeleteCategory(int id)
        {
            Query query = new Query("DELETE", "Delete category: " + id);
            try
            {
                bool c = DatabaseConsumer<Category>.Delete(url + $"?id={id}");
                if(c)
                {
                    query.status = "Correcto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;
                    
                    HistoryLog.AddQuery(query);
                    return Ok("Category eliminated");
                }
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;
                
                HistoryLog.AddQuery(query);
                return NotFound();
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second;
                
                HistoryLog.AddQuery(query);
                return NotFound(ex.Message);
            }
            
        }
    }
}