using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApi.Models;

namespace TestApi.Controllers
{
    public class CatalogsController : ControllerBase {
        FakeForJSON fake = new FakeForJSON();

        [Route("api/GetAllCatalogs")]
        [HttpGet]
        public IEnumerable<Catalog> GetAll(){
            return fake.catalogs;
        }
    }
}