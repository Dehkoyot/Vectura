using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VecturaServer.Models
{
    public class VisitModel
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public string Comment { get; set; }
        public string Date { get; set; }
        public int Raiting { get; set; }
    }
}