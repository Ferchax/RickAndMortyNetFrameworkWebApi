using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RickAndMorty3.Models
{
    public class PaginationModel
    {
        public int Count { get; set; }
        public int Pages { get; set; }
        public string Next { get; set; }
        public string Prev { get; set; }
    }
}