using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BackEnd1API.Models;
using BackEnd1API.Controllers;

namespace BackEnd1API.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductsController : ControllerBase {
        string url = "http://localhost:9081/products";
        //FakeForJSON fake = Startup.fake;
        [HttpGet("Test")]
        public IActionResult Test(){
            return Ok("Funcionando");
        }
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Product>> GetAll(){
            IEnumerable<Product> p = DatabaseConsumer<Product>.GetAllProducts(url + "?page=0&size=1000");
            if(p!=null) return Ok(p);
            return NotFound();
        }
        [HttpPost("GetList")]
        public ActionResult<IEnumerable<Product>> GetList(List<int> ids){
            //int[] ids = JsonHandler<int[]>.Deserialize(product);
            List<Product> result = new List<Product>();
            for(int i=0;i<ids.Count;i++){
                Product p = DatabaseConsumer<Product>.Get(url + $"/find?id={ids[i]}");
                if(p!=null) result.Add(p);
            }
            if(result.Count>0) return Ok(result);
            return NotFound();
            
        }
        [HttpGet("GetId")]
        public ActionResult<Product> GetProductByID(int id){
            Product p = DatabaseConsumer<Product>.Get(url + $"/find?id={id}");
            if(p!=null) return Ok(p);
            return NotFound();
        }
        
        [HttpGet("GetByCatalog")]
        public ActionResult<IEnumerable<Product>> GetByCatalog(int id){
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
            if(result.Count==0)return NoContent();
            return Ok(result);
        }
        
        [HttpGet("GetByCategory")]
        public ActionResult<IEnumerable<Product>> GetByCategory(int id){
            IEnumerable<Product> products = DatabaseConsumer<Product>.GetAllProducts(url + "?page=0&size=1000");
            if(products==null) return NotFound();
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
            if(result.Count==0)return NoContent();
            return Ok(result);
        }
        
        [HttpPost("Post")]
        public ActionResult<Product> PostProduct(Product newProduct){
            Product p = DatabaseConsumer<Product>.Post(url,JsonHandler<Product>.Serialize(newProduct));
            if(p!=null) return Ok(p);
            return NotFound();
        }
        
        [HttpPut("Put")]
        public ActionResult<Product> PutProduct(Product updatedProduct){
            Product p = DatabaseConsumer<Product>.Put(url,JsonHandler<Product>.Serialize(updatedProduct));
            if(p!=null) return Ok(p);
            return NotFound();
        }
        

        [HttpDelete("Delete")]
        public ActionResult<Product> DeleteProduct(int id){
            bool p = DatabaseConsumer<Product>.Delete(url + $"?id={id}");
            if(p) return Ok("Product eliminated");
            return NotFound();
        }
        
    }
}