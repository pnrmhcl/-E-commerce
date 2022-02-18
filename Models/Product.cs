using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebUI.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
            ProductImages = new HashSet<ProductImage>();
            UserBasketItems = new HashSet<UserBasketItem>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="İsim boş olamaz.")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Fiyat boş olamaz.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage ="Açıklama boş olamaz.")]

        public string Description { get; set; }
        [Required(ErrorMessage ="Stok adedi boş olamaz.")]
        public int UnitsInStock { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<UserBasketItem> UserBasketItems { get; set; }
    }
}
