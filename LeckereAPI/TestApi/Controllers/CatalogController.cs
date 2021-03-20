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
        [Route("api/GetAllCatalogs")]
        [HttpGet]
        public Catalog GetCatalogByID(int id){
            foreach(Catalog c in fake.catalogs){
                if(c.id == id){
                    return c;
                }
            }
            return null;
        }

        [Route("api/PostCatalog")]
        [HttpPost]
        public bool PostCatalog(Catalog c){
            if(fake.catalogs.Contains(c)){
                return false;
            }
            else{
                fake.catalogs.Add(c);
                return true;
            }
        }

        [Route("api/PutCatalog")]
        [HttpPost]
        public bool PutCatalog(Catalog updatedCatalog){
            foreach(Catalog c in fake.catalogs){
                if(c.id == updatedCatalog.id){
                    c.name = updatedCatalog.name;
                    return true;
                }
            }
            return false;
        }

        [Route("api/DeleteCatalog")]
        [HttpPost]
        public bool DeleteCatalog(int id){
            foreach(Catalog c in fake.catalogs){
                if(c.id == id){
                    fake.catalogs.Remove(c);
                    return true;
                }
            }
            return false;
        }
    }
}