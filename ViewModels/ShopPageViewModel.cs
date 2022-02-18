using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.ViewModels
{
    public class ShopPageViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}
