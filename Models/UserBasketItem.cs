using System;
using System.Collections.Generic;

#nullable disable

namespace WebUI.Models
{
    public partial class UserBasketItem
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual UserBasket Basket { get; set; }
        public virtual Product Product { get; set; }
    }
}
