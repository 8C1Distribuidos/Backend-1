using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApi.Models;

namespace TestApi.Controllers
{
    public class ClasificationsController : ControllerBase {
        FakeForJSON fake = new FakeForJSON();

        [Route("api/GetAllClasifications")]
        [HttpGet]
        public IEnumerable<Clasification> GetAll(){
            return fake.clasifications;
        }
        [Route("api/GetClasification")]
        [HttpGet]
        public Clasification Get(int id){
            foreach(Clasification c in fake.clasifications)
            {
                if(c.id == id){
                    return c;
                }
            }
            return null;
        }

        
    }
}