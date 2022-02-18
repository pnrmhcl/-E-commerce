using System;
using System.Collections.Generic;

#nullable disable

namespace WebUI.Models
{
    public partial class UserBasket
    {
        public UserBasket()
        {
            UserBasketItems = new HashSet<UserBasketItem>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<UserBasketItem> UserBasketItems { get; set; }
    }
}
