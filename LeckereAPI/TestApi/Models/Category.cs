namespace TestApi.Models{
    public class Category{
        public int id { get; set; }
        public string name { get; set; }
        public Catalog catalog { get; set; }
    }
}