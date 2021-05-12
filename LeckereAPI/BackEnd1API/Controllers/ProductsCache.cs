using System.Collections.Generic;
using BackEnd1API.Models;

namespace BackEnd1API.Controllers
{
    public class ProductsCache{
        private static List<Product> cache =  new List<Product>();
        private static List<string> cachedQueries = new List<string>();

        public static bool ConsultCache(int id){
            if(cache.Count == 0) return false;
            foreach(Product p in cache){
                if(p.id.Equals(id)) return true;
            }
            return false;
        }
        public static bool ConsultCache(string query){
            if(cachedQueries.Count == 0) return false;
            foreach(string s in cachedQueries){
                if(s.Equals(query)) return true;
            }
            return false;
        }

        public static void AddCache(Product newProduct, string query){
            if(!cachedQueries.Contains(query)){
                cachedQueries.Add(query);
                cache.Add(newProduct);
            }else{
                int i = 0;
                foreach(Product p in cache){
                    if(p.id.Equals(newProduct.id)){
                        cache[i] = newProduct;
                    }
                    i++;
                }
            }
        }

        public static void AddCache(Product[] newProducts, string query){
            if(!cachedQueries.Contains(query)){
                cachedQueries.Add(query);
                for(int i=0;i<newProducts.Length;i++){
                    cache.Add(newProducts[i]);
                }
            }else{
                for(int i=0;i<newProducts.Length;i++){
                    int x=0;
                    foreach(Product p in cache){
                        if(p.id.Equals(newProducts[i].id)){
                            cache[i] = newProducts[x];
                        }
                        x++;
                    }
                    
                }
            }
        }

        public static void InvalidateCache(){
            cache.Clear();
            cachedQueries.Clear();
        }

        public static Product Get(int id){
            foreach(Product p in cache){
                System.Console.WriteLine("Cache consulted data retrived:");
                System.Console.WriteLine(p.id +" : " + p.name);
                if(p.id.Equals(id)) return p;
            }
            InvalidateCache();
            System.Console.WriteLine("Error fatal: se perdio la cache");
            return null;
        }

        public static List<Product> GetAll(){
            return cache;
        }
    }
}