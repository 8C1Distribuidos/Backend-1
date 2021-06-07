using System;
using System.Collections.Generic;
using System.Net;
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
        string url = ConnectionTester.getURL() + "/categories";
        
        
        [HttpGet("Test")]
        public IActionResult Test(){
            return Ok("Funcionando");
        }

        [HttpPost("GetAll")]
        public ActionResult<IEnumerable<Category>> GetAll(Data data)
        {
            Query query = new Query("Get all categories");
            query.user = data.usuario;
            try
            {
                IEnumerable<Category> c = DatabaseConsumer<Category>.GetAll(url);
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
                return NoContent();
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = Query.DateNow();
                HistoryLog.AddQuery(query);
                if(ex.Status == WebExceptionStatus.ProtocolError){
                    var response = ex.Response as HttpWebResponse;
                    System.Console.WriteLine("StatusCode: " + (int)response.StatusCode);
                    if(((int)response.StatusCode) == 500){
                        Response.StatusCode = 504;
                        return Content("No hay conexion con db \n" + ex.Message);
                    }

                }
                return NotFound(ex.Message);
            }
            
        }

        [HttpPost("GetId")]
        public ActionResult<Category> GetByID(Data data)
        {
            // puede fallar
            int id = Int32.Parse(data.informacion);
            //

            Query query = new Query("Get category by id: " + id);
            query.user = data.usuario;

            try
            {
                Category c = DatabaseConsumer<Category>.Get(url +$"/find?id={id}");
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
                return NotFound();
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = Query.DateNow();
                HistoryLog.AddQuery(query);
                if(ex.Status == WebExceptionStatus.ProtocolError){
                    var response = ex.Response as HttpWebResponse;
                    System.Console.WriteLine("StatusCode: " + (int)response.StatusCode);
                    if(((int)response.StatusCode) == 500){
                        Response.StatusCode = 504;
                        return Content("No hay conexion con db \n" + ex.Message);
                    }

                }
                return NotFound(ex.Message);
            }
            
        }

        [HttpPost("GetByCatalog")]
        public ActionResult<IEnumerable<Category>> GetByCatalog(Data data)
        {
            // puede fallar
            int id = Int32.Parse(data.informacion);
            //

            Query query = new Query("Get category by cataglog id: " + id);
            query.user = data.usuario;
            
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
                    query.date = Query.DateNow();
                    HistoryLog.AddQuery(query);
                    return NoContent();
                }
                query.status = "Correcto";
                query.date = Query.DateNow();
                HistoryLog.AddQuery(query);
                return Ok(result);
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = Query.DateNow();
                HistoryLog.AddQuery(query);
                if(ex.Status == WebExceptionStatus.ProtocolError){
                    var response = ex.Response as HttpWebResponse;
                    System.Console.WriteLine("StatusCode: " + (int)response.StatusCode);
                    if(((int)response.StatusCode) == 500){
                        Response.StatusCode = 504;
                        return Content("No hay conexion con db \n" + ex.Message);
                    }

                }
                return NotFound(ex.Message);
            }
            
        }

        [HttpPost("Post")]
        public ActionResult<Category> PostCategory(Data data)
        {
            Category newCategory = JsonHandler<Category>.Deserialize(data.informacion);
            Query query = new Query("Post category: " + newCategory.name);
            query.user = data.usuario;

            try
            {
                Category c = DatabaseConsumer<Category>.Post(url,JsonHandler<Category>.Serialize(newCategory));
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
                return NotFound();
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = Query.DateNow();
                HistoryLog.AddQuery(query);
                if(ex.Status == WebExceptionStatus.ProtocolError){
                    var response = ex.Response as HttpWebResponse;
                    System.Console.WriteLine("StatusCode: " + (int)response.StatusCode);
                    if(((int)response.StatusCode) == 500){
                        Response.StatusCode = 504;
                        return Content("No hay conexion con db \n" + ex.Message);
                    }

                }
                return NotFound(ex.Message);
            }
            
        }
        
        [HttpPut("Put")]
        public ActionResult<Category> PutCategory(Data data)
        {
            Category updatedCategory = JsonHandler<Category>.Deserialize(data.informacion);

            Query query = new Query("Put category: " + updatedCategory.name);
            query.user = data.usuario;

            try
            {
                Category c = DatabaseConsumer<Category>.Put(url,JsonHandler<Category>.Serialize(updatedCategory));
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
                return NotFound();
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = Query.DateNow();
                HistoryLog.AddQuery(query);
                if(ex.Status == WebExceptionStatus.ProtocolError){
                    var response = ex.Response as HttpWebResponse;
                    System.Console.WriteLine("StatusCode: " + (int)response.StatusCode);
                    if(((int)response.StatusCode) == 500){
                        Response.StatusCode = 504;
                        return Content("No hay conexion con db \n" + ex.Message);
                    }

                }
                return NotFound(ex.Message);
            }
        }
        

        [HttpDelete("Delete")]
        public ActionResult<Category> DeleteCategory(Data data)
        {
            // puede fallar
            int id = Int32.Parse(data.informacion);
            //

            Query query = new Query("Delete category: " + id);
            query.user = data.usuario;

            try
            {
                bool c = DatabaseConsumer<Category>.Delete(url + $"?id={id}");
                if(c)
                {
                    query.status = "Correcto";
                    query.date = Query.DateNow();
                    HistoryLog.AddQuery(query);
                    return Ok("Category eliminated");
                }
                query.status = "Incorrecto";
                query.date = Query.DateNow();
                HistoryLog.AddQuery(query);
                return NotFound();
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = Query.DateNow();
                HistoryLog.AddQuery(query);
                if(ex.Status == WebExceptionStatus.ProtocolError){
                    var response = ex.Response as HttpWebResponse;
                    System.Console.WriteLine("StatusCode: " + (int)response.StatusCode);
                    if(((int)response.StatusCode) == 500){
                        Response.StatusCode = 504;
                        return Content("No hay conexion con db \n" + ex.Message);
                    }

                }
                return NotFound(ex.Message);
            }
            
        }
    }
}