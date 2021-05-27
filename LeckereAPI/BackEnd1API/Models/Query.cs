using System;

namespace BackEnd1API.Models{
    public class Query{
        public Query(string action, string info){
            this.action = action;
            this.info = info;
        }
        public int id { get; set; }
        public string usuario { get; set; }
        public string action { get; set; }
        public bool status  { get; set; }
        public DateTime date { get; set; }
        public string info { get; set; }
    }
}