using System;
using System.Collections.Generic;

#nullable disable

namespace WebUI.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            UserBaskets = new HashSet<UserBasket>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserBasket> UserBaskets { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
