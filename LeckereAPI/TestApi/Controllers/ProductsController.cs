using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller {
        public IEnumerable<Products> GetAll(){
            return new List<Products>{
                new Products{
                    id_product=1,
                    name_product="Gerico",
                    cost_product=12.00f
                },
                new Products{
                    id_product=2,
                    name_product="Vodka",
                    cost_product=20.00f
                },
                new Products{
                    id_product=3,
                    name_product="Ronalds",
                    cost_product=50.00f
                }
            };
        }
    }
}