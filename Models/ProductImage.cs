using System;
using System.Collections.Generic;

#nullable disable

namespace WebUI.Models
{
    public partial class ProductImage
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int ProductId { get; set; }
        public bool IsMainImg { get; set; }

        public virtual Product Product { get; set; }
    }
}
