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