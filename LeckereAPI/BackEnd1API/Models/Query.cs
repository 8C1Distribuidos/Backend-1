using System;

namespace BackEnd1API.Models{
    public class Query{
        public Query(string action, string info){
            this.action = action;
            this.info = info;
            this.device = "Productos";
        }
        public string usuario { get; set; }
        public string action { get; set; }
        public string status  { get; set; }
        public string device {get;set;}
        public string date { get; set; }
        public string info { get; set; }

        public static string DateNow(){
            string dateFormated = "";
            dateFormated += DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            return dateFormated;
        }
    }
}