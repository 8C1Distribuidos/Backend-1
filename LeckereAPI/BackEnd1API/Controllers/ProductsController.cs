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
            return Ok("Funcionando");
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            try
            {
                IEnumerable<Product> p = DatabaseConsumer<Product>.GetAllProducts(url + "?page=0&size=1000");
                if(p!=null) return Ok(p);
                return NotFound();   
            }
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("GetList")]
        public ActionResult<IEnumerable<Product>> GetList(int[] ids)
        {
            try
            {
                string data = JsonHandler<int[]>.Serialize(ids);
                IEnumerable<Product> result = new List<Product>();
                result = DatabaseConsumer<Product>.GetList(url + "/find-list",data);
                if(result.ToList().Count>0) return Ok(result);
                return NotFound();
            }
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [HttpGet("GetId")]
        public ActionResult<Product> GetProductByID(int id)
        {
            try
            {
                Product p = DatabaseConsumer<Product>.Get(url + $"/find?id={id}");
                if(p!=null) return Ok(p);
                return NotFound();
            }
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        [HttpGet("GetByCatalog")]
        public ActionResult<IEnumerable<Product>> GetByCatalog(int id)
        {
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
                if(result.Count==0)return NoContent();
                return Ok(result);
            }
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        [HttpGet("GetByCategory")]
        public ActionResult<IEnumerable<Product>> GetByCategory(int id)
        {
            try
            {
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
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        [HttpPost("Post")]
        public ActionResult<Product> PostProduct(Product newProduct)
        {
            try
            {
                Product p = DatabaseConsumer<Product>.Post(url,JsonHandler<Product>.Serialize(newProduct));
                if(p!=null) return Ok(p);
                return NotFound();
            }
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        [HttpPut("Put")]
        public ActionResult<Product> PutProduct(Product updatedProduct)
        {
            try
            {
                Product p = DatabaseConsumer<Product>.Put(url,JsonHandler<Product>.Serialize(updatedProduct));
                if(p!=null) return Ok(p);
                return NotFound();
            }
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
        }
        

        [HttpDelete("Delete")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            try
            {
                bool p = DatabaseConsumer<Product>.Delete(url + $"?id={id}");
                if(p) return Ok("Product eliminated");
                return NotFound();
            }
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        [HttpPost("UpdateStock")]
        public ActionResult UpdateStock(ExtProduct[] products)
        {
            try
            {
                bool isGood = true;
                for(int i=0;i<products.Length;i++){
                    products[i].stock -= products[i].amaunt;
                    Product p = (Product) products[i];
                    Product res = DatabaseConsumer<Product>.Put(url,JsonHandler<Product>.Serialize(p));
                    if(res==null)isGood = false;
                }
                if(!isGood) NotFound("No todos los objetos se actualizaron");
                return Ok();
            }
            catch (WebException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}