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
        public IEnumerable<Product> GetAll(){
            IEnumerable<Product> p = DatabaseConsumer<Product>.GetAll(url);
            if(p!=null) return p;
            return null;
        }
        [HttpGet("GetList")]
        public IEnumerable<Product> GetList([FromQuery(Name = "ids")] int[] ids){
            
            List<Product> p = new List<Product>();
            for(int i=0;i<ids.Length;i++){
                Product product = DatabaseConsumer<Product>.Get(url + $"/find?id={ids[i]}");
                if(product!=null) p.Add(product);
            }
            if(p.Count>0) return p;
            return null;
            
        }
        [HttpGet("GetId")]
        public ActionResult<Product> GetProductByID(int id){
            Product p = DatabaseConsumer<Product>.Get(url + $"/find?id={id}");
            if(p!=null) return Ok(p);
            return NotFound();
        }
        /*
        [HttpGet("GetClasification")]
        public IEnumerable<Product> GetProductByClasification(string clasification){
            bool isThere = false;
            foreach(Clasification c in fake.clasifications){
                if(c.name == clasification){
                    isThere=true;
                }
            }
            if(!isThere)return null;
            List<Product> productsWithClasisfication = new List<Product>();
            foreach(Product c in fake.products){
                if(c.clasification.name == clasification){
                    productsWithClasisfication.Add(c);
                }
            }
            return productsWithClasisfication;
        }
        [HttpGet("GetCatalog")]
        public IEnumerable<Product> GetProductByCatalog(string catalog){
            bool isThere = false;
            foreach(Catalog c in fake.catalogs){
                if(c.name == catalog){
                    isThere=true;
                }
            }
            if(!isThere)return null;
            List<Product> productsWithCatalog = new List<Product>();
            foreach(Product c in fake.products){
                if(c.clasification.catalog.name == catalog){
                    productsWithCatalog.Add(c);
                }
            }
            return productsWithCatalog;
        }
        */
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