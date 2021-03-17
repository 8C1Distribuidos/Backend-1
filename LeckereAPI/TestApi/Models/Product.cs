namespace TestApi.Models{
    public class Product{
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public float cost { get; set; }
        public int stock { get; set; }
        public Clasification clasification { get; set; }
    }
}