using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Areas.Admin.Models
{
    public class ProductWithCategoriesModel
    {
        public Product Product { get; set; }
        public Category MainCategory { get; set; }
        public List<Category> Categories { get; set; }
    }
}
