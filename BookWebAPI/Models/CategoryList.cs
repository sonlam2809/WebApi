using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWebAPI.Models
{
    public class CategoryList
    {
        public List<Category> categories { get; set; }
        public string totalCount { get; set; }
    }
}