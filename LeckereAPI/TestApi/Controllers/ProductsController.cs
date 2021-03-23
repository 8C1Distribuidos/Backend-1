using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductsController : ControllerBase {
        FakeForJSON fake = Startup.fake;
        [HttpGet("Test")]
        public IActionResult Test(){
            return Ok("Funcionando");
        }

        [HttpGet("GetAll")]
        public IEnumerable<Product> GetAll(){
            return fake.products;
        }

        [HttpGet("GetId")]
        public string GetProductgByID(int id){
            foreach(Product c in fake.products){
                if(c.id == id){
                    return JsonHandler<Product>.Serialize(c);
                }
            }
            return null;
        }

        
        [HttpPost("Post")]
        public bool PostProduct(Product newProduct){
            //Catalog c = JsonHandler<Catalog>.Deserialize(jsonString);
            foreach (Product c in fake.products)
            {
                if(c.id == newProduct.id){
                    return false;
                }
            }
            fake.products.Add(newProduct);
            return true;
        }

        [HttpPut("Put")]
        public bool PutProduct(Product updatedProduct){
            //Catalog updatedCatalog = JsonHandler<Catalog>.Deserialize(jsonString);
            foreach(Product c in fake.products){
                if(c.id == updatedProduct.id){
                    c.name = updatedProduct.name;
                    c.image = updatedProduct.image;
                    c.stock = updatedProduct.stock;
                    c.cost = updatedProduct.cost;
                    c.clasification = updatedProduct.clasification;
                    return true;
                }
            }
            return false;
        }

        [HttpDelete("Delete")]
        public bool DeleteProduct(int id){
            foreach(Product c in fake.products){
                if(c.id == id){
                    fake.products.Remove(c);
                    return true;
                }
            }
            return false;
        }
    }
}