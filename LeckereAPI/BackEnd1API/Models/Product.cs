
namespace BackEnd1API.Models{
    public class Product{
        public int id { get; set; }
        public string name { get; set; }
        public string imageLink { get; set; }
        public float price { get; set; }
        public int stock { get; set; }
        public Category category { get; set; }
    }
}