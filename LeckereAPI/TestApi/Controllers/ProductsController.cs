using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApi.Models;

namespace TestApi.Controllers
{
    public class ProductsController : ControllerBase {
        FakeForJSON fake = new FakeForJSON();

        [Route("api/GetAllProducts")]
        [HttpGet]
        public IEnumerable<Product> GetAll(){
            return fake.products;
        }
        [Route("api/GetProduct/id:{product_id}")]
        [HttpGet]
        public Product FindProduct(int product_id){
            if(product_id > fake.products.Count){
                return null;
            }
            foreach(Product p in fake.products){
                if(p.id == product_id){
                    return p;
                }
            }
            return null;
        }

        [Route("api/PushProduct")]
        [HttpPost]
        public bool AddProduct(Product p){
            fake.products.Add(p);
            return fake.products.Contains(p);
        }
    }
}