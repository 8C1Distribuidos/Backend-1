using System;

namespace BackEnd1API.Models{
    public class Query{
        public Query(){
            
        }
        public Query(string description){
            this.description = description;
            this.identifier = "Productos";
        }
        public Query( string description,string user){
            this.description = description;
            this.identifier = "Productos";
            this.user = user;
        }
        public string date { get; set; }
        public string description { get; set; }
        public string status  { get; set; }
        public string identifier {get;set;}
        public string user { get; set; }

        public static string DateNow(){
            string dateFormated = "";
            dateFormated += DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            return dateFormated;
        }
    }
}