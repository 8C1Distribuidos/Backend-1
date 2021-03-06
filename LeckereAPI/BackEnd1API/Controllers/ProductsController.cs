using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BackEnd1API.Models;
using BackEnd1API.Controllers;
using System.Net;


namespace BackEnd1API.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductsController : ControllerBase {
        //string url = "http://25.16.129.2:9081/products"; //Luigi?
        string url = ConnectionTester.getURL() + "/products";
        [HttpGet("Test")]
        public IActionResult Test(){
            ProductsCache.InvalidateCache();
            Response.StatusCode = 504;
            return Content("Conexion: " + ConnectionTester.getURL());
            //return Content("Quiero las exxcepciones de luigi uwu");
        }

        [HttpPost("GetAll")]
        public ActionResult<IEnumerable<Product>> GetAll(Data data)
        {
            Query query = new Query("Get all products",data.usuario);
            if(!ProductsCache.ConsultCache(url + "?page=0&size=1000")){
                try
                {
                    IEnumerable<Product> p = DatabaseConsumer<Product>.GetAllProducts(url + "?page=0&size=1000");
                    if(p!=null){
                        ProductsCache.AddCache(p.ToArray(), url + "?page=0&size=1000");
                        query.status = "Correcto";
                        query.date = Query.DateNow();
                        
                        HistoryLog.AddQuery(query);
                        return Ok(p);
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
            }else{
                return Ok(ProductsCache.GetAll());
            }
            
        }

        [HttpPost("GetList")]
        public ActionResult<IEnumerable<Product>> GetList(Data data)
        {
            string pIDS = data.informacion;

            Query query = new Query("Get lista de productos : " + pIDS,data.usuario);
            int [] ids = JsonHandler<Int32[]>.Deserialize(data.informacion);
            ProductsCache.InvalidateCache();
            string json = JsonHandler<int[]>.Serialize(ids);
            bool inCache = false;
            for(int i =0; i<ids.Length; i++){
                inCache = ProductsCache.ConsultCache(ids[i]);
            }
            if(!inCache){
                try
                {
                    IEnumerable<Product> result = new List<Product>();
                    result = DatabaseConsumer<Product>.GetList(url + "/find-list",json);
                    if(result.ToList().Count>0){
                        ProductsCache.AddCache(result.ToArray(),url + "/find-list" + json);
                        //add a historylog
                        query.status = "Correcto";
                        query.date = Query.DateNow();
                        HistoryLog.AddQuery(query);
                        return Ok(result);
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
            List<Product> r = new List<Product>();
            for(int i=0; i<ids.Length;i++){
                r.Add(ProductsCache.Get(i));
            }
            return Ok(r);
        }

        [HttpPost("GetId")]
        public ActionResult<Product> GetProductByID(Data data)
        {
            int id = Int32.Parse(data.informacion);
            Query query = new Query("Get producto de id:" + id,data.usuario);
            if(!ProductsCache.ConsultCache(id)){
                try
                {
                    Product p = DatabaseConsumer<Product>.Get(url + $"/find?id={id}");
                    if(p!=null){
                        query.status = "Correcto";
                        query.date = Query.DateNow();
                        HistoryLog.AddQuery(query);
                        return Ok(p);
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
            return Ok(ProductsCache.Get(id));
        }
        
        [HttpPost("GetByCatalog")]
        public ActionResult<IEnumerable<Product>> GetByCatalog(Data data)
        {
            int id = Int32.Parse(data.informacion);
            Query query = new Query("Get productos por catalogo:" + id,data.usuario);
            if(!ProductsCache.ConsultCache(url + "?page=0&size=1000")){
                try
                {
                    IEnumerable<Product> products = DatabaseConsumer<Product>.GetAllProducts(url + "?page=0&size=1000");
                    if(products==null) return NoContent();
                    List<Product> result = new List<Product>();
                    foreach(Product p in products){
                        if(p!=null){
                            if(p.category !=null && p.category.catalog!=null){
                                if(p.category.catalog.id.Equals(id)){
                                //System.Console.WriteLine(p.name + "(" + p.id +")");
                                //System.Console.WriteLine(p.category.id + ":" + p.category.name);
                                result.Add(p);
                                }
                            }
                        }
                }
                if(result.Count==0){
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
            }else{
                List<Product> products = ProductsCache.GetAll();
                List<Product> result = new List<Product>();
                    foreach(Product p in products){
                        if(p!=null){
                            if(p.category !=null && p.category.catalog!=null){
                                if(p.category.catalog.id.Equals(id)){
                                //System.Console.WriteLine(p.name + "(" + p.id +")");
                                //System.Console.WriteLine(p.category.id + ":" + p.category.name);
                                result.Add(p);
                                }
                            }
                        }
                    }
               if(result.Count==0){
                    query.status = "Incorrecto";
                    query.date = Query.DateNow();
                    HistoryLog.AddQuery(query);
                   return NoContent();
                   }
                    return Ok(result);
                }
        }
        
        [HttpPost("GetByCategory")]
        public ActionResult<IEnumerable<Product>> GetByCategory(Data data)
        {
            int id = Int32.Parse(data.informacion);
            Query query = new Query("Get productos por categoria:" + id,data.usuario);
            if(!ProductsCache.ConsultCache(url + "?page=0&size=1000")){
                try
                {
                    IEnumerable<Product> products = DatabaseConsumer<Product>.GetAllProducts(url + "?page=0&size=1000");
                    if(products==null) return NotFound();
                    List<Product> result = new List<Product>();
                    foreach(Product p in result){
                        if(p!=null){
                            if(p.category.id.Equals(id)){
                                //System.Console.WriteLine(p.name + "(" + p.id +")");
                                //System.Console.WriteLine(p.category.id + ":" + p.category.name);
                                result.Add(p);
                            }
                            
                        }
                    }
                    if(result.Count==0){
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
            }else{
                List<Product> products = ProductsCache.GetAll();
                List<Product> result = new List<Product>();
                    foreach(Product p in products){
                        if(p!=null){
                            if(p.category.id.Equals(id)){
                                //System.Console.WriteLine(p.name + "(" + p.id +")");
                                //System.Console.WriteLine(p.category.id + ":" + p.category.name);
                                result.Add(p);
                            }
                            
                        }
                    }
                    if(result.Count==0){
                        query.status = "Incorrecto";
                        query.date = Query.DateNow();
                        HistoryLog.AddQuery(query);
                        return NoContent();
                    }
                    return Ok(result);
            }
        }
        
        [HttpPost("Post")]
        public ActionResult<Product> PostProduct(Data data)
        {
            Product newProduct = JsonHandler<Product>.Deserialize(data.informacion);
            Query query = new Query("Se creo un nuevo producto",data.usuario);
            ProductsCache.InvalidateCache();
            try
            {
                Product p = DatabaseConsumer<Product>.Post(url,JsonHandler<Product>.Serialize(newProduct));
                if(p!=null){
                    query.status = "Correcto";
                    query.date = Query.DateNow();
                    HistoryLog.AddQuery(query);
                    return Ok(p);
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
        public ActionResult<Product> PutProduct(Data data )
        {
            Product updatedProduct = JsonHandler<Product>.Deserialize(data.informacion);
            Query query = new Query("Se modifico info de un producto id:" + updatedProduct.id,data.usuario);
            ProductsCache.InvalidateCache();
            try
            {
                Product p = DatabaseConsumer<Product>.Put(url,JsonHandler<Product>.Serialize(updatedProduct));
                if(p!=null){ 
                    query.status = "Correcto";
                    query.date = Query.DateNow();
                    HistoryLog.AddQuery(query);
                    return Ok(p);
                }
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
        public ActionResult<Product> DeleteProduct(Data data)
        {
            int id = JsonHandler<Int32>.Deserialize(data.informacion);
            Query query = new Query("Se elimino un producto id:" + id,data.usuario);
            ProductsCache.InvalidateCache();
            try
            {
                bool p = DatabaseConsumer<Product>.Delete(url + $"?id={id}");
                if(p){
                    query.status = "Correcto";
                    query.date = Query.DateNow();
                    HistoryLog.AddQuery(query);
                    return Ok("Product eliminated");
                }  
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
        
        [HttpPost("UpdateStock")]
        public ActionResult UpdateStock(Data data)
        {
            ExtProduct[] products = JsonHandler<ExtProduct[]>.Deserialize(data.informacion);
            string pIDs = "";
            for(int i =0;i<products.Count();i++){
                pIDs += products[i].id + "";
                if(i != products.Count()-1){
                    pIDs += ",";
                }
            }
            Query query = new Query("Se actulizaron los stocks (una compra) de los productos:[" + pIDs + "]");
            ProductsCache.InvalidateCache();
            try
            {
                Product[] products1 = new Product[products.Length];
                
                for(int i=0;i<products.Length;i++){
                    products1[i] = DatabaseConsumer<Product>.Get(url + $"/find?id={products[i].id}");
                    products1[i].stock -= products[i].amount;
                    DatabaseConsumer<Product>.Put(url,JsonHandler<Product>.Serialize(products1[i]));
                }
                query.status = "Correcto";
                query.date = Query.DateNow();
                HistoryLog.AddQuery(query);
                return Ok();
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