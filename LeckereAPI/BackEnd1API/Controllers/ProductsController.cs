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
            return Ok("Conexion: " + ConnectionTester.getURL());
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            Query query = new Query("GET","Get all products");
            if(!ProductsCache.ConsultCache(url + "?page=0&size=1000")){
                try
                {
                    IEnumerable<Product> p = DatabaseConsumer<Product>.GetAllProducts(url + "?page=0&size=1000");
                    if(p!=null){
                        ProductsCache.AddCache(p.ToArray(), url + "?page=0&size=1000");
                        query.status = "Correcto";
                        query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                        
                        HistoryLog.AddQuery(query);
                        return Ok(p);
                    }
                    query.status = "Incorrecto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    HistoryLog.AddQuery(query);
                    return NotFound();   
                }
                catch (WebException ex)
                {
                    query.status = "Incorrecto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    HistoryLog.AddQuery(query);
                    return NotFound(ex.Message);
                }
            }else{
                return Ok(ProductsCache.GetAll());
            }
            
        }

        [HttpPost("GetList")]
        public ActionResult<IEnumerable<Product>> GetList(int[] ids)
        {
            string pIDS = "";
            for(int i=0;i<ids.Length;i++){
                pIDS += ids[i] +"";
                if(i <ids.Length-1){
                    pIDS += ",";
                }
            }
            Query query = new Query("GET","Get lista de productos : " + pIDS);
            ProductsCache.InvalidateCache();
            string data = JsonHandler<int[]>.Serialize(ids);
            bool inCache = false;
            for(int i =0; i<ids.Length; i++){
                inCache = ProductsCache.ConsultCache(ids[i]);
            }
            if(!inCache){
                try
                {
                    IEnumerable<Product> result = new List<Product>();
                    result = DatabaseConsumer<Product>.GetList(url + "/find-list",data);
                    if(result.ToList().Count>0){
                        ProductsCache.AddCache(result.ToArray(),url + "/find-list" + data);
                        //add a historylog
                        query.status = "Correcto";
                        query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                        HistoryLog.AddQuery(query);
                        return Ok(result);
                    } 
                    query.status = "Incorrecto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    HistoryLog.AddQuery(query);
                    return NotFound();
                }
                catch (WebException ex)
                {
                    query.status = "Incorrecto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    HistoryLog.AddQuery(query);
                    return NotFound(ex.Message);
                }
            }
            List<Product> r = new List<Product>();
            for(int i=0; i<ids.Length;i++){
                r.Add(ProductsCache.Get(i));
            }
            return Ok(r);
        }

        [HttpGet("GetId")]
        public ActionResult<Product> GetProductByID(int id)
        {
            Query query = new Query("GET","Get producto de id:" + id);
            if(!ProductsCache.ConsultCache(id)){
                try
                {
                    Product p = DatabaseConsumer<Product>.Get(url + $"/find?id={id}");
                    if(p!=null){
                        query.status = "Correcto";
                        query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                        HistoryLog.AddQuery(query);
                        return Ok(p);
                    }
                    query.status = "Incorrecto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    HistoryLog.AddQuery(query); 
                    return NotFound();
                }
                catch (WebException ex)
                {
                    query.status = "Incorrecto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    HistoryLog.AddQuery(query);
                    return NotFound(ex.Message);
                }
            }
            return Ok(ProductsCache.Get(id));
        }
        
        [HttpGet("GetByCatalog")]
        public ActionResult<IEnumerable<Product>> GetByCatalog(int id)
        {
            Query query = new Query("GET","Get productos por catalogo:" + id);
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
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    HistoryLog.AddQuery(query);
                    return NoContent();
                }
                query.status = "Correcto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                HistoryLog.AddQuery(query);
                return Ok(result);  
                }
                catch (WebException ex)
                {
                    query.status = "Incorrecto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    HistoryLog.AddQuery(query);
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
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    HistoryLog.AddQuery(query);
                   return NoContent();
                   }
                    return Ok(result);
                }
        }
        
        [HttpGet("GetByCategory")]
        public ActionResult<IEnumerable<Product>> GetByCategory(int id)
        {
            Query query = new Query("GET","Get productos por categoria:" + id);
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
                        query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                        HistoryLog.AddQuery(query);
                        return NoContent();
                    }
                    query.status = "Correcto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    HistoryLog.AddQuery(query);
                    return Ok(result);
                }
                catch (WebException ex)
                {
                    query.status = "Incorrecto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    HistoryLog.AddQuery(query);
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
                        query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                        HistoryLog.AddQuery(query);
                        return NoContent();
                    }
                    return Ok(result);
            }
        }
        
        [HttpPost("Post")]
        public ActionResult<Product> PostProduct(Product newProduct)
        {
            Query query = new Query("POST","Se creo un nuevo producto");
            ProductsCache.InvalidateCache();
            try
            {
                Product p = DatabaseConsumer<Product>.Post(url,JsonHandler<Product>.Serialize(newProduct));
                if(p!=null){
                    query.status = "Correcto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    HistoryLog.AddQuery(query);
                    return Ok(p);
                }
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                HistoryLog.AddQuery(query);
                return NotFound();
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                HistoryLog.AddQuery(query);
                return NotFound(ex.Message);
            }
        }
        
        [HttpPut("Put")]
        public ActionResult<Product> PutProduct(Product updatedProduct)
        {
            Query query = new Query("Put","Se modifico info de un producto id:" + updatedProduct.id);
            ProductsCache.InvalidateCache();
            try
            {
                Product p = DatabaseConsumer<Product>.Put(url,JsonHandler<Product>.Serialize(updatedProduct));
                if(p!=null){ 
                    query.status = "Correcto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    HistoryLog.AddQuery(query);
                    return Ok(p);
                }
                return NotFound();
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                HistoryLog.AddQuery(query);
                return NotFound(ex.Message);
            }
        }
        

        [HttpDelete("Delete")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            Query query = new Query("DELETE","Se elimino un producto id:" + id);
            ProductsCache.InvalidateCache();
            try
            {
                bool p = DatabaseConsumer<Product>.Delete(url + $"?id={id}");
                if(p){
                    query.status = "Correcto";
                    query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    HistoryLog.AddQuery(query);
                    return Ok("Product eliminated");
                }  
                return NotFound();
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                HistoryLog.AddQuery(query);
                return NotFound(ex.Message);
            }
        }
        
        [HttpPost("UpdateStock")]
        public ActionResult UpdateStock(ExtProduct[] products)
        {
            string pIDs = "";
            for(int i =0;i<products.Count();i++){
                pIDs += products[i].id + "";
                if(i != products.Count()-1){
                    pIDs += ",";
                }
            }
            Query query = new Query("POST","Se actulizaron los stocks (una compra) de los productos:[" + pIDs + "]");
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
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                HistoryLog.AddQuery(query);
                return Ok();
            }
            catch (WebException ex)
            {
                query.status = "Incorrecto";
                query.date = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                HistoryLog.AddQuery(query);
                return NotFound(ex.Message);
            }
        }
    }
}