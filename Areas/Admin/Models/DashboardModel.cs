using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Areas.Admin.Models
{
    public class DashboardModel
    {
        public int ProductCount { get; set; }
        public int CategoryCount { get; set; }
        public int OrderCount { get; set; }
        public int UserCount { get; set; }
    }
}
