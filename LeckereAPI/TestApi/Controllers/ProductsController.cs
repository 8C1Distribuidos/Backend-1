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
        public FakeForJSON fake = new FakeForJSON();
        [Route("api/Product/GetAll")]
        [HttpGet]
        public IEnumerable<Product> GetAll(){
            return fake.products;
            //return List
        }

        [Route("api/PostProduct:{json}")]
        [HttpPost]
        public string AddProduct(string json){
            return json;
        }
    }
}