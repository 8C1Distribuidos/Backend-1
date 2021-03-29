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
        public Product GetProductByID(int id){
            foreach(Product c in fake.products){
                if(c.id == id){
                    //return JsonHandler<Product>.Serialize(c);
                    return c;
                }
            }
            return null;
        }

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